using System;
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
        public (char[,], string) mapWherePlayerIsLocated()
        {
            Map map = model.game.characters.player.mapWhereIsLocated;
            char[,] charMap = new char[map.twoDArray.GetLength(0), map.twoDArray.GetLength(1)];
            List<GameObject>[,] twoDArrayt = map.twoDArray;
            int mapHeight = twoDArrayt.GetLength(0);
            int mapWidth = twoDArrayt.GetLength(1);
            Console.WriteLine(map.name);
            for (int i = 0; i < mapHeight * mapWidth; i++)
            {
                int height = (i) / mapWidth;
                int width = i - ((i / mapWidth) * mapWidth);
                List<GameObject> objects = twoDArrayt[height, width];
                GameObject mostPreferredObject = mapHelper.mostPreferredObjectInList(objects);
                char c = mostPreferredObject.symbol;
                charMap[height, width] = c;
            }
            return (charMap,map.name);
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
