using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class TxtFunctions
    {
        string mainFolderPath;
        public TxtFunctions(string mainFolderPat)
        {
            this.mainFolderPath = mainFolderPat;
        }
        public List<string> TxtLoad(string folderName, string txtFileName)
        {
            List<string> lines = new List<string>();
            string path = mainFolderPath + folderName + @"\" + txtFileName;
            using (StreamReader sr = new StreamReader(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    lines.Add(s);
                }
            }
            return lines;
        }
        
        public string[] NamesOfALLFilesOfThatTypeInFolder(string folderName, string type)
        {
            string folderPath = mainFolderPath + folderName + @"\";
            string[] jsonFiles = Directory.GetFiles(folderPath, "*" + type);
            string[] jsonFilesNames = new string[jsonFiles.Length];
            for (int i = 0; i < jsonFiles.Length; i++)
            {
                string fileName = Path.GetFileName(jsonFiles[i]);
                fileName = fileName.Replace(".json", string.Empty);
                jsonFilesNames[i] = fileName;
            }
            return jsonFilesNames;
        }
        public string SuitableName(Character character)
        {
            return character.name.Replace(" ", string.Empty);
        }
    }
}
