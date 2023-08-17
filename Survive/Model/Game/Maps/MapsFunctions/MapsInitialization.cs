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
        MapsCleaning mapsCleaning = new MapsCleaning();
        public MapsInitialization(DataIOManager dataIOManager, RoomMapCollection roomMapCollection)
        {
            this.dataIOManager = dataIOManager;
            this.roomMapCollection = roomMapCollection;
            mapsLinking = new MapsLinking(dataIOManager);
            this.mapsLinking.LinkingMaps(roomMapCollection, this);
            this.mapsCleaning.RemovingOfUnusedDoors(roomMapCollection);
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
                GameObject element = new Wall();
                Coordinates coordinates = new Coordinates(); Coordinates transitionCoordinates = new Coordinates(); coordinates.y = height; coordinates.x = width;
                switch (c)
                {
                    case '.':
                        element = new Floor();
                        break;
                    case 'x':
                        element = new Wall();
                        break;
                    case 'b':
                        element = new Bed();
                        mapInformations.mapLayout.furnitureCoordinates.Add(coordinates);
                        break;
                    case 'c':
                        element = new Closet();
                        mapInformations.mapLayout.furnitureCoordinates.Add(coordinates);
                        break;
                    case 'd':
                        element = new Door();
                        
                        if (height == 0)
                        {
                            mapInformations.mapLayout.doorCoordinates.Add(Direction.Up, coordinates);
                            mapInformations.mapLayout.doors.Add(Direction.Up, (Door)element);
                            transitionCoordinates.y = 1;
                            transitionCoordinates.x = width;
                            mapInformations.mapLayout.transitionsCoordinates.Add(Direction.Up, transitionCoordinates);
                        }
                        else if (width == 0)
                        {
                            mapInformations.mapLayout.doorCoordinates.Add(Direction.Left, coordinates);
                            mapInformations.mapLayout.doors.Add(Direction.Left, (Door)element);
                            transitionCoordinates.y = height;
                            transitionCoordinates.x = 1;
                            mapInformations.mapLayout.transitionsCoordinates.Add(Direction.Left, transitionCoordinates);
                        }
                        else if (width == mapWidth - 1)
                        {
                            mapInformations.mapLayout.doorCoordinates.Add(Direction.Right, coordinates);
                            mapInformations.mapLayout.doors.Add(Direction.Right, (Door)element);
                            transitionCoordinates.y = height;
                            transitionCoordinates.x = mapWidth - 2;
                            mapInformations.mapLayout.transitionsCoordinates.Add(Direction.Right, transitionCoordinates);
                        }
                        else if (height == mapHeight - 1)
                        {
                            mapInformations.mapLayout.doorCoordinates.Add(Direction.Down, coordinates);
                            mapInformations.mapLayout.doors.Add(Direction.Down, (Door)element);
                            transitionCoordinates.y = height - 1;
                            transitionCoordinates.x = width;
                            mapInformations.mapLayout.transitionsCoordinates.Add(Direction.Down, transitionCoordinates);
                        }
                        mapInformations.mapLayout.occupiedPlaces.Add(transitionCoordinates);
                        break;
                    default:
                        // If the game designer is capable, this won't happen
                        break;
                }
                twoDArray[height, width] = new List<GameObject>{element};
            }
            return (twoDArray,mapInformations);
        }
    }
}