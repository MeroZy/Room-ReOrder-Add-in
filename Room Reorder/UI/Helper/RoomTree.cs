using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Room_Reorder.Methods;

namespace Room_Reorder.Helpers
{
    public static class RoomTreeHelper
    {
        public static void PopulateTreeView(Document doc, TreeView treeView)
        {
            treeView.Nodes.Clear();
            treeView.BeginUpdate();

            List<Level> levels = new FilteredElementCollector(doc)
                .OfClass(typeof(Level))
                .Cast<Level>()
                .OrderBy(l => l.Elevation)
                .ToList();

            foreach (Level level in levels)
            {
                TreeNode levelNode = new TreeNode(level.Name);
                levelNode.Tag = level.Id;

                List<SpatialElement> roomsOnLevel = GetRoomsOnLevel(doc, level.Id);

                if (roomsOnLevel.Count > 0)
                {
                    BuildLevelHierarchy(doc, levelNode, roomsOnLevel);
                }

                if (levelNode.Nodes.Count > 0)
                {
                    treeView.Nodes.Add(levelNode);
                }
            }

            treeView.EndUpdate();
            treeView.ExpandAll();
        }

        private static void BuildLevelHierarchy(Document doc, TreeNode parentNode, List<SpatialElement> rooms)
        {
            
            Dictionary<ElementId, List<ElementId>> roomGraph = FlowSortMethod.GetRoomConnections(doc, rooms); // from flow sort method

            HashSet<ElementId> visitedIds = new HashSet<ElementId>();

            SpatialElement rootRoom = rooms
                .OrderByDescending(r => roomGraph.ContainsKey(r.Id) ? roomGraph[r.Id].Count : 0)
                .FirstOrDefault();

            if (rootRoom == null) return;

            AddRoomNodeRecursive(rootRoom, parentNode, roomGraph, rooms, visitedIds);

            // hundle Orphans
            var orphans = rooms.Where(r => !visitedIds.Contains(r.Id)).ToList();
            if (orphans.Count > 0)
            {
                TreeNode orphanNode = new TreeNode("Disconnected / Others");
                foreach (var orphan in orphans)
                {
                    orphanNode.Nodes.Add(new TreeNode($"{orphan.Number} - {orphan.Name}"));
                }
                parentNode.Nodes.Add(orphanNode);
            }
        }

        private static void AddRoomNodeRecursive(
            SpatialElement currentRoom,
            TreeNode parentTreeNode,
            Dictionary<ElementId, List<ElementId>> graph,
            List<SpatialElement> allRooms,
            HashSet<ElementId> visitedIds)
        {
            visitedIds.Add(currentRoom.Id);

            TreeNode roomNode = new TreeNode($"{currentRoom.Number} - {currentRoom.Name}");
            roomNode.Tag = currentRoom;
            parentTreeNode.Nodes.Add(roomNode);

            if (graph.ContainsKey(currentRoom.Id))
            {
                List<ElementId> neighborIds = graph[currentRoom.Id];

                foreach (ElementId id in neighborIds)
                {
                    if (!visitedIds.Contains(id))
                    {
                        SpatialElement neighbor = allRooms.FirstOrDefault(x => x.Id == id);
                        if (neighbor != null)
                        {
                            AddRoomNodeRecursive(neighbor, roomNode, graph, allRooms, visitedIds);
                        }
                    }
                }
            }
        }

        private static List<SpatialElement> GetRoomsOnLevel(Document doc, ElementId levelId)
        {
            return new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Rooms)
                .WhereElementIsNotElementType()
                .Cast<SpatialElement>()
                .Where(x => x.LevelId == levelId && x.Location != null)
                .ToList();
        }

    }
}