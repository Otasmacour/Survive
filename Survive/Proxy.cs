using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Survive
{
    static class Proxy
    {
        public static OutputType outputType;
        static char currentKey;
        private static bool _keyAvaible;
        public static void Set()
        {
            Assembly assembly = Assembly.GetEntryAssembly();
            Type type = assembly.EntryPoint.DeclaringType;
        }
        public static bool KeyAvaible {
            get
            {
                if(outputType == OutputType.Console)
                {
                    return Console.KeyAvailable;
                }
                else
                {
                    return _keyAvaible;
                }
            }
        }
        public static char ReadKey()
        {
            if (outputType == OutputType.Console)
            {
                return Console.ReadKey(intercept: true).KeyChar;
            }
            else { 
                _keyAvaible = false;
                return currentKey; }
        }
        public static void SetCurrentKey(char c)
        {
            currentKey = c;
            _keyAvaible = true;
        }
    }
}