﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class ReturningFunctionsForView
    {
        Model model;
        MapHelper mapHelper;
        public ReturningFunctionsForView(MapHelper mapHelper, Model model)
        {
            this.model = model;
            this.mapHelper = mapHelper;
        }
        public (char[,] map, int floorNumber, string mapName) mapWherePlayerIsLocated()
        {
            Map map = model.game.characters.player.mapWhereIsLocated;
            char[,] charMap = new char[map.twoDArray.GetLength(0), map.twoDArray.GetLength(1)];
            List<GameObject>[,] twoDArrayt = map.twoDArray;
            int mapHeight = twoDArrayt.GetLength(0);
            int mapWidth = twoDArrayt.GetLength(1);
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    List<GameObject> objects = twoDArrayt[y, x];
                    GameObject mostPreferredObject = mapHelper.mostPreferredObjectInList(objects);
                    char c = mostPreferredObject.symbol;
                    charMap[y, x] = c;
                }
            }
            return (charMap, map.mapInformations.floorNumber, map.name);
        }
        public List<(char, string)> CharactersOnMapWherePlayerIsLocated()
        {
            List<(char, string)> lines = new List<(char, string)>();
            Character player = model.game.characters.player;
            List<Character> characters = player.mapWhereIsLocated.charactersOnMap.OrderBy(Postava => Postava.symbol).ToList();
            foreach (Character character in characters)
            {
                lines.Add((character.symbol, character.name));
            }
            return lines;
        }
    }
}
