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
        public static List<TreeNode> _allNodes { get; set; }

        public static void PopulateTreeView(Document doc, TreeView treeView)
        {
            treeView.Nodes.Clear();
            treeView.BeginUpdate();

            // Sort Levels by Elevation so the tree goes Level 1 -> Level 2
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
            Dictionary<ElementId, List<ElementId>> roomGraph = FlowSortMethod.GetRoomConnections(doc, rooms);

            HashSet<ElementId> visitedIds = new HashSet<ElementId>();

            // Find the "Mother Room" (Root)
            SpatialElement rootRoom = rooms
                .OrderByDescending(r => roomGraph.ContainsKey(r.Id) ? roomGraph[r.Id].Count : 0)
                .FirstOrDefault();

            if (rootRoom == null) return;

            AddRoomNodeRecursive(rootRoom, parentNode, roomGraph, rooms, visitedIds);

            // Handle Orphans (Disconnected rooms)
            var orphans = rooms
                .Where(r => !visitedIds.Contains(r.Id))
                .OrderBy(r => r.Number)
                .ToList();

            if (orphans.Count > 0)
            {
                TreeNode orphanNode = new TreeNode("Disconnected / Others");

                foreach (var orphan in orphans)
                {
                    string displayName = GetCleanDisplayName(orphan);

                    //Create the Node variable first
                    TreeNode childNode = new TreeNode(displayName);

                    // Set the Tag 
                    childNode.Tag = orphan;

                    // Add it to the parent
                    orphanNode.Nodes.Add(childNode);
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

            // Remove number from name
            string displayName = GetCleanDisplayName(currentRoom);

            TreeNode roomNode = new TreeNode(displayName);
            roomNode.Tag = currentRoom;
            parentTreeNode.Nodes.Add(roomNode);

            if (graph.ContainsKey(currentRoom.Id))
            {
                List<ElementId> neighborIds = graph[currentRoom.Id];

                // Instead of processing random IDs, we look up the rooms and SORT them first.
                var sortedNeighbors = neighborIds
                    .Select(id => allRooms.FirstOrDefault(r => r.Id == id))
                    .Where(r => r != null && !visitedIds.Contains(r.Id))
                    .OrderBy(r => r.Number) 
                    .ToList();

                foreach (SpatialElement neighbor in sortedNeighbors)
                {
                    AddRoomNodeRecursive(neighbor, roomNode, graph, allRooms, visitedIds);
                }
            }
        }

        private static string GetCleanDisplayName(SpatialElement room)
        {
            string number = room.Number;
            string name = room.Name;

            string cleanName = name.Replace(number, "").Trim();

            // fix no name
            if (string.IsNullOrWhiteSpace(cleanName) || cleanName == "-")
            {
                cleanName = "Room";
            }

            // "101 - Room"
            return $"{number} - {cleanName}";
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


        // Caching Full Tree for Search Functionality
        public static void CacheFullTree()
        {
            _allNodes = new List<TreeNode>();

            foreach (TreeNode node in ExtCmd.Mainform.treeViewRooms.Nodes)
                _allNodes.Add(CloneNode(node));
        }

        public static TreeNode CloneNode(TreeNode node)
        {
            TreeNode newNode = new TreeNode(node.Text)
            {
                Tag = node.Tag
            };

            foreach (TreeNode child in node.Nodes)
                newNode.Nodes.Add(CloneNode(child));

            return newNode;
        }
    }
}