using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class MapsInitialization
    {
        string folderName = "Maps";
        DataIOManager dataIOManager;
        public RoomMapCollection roomMapCollection;
        MapsLinking mapsLinking;
        MapCleaning mapCleaning = new MapCleaning();
        public MapsInitialization(DataIOManager dataIOManager, RoomMapCollection roomMapCollection)
        {
            this.dataIOManager = dataIOManager;
            this.roomMapCollection = roomMapCollection;
            mapsLinking = new MapsLinking(dataIOManager);
            this.mapsLinking.LinkingMaps(roomMapCollection, this);
            this.mapCleaning.RemovingOfUnusedDoors(roomMapCollection);
        }
        public (List<GameObject>[,] twoDArray, MapInformations mapInformations) CreatingTwoDArrayPlusInformationsOfIt(string txtFileName)
        {
            List<string> lines = dataIOManager.txtFunctions.TxtLoad(folderName, txtFileName+".txt");
            List<GameObject>[,] twoDArray = new List<GameObject>[lines.Count, lines[0].Length];
            MapInformations mapInformations = new MapInformations();
            int mapHeight = lines.Count();
            int mapWidth = lines[0].Length;
            for (int i = 0; i < mapHeight * mapWidth; i++)
            {
                int height = (i) / mapWidth;
                int width = i - ((i / mapWidth) * mapWidth);
                char c = lines[height][width];
                List<GameObject> objekty = new List<GameObject>();
                GameObject objekt;
                if (c == '.')
                {
                    objekt = new Floor();
                    objekty.Add(objekt);
                }
                else if (c == 'x')
                {
                    objekt = new Wall();
                    objekty.Add(objekt);
                }
                else if (c == 'd')
                {
                    objekt = new Door();
                    objekty.Add(objekt);
                    Coordinates coordinates = new Coordinates();
                    Coordinates transitionCoordinates = new Coordinates();
                    coordinates.y = height;
                    coordinates.x = width;
                    if (height == 0)
                    {
                        mapInformations.mapLayout.doorCoordinates.Add(Direction.Up,coordinates);
                        mapInformations.mapLayout.doors.Add(Direction.Up, (Door)objekt);
                        transitionCoordinates.y = 1;
                        transitionCoordinates.x = width;
                        mapInformations.mapLayout.transitionsCoordinates.Add(Direction.Up,transitionCoordinates);
                    }
                    else if (width == 0)
                    {
                        mapInformations.mapLayout.doorCoordinates.Add(Direction.Left, coordinates);
                        mapInformations.mapLayout.doors.Add(Direction.Left, (Door)objekt);
                        transitionCoordinates.y = height;
                        transitionCoordinates.x = 1;
                        mapInformations.mapLayout.transitionsCoordinates.Add(Direction.Left, transitionCoordinates);
                    }
                    else if (width == mapWidth - 1)
                    {
                        mapInformations.mapLayout.doorCoordinates.Add(Direction.Right, coordinates);
                        mapInformations.mapLayout.doors.Add(Direction.Right, (Door)objekt);
                        transitionCoordinates.y = height;
                        transitionCoordinates.x = mapWidth - 2;
                        mapInformations.mapLayout.transitionsCoordinates.Add(Direction.Right, transitionCoordinates);
                    }
                    else if (height == mapHeight - 1)
                    {
                        mapInformations.mapLayout.doorCoordinates.Add(Direction.Down, coordinates);
                        mapInformations.mapLayout.doors.Add(Direction.Down, (Door)objekt);
                        transitionCoordinates.y = height - 1;
                        transitionCoordinates.x = width;
                        mapInformations.mapLayout.transitionsCoordinates.Add(Direction.Down, transitionCoordinates);
                    }
                    mapInformations.mapLayout.occupiedPlaces.Add(transitionCoordinates);
                }
                twoDArray[height, width] = objekty;
            }
            return (twoDArray,mapInformations);
        }
    }
}