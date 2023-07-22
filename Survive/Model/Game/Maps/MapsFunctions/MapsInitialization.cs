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
        MapsLinking mapsLinking = new MapsLinking();
        public MapsInitialization(DataIOManager dataIOManager, RoomMapCollection roomMapCollection)
        {
            this.dataIOManager = dataIOManager;
            this.roomMapCollection = roomMapCollection;
            this.mapsLinking.LinkingMaps(roomMapCollection, this);
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
                        mapInformations.upperDoor = coordinates;
                        transitionCoordinates.y = 1;
                        transitionCoordinates.x = width;
                        mapInformations.upperTransition = transitionCoordinates;
                    }
                    else if (width == 0)
                    {
                        mapInformations.leftDoor = coordinates;
                        transitionCoordinates.y = height;
                        transitionCoordinates.x = 1;
                        mapInformations.leftTransition = transitionCoordinates;
                    }
                    else if (width == mapWidth - 1)
                    {
                        mapInformations.rightDoor = coordinates;
                        transitionCoordinates.y = height;
                        transitionCoordinates.x = mapWidth - 2;
                        mapInformations.rightTransition = transitionCoordinates;
                    }
                    else if (height == mapHeight - 1)
                    {
                        mapInformations.bottomDoor = coordinates;
                        transitionCoordinates.y = height - 1;
                        transitionCoordinates.x = width;
                        mapInformations.bottomTransition = transitionCoordinates;
                    }
                    mapInformations.occupiedPlaces.Add(transitionCoordinates);
                }
                twoDArray[height, width] = objekty;
            }
            return (twoDArray,mapInformations);
        }
    }
}

