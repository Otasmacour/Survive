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
    }
}