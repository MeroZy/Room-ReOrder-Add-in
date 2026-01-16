using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Room_Reorder.Revit;

namespace Room_Reorder.Methods
{
    public static class RenumberingMethod
    {
        public static void GetRoomData(Document doc) //for testing
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            ICollection<Element> rooms = collector.OfCategory(BuiltInCategory.OST_Rooms)
                                                  .WhereElementIsNotElementType()
                                                  .ToElements();

            StringBuilder sb = new StringBuilder();
            foreach (Element e in rooms)
            {
                SpatialElement room = e as SpatialElement;
                if (room != null)
                {
                    string roomName = room.Name;
                    string roomNumber = room.Number;
                    string roomLevel = room.Level.Name;
                    sb.AppendLine($"Room: {roomName}, Number: {roomNumber}, Level: {roomLevel}");
                }     
            }
            MessageBox.Show(sb.ToString());
        }
        public static void RenumberRooms(Document doc, string groundLevelName)
        {
            bool defaultXY = false;
 
            List<SpatialElement> allRooms = new FilteredElementCollector(doc)
                                            .OfCategory(BuiltInCategory.OST_Rooms)
                                            .WhereElementIsNotElementType()
                                            .Cast<SpatialElement>() //for rooms
                                            .Where(x => x.Location != null) //ignonre unplaced or undbounded rooms
                                            .ToList();

            // groub by level id excludes any levels that have NO rooms
            Dictionary<ElementId, List<SpatialElement>> roomsByLevelId = allRooms
                .GroupBy(x => x.LevelId)
                .ToDictionary(x => x.Key, x => x.ToList()); //Tkey - Elemntid, Tvalue - Room

            // get real lvls
            List<Level> activeLevels = new List<Level>();
            foreach (ElementId id in roomsByLevelId.Keys)
            {
                Level lvl = doc.GetElement(id) as Level;
                if (lvl != null) activeLevels.Add(lvl);
            }

            // sort lvls
            activeLevels = activeLevels.OrderBy(x => x.Elevation).ToList();

            // find (Ground Level)
            int groundIndex = activeLevels.FindIndex(x => x.Name.Equals(groundLevelName, StringComparison.InvariantCultureIgnoreCase));

            List<Level> upperLevels = new List<Level>();
            List<Level> lowerLevels = new List<Level>();

            if (groundIndex == -1)
            {
                // handle level name wrong
                Level userGroundLevel = new FilteredElementCollector(doc)
                    .OfClass(typeof(Level))
                    .WhereElementIsNotElementType()
                    .Cast<Level>()
                    .FirstOrDefault(x => x.Name.Equals(groundLevelName, StringComparison.InvariantCultureIgnoreCase));

                if (userGroundLevel == null)
                {
                    TaskDialog.Show("Error", $"The level '{groundLevelName}' does not exist in the project");
                    return;
                }

                //choose the closest one
                groundIndex = activeLevels.FindIndex(x => x.Elevation >= userGroundLevel.Elevation);

                if (groundIndex == -1)
                {
                    TaskDialog.Show("Notice", "No levels with rooms found above the specified Ground Level\n\n" +
                        "Program will continue defining all levels as underground");
                    lowerLevels = activeLevels
                        .OrderByDescending(x => x.Elevation)
                        .ToList();
                }
                else
                {
                    // Split the list
                    upperLevels = activeLevels.Skip(groundIndex).ToList();
                    lowerLevels = activeLevels.Take(groundIndex)
                        .OrderByDescending(x => x.Elevation)
                        .ToList();
                }
            }
            else
            {
                // Split the list
                upperLevels = activeLevels.Skip(groundIndex).ToList();
                lowerLevels = activeLevels.Take(groundIndex)
                    .OrderByDescending(x => x.Elevation)
                    .ToList();
            }




                using (Transaction trans = new Transaction(doc, "Renumber Rooms"))
                {
                    trans.Start();

                    int upperLevelCounter = 1;

                    foreach (Level level in upperLevels)
                    {
                        // get list of rooms from dic
                        var roomsOnThisLevel = roomsByLevelId[level.Id];

                        // sort rooms

                        var sortedRooms = new List<SpatialElement>();

                        if (!defaultXY) sortedRooms = FlowSortMethod.SortRoomsByFlow(doc, roomsOnThisLevel, RvtData.StartingPoint);
                        else sortedRooms = SortRoomsByXY(roomsOnThisLevel);

                        int roomCounter = 1;
                        foreach (SpatialElement room in sortedRooms)
                        {
                            // Level + Room (e.g., 205)
                            string newNumber = $"{upperLevelCounter}{roomCounter.ToString("D2")}";
                            SetRoomNumber(doc, room, newNumber);
                            roomCounter++;
                        }
                        upperLevelCounter++;
                    }


                    int lowerLevelCounter = 1; // 1 = Closest to Ground (B1)

                    foreach (Level level in lowerLevels)
                    {
                        var roomsOnThisLevel = roomsByLevelId[level.Id];

                        var sortedRooms = new List<SpatialElement>();

                        if (!defaultXY) sortedRooms = FlowSortMethod.SortRoomsByFlow(doc, roomsOnThisLevel, RvtData.StartingPoint);
                        else sortedRooms = SortRoomsByXY(roomsOnThisLevel);

                        int roomCounter = 1;
                        foreach (SpatialElement room in sortedRooms)
                        {
                            //P + Level + Room (e.g., P105)
                            string newNumber = $"P{lowerLevelCounter}{roomCounter.ToString("D2")}";
                            SetRoomNumber(doc, room, newNumber);
                            roomCounter++;
                        }
                        lowerLevelCounter++;
                    }

                    trans.Commit();
                }
        }


        private static List<SpatialElement> SortRoomsByXY(List<SpatialElement> rooms)
        {
            return rooms
                .OrderBy(r => ((LocationPoint)r.Location).Point.X)
                .ThenBy(r => ((LocationPoint)r.Location).Point.Y)
                .ToList();
        }

        private static void SetRoomNumber(Document doc, SpatialElement room, string number)
        {
            try
            {
                room.Number = number;
            }
            catch
            {
                SpatialElement conflictingRoom = new FilteredElementCollector(doc)
                    .OfCategory(BuiltInCategory.OST_Rooms)
                    .WhereElementIsNotElementType()
                    .Cast<SpatialElement>()
                    .FirstOrDefault(x => x.Number == number);

                if (conflictingRoom != null)
                {
                    //use 5 guid digits to handle _temp alraedy assigned
                    conflictingRoom.Number = number + "_TEMP_" + Guid.NewGuid().ToString().Substring(0, 5); 
                }
                room.Number = number;
            }
        }
    }
}
