using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    class DataIOManager
    {
        public string mainFolderPath;
        public TxtFunctions txtFunctions;
        public JsonFunctions jsonFunctions;
        public EnumFunctions enumFunctions;
        public DataIOManager()
        {
            mainFolderPath = Directory.GetParent(Environment.CurrentDirectory)?.Parent?.FullName + @"\Source\";
            txtFunctions = new TxtFunctions(mainFolderPath);
            jsonFunctions = new JsonFunctions(mainFolderPath, this, txtFunctions);
            enumFunctions = new EnumFunctions();
        }
    }
}
