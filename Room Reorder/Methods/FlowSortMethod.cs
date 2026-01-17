using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Room_Reorder.Methods
{
    public static class FlowSortMethod
    {
        /// <summary>
        /// Sorts the rooms on a specific level based on their flow characteristics.
        /// </summary>
        /// <param name="doc">The Revit document.</param>
        /// <param name="roomsOnLevel">The list of rooms on the specific level.</param>
        /// <param name="startPoint">The starting point for the flow analysis.</param>
        /// <returns>A sorted list of rooms based on their flow characteristics.</returns>
        public static List<SpatialElement> SortRoomsByFlow(Document doc, List<SpatialElement> roomsOnLevel, XYZ startPoint)
        {
            // =========================================================================================
            // 1. SAFETY & PREPARATION
            // =========================================================================================
            // Filter out rooms that are "Unplaced" or "Not Enclosed" to prevent crashes (NullReferenceException).
            List<SpatialElement> validRooms = roomsOnLevel
                .Where(r => r.Location != null && r.Location is LocationPoint)
                .ToList();

            if (validRooms.Count == 0) return roomsOnLevel;

            // Build the "Map" of the building. This Dictionary tells us which room connects to which.
            Dictionary<ElementId, List<ElementId>> roomGraph = GetRoomConnections(doc, validRooms);

            // =========================================================================================
            // 2. SMART START POINT DETECTION (The "Golden Rule" Logic)
            // =========================================================================================
            SpatialElement startRoom = null;

            // PRIORITY A: Is the point physically INSIDE a room?
            // This is the most accurate check. If you click inside the Lobby, the Lobby wins.
            foreach (SpatialElement r in validRooms)
            {
                // specific cast for Revit API
                Room room = r as Room;

                if (room != null && room.IsPointInRoom(startPoint))
                {
                    startRoom = r;
                    break;
                }
            }

            // PRIORITY B: If point is on a wall/door (technically "outside"), find the best nearby room.
            if (startRoom == null)
            {
                double searchRadius = 15.0; // Search range (feet)

                var nearbyRooms = validRooms
                    .Select(r => new {
                        Room = r,
                        Distance = ((LocationPoint)r.Location).Point.DistanceTo(startPoint)
                    })
                    .Where(x => x.Distance < searchRadius)
                    .OrderBy(x => x.Distance) // Closest first
                    .ToList();

                if (nearbyRooms.Count > 0)
                {
                    // "Mother Room" Logic:
                    // We don't just want the closest room (it might be a tiny closet).
                    // We want the closest room that is ALSO a "Connector" (has more than 1 door).

                    var bestCandidate = nearbyRooms.FirstOrDefault(x =>
                    {
                        int connections = roomGraph.ContainsKey(x.Room.Id) ? roomGraph[x.Room.Id].Count : 0;
                        return connections > 1; // Skip "Dead Ends" (Closets/Shafts)
                    });

                    // If we found a connector, use it. Otherwise, fallback to the absolute closest.
                    startRoom = (bestCandidate != null) ? bestCandidate.Room : nearbyRooms.First().Room;
                }
                else
                {
                    // Fallback: User clicked way outside. Just pick the mathematically closest room.
                    startRoom = validRooms
                        .OrderBy(r => ((LocationPoint)r.Location).Point.DistanceTo(startPoint))
                        .FirstOrDefault();
                }
            }

            if (startRoom == null) return validRooms; // Safety fallback if something went wrong

            // =========================================================================================
            // 3. DEPTH-FIRST SEARCH (DFS) TRAVERSAL
            // =========================================================================================
            // We use a STACK (Last-In, First-Out) to ensure that when we enter a room (like a suite),
            // we finish numbering its inner rooms (Bath, Closet) before moving back to the Hallway.

            List<SpatialElement> sortedList = new List<SpatialElement>();
            HashSet<ElementId> visitedIds = new HashSet<ElementId>();
            Stack<ElementId> stack = new Stack<ElementId>();

            stack.Push(startRoom.Id);
            visitedIds.Add(startRoom.Id);

            while (stack.Count > 0)
            {
                ElementId currentId = stack.Pop();

                SpatialElement currentRoom = validRooms.FirstOrDefault(r => r.Id == currentId);
                if (currentRoom != null)
                {
                    sortedList.Add(currentRoom);

                    if (roomGraph.ContainsKey(currentId))
                    {
                        List<ElementId> neighbors = roomGraph[currentId];

                        // =============================================================================
                        // KEY LOGIC: GLOBAL DISTANCE SORTING
                        // =============================================================================
                        // Question: In what order do we push neighbors to the Stack?
                        // Answer: We want the room CLOSEST to the Start Point to be processed NEXT.
                        // Since Stack is LIFO (Last In, First Out), we must push the FARTHEST rooms FIRST.

                        var sortedNeighbors = neighbors
                            .Where(id => !visitedIds.Contains(id))
                            .OrderByDescending(id => // DESCENDING = Farthest goes to bottom of stack
                            {
                                var neighborRoom = validRooms.FirstOrDefault(r => r.Id == id);
                                if (neighborRoom == null) return 0;

                                // We compare distance to 'startPoint' (Global), not 'currentRoom' (Local).
                                // This keeps the numbering flowing away from the Lobby naturally.
                                return ((LocationPoint)neighborRoom.Location).Point.DistanceTo(startPoint);
                            })
                            .ToList();

                        foreach (ElementId neighborId in sortedNeighbors)
                        {
                            visitedIds.Add(neighborId);
                            stack.Push(neighborId);
                        }
                    }
                }
            }

            // =========================================================================================
            // 4. ORPHAN HANDLING
            // =========================================================================================
            // Collect any rooms that were not reached by walking through doors (e.g., detached buildings).
            // We sort them simply by X coordinate as a fallback.
            var unvisitedRooms = validRooms
                .Where(r => !visitedIds.Contains(r.Id))
                .OrderBy(r => ((LocationPoint)r.Location).Point.X)
                .ToList();

            sortedList.AddRange(unvisitedRooms);

            return sortedList;
        }
        ///
        // =============================================================================================
        // HELPER: BUILD RELATIONSHIPS
        // =============================================================================================
        /// <summary>
        /// Builds a graph of room connections based on door locations.
        /// </summary>
        /// <param name="doc">The Revit document.</param>
        /// <param name="roomsOnLevel">The list of rooms on the specific level.</param>
        /// <returns>A dictionary representing room connections with their ids.</returns>
        public static Dictionary<ElementId, List<ElementId>> GetRoomConnections(Document doc, List<SpatialElement> roomsOnLevel)
        {
            Dictionary<ElementId, List<ElementId>> graph = new Dictionary<ElementId, List<ElementId>>();
            HashSet<ElementId> levelRoomIds = new HashSet<ElementId>(roomsOnLevel.Select(r => r.Id));

            // Optimize: Only get doors on the specific level we are processing
            ElementId levelId = roomsOnLevel.First().LevelId;

            List<FamilyInstance> doors = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Doors)
                .OfClass(typeof(FamilyInstance))
                .WhereElementIsNotElementType()
                .Cast<FamilyInstance>()
                .Where(d => d.LevelId == levelId)
                .ToList();

            foreach (FamilyInstance door in doors)
            {
                // Retrieve the correct Phase to find To/From rooms accurately
                ElementId phaseId = door.CreatedPhaseId;
                Phase doorPhase = doc.GetElement(phaseId) as Phase;

                if (doorPhase != null)
                {
                    Room room1 = door.get_FromRoom(doorPhase);
                    Room room2 = door.get_ToRoom(doorPhase);

                    if (room1 != null && room2 != null)
                    {
                        // Only link if both rooms are in our "Valid List" (ignore demolished/unplaced rooms)
                        if (levelRoomIds.Contains(room1.Id) && levelRoomIds.Contains(room2.Id))
                        {
                            AddConnection(graph, room1.Id, room2.Id);
                            AddConnection(graph, room2.Id, room1.Id);
                        }
                    }
                }
            }
            return graph;
        }

        /// <summary>
        /// Adds a connection between two room IDs in the graph.
        /// </summary>
        /// <param name="graph">The graph representing room connections.</param>
        /// <param name="id1">The ID of the first room.</param>
        /// <param name="id2">The ID of the second room.</param>
        private static void AddConnection(Dictionary<ElementId, List<ElementId>> graph, ElementId id1, ElementId id2)
        {
            if (!graph.ContainsKey(id1)) graph[id1] = new List<ElementId>();
            if (!graph[id1].Contains(id2)) graph[id1].Add(id2);
        }
    }
}