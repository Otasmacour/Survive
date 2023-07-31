using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class JsonFunctions
    {
        string mainFolderPath;
        DataIOManager dataIOManager;
        TxtFunctions txtFunctions;
        public JsonFunctions(string mainFolderPath, DataIOManager dataIOManager, TxtFunctions txtFunctions)
        {
            this.mainFolderPath = mainFolderPath;
            this.dataIOManager = dataIOManager;
            this.txtFunctions = txtFunctions;
        }
        public string JsonFileLoad(string folderName, string jsonFileName)
        {
            string path = mainFolderPath + folderName + @"\" + jsonFileName + ".json";
            string json = File.ReadAllText(path, Encoding.GetEncoding(1250));
            byte[] bytes = Encoding.Default.GetBytes(json);
            json = Encoding.UTF8.GetString(bytes);
            return json;
        }
        class CharacterForJsonCreate
        {
            public string name;
            public string rank;
        }
        public void JsonForCharacterCreate(Character character1)
        {
            CharacterForJsonCreate character = new CharacterForJsonCreate();
            character.name = character1.name;
            string json = JsonConvert.SerializeObject(character, Formatting.Indented);
            string folderPath = mainFolderPath + "JsonCharacters" + @"\";
            string filePath = $"{folderPath}{txtFunctions.SuitableName(character1)}.json";
            File.WriteAllText(filePath, json);
        }
        //public void JsonForTraitsCreate(string suitableName)
        //{
        //    Traits traits = new Traits();
        //    traits.Courage = 50;
        //    traits.Intelligence = 50;
        //    traits.Charisma = 50;
        //    traits.Loyalty = 50;
        //    traits.Aggression = 50;
        //    traits.Empathy = 50;
        //    traits.Perseverance = 50;
        //    traits.Curiosity = 50;
        //    traits.Wit = 50;
        //    traits.Honesty = 50;
        //    string json = JsonConvert.SerializeObject(traits, Formatting.Indented);
        //    string folderPath = mainFolderPath + "CharactersTraits" + @"\";
        //    string filePath = $"{folderPath}{suitableName}.json";
        //    File.WriteAllText(filePath, json);
        //}
        public Character CharacterLoadFromJson(string folderName, string jsonFileName)
        {
            string json = JsonFileLoad(folderName, jsonFileName);
            Character character = JsonConvert.DeserializeObject<Character>(json);
            return character;
        }
        //public Traits TraitsLoadFromJson(string folderName, string jsonFileName)
        //{
        //    string json = JsonFileLoad(folderName, jsonFileName);
        //    Traits traits = JsonConvert.DeserializeObject<Traits>(json);
        //    return traits;
        //}
        //public List<Character> AllCharactersCreateFromJsonFiles(string folderName)
        //{
        //    List<Character> characters = new List<Character>();
        //    string[] jsonFilesNames = txtFunctions.NamesOfALLFilesInFolderOfThatType(folderName, "json");
        //    foreach (string jsonFileName in jsonFilesNames)
        //    {
        //        Character character = CharacterLoadFromJson(folderName, jsonFileName);
        //        characters.Add(character);
        //    }
        //    return characters;
        //}
    }
}
