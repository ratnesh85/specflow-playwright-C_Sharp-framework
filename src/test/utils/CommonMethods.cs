using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowPlaywrightFramework.src.test.utils
{
    public class CommonMethods
    {
        //get projectDirectoryPath
        public static string GetCurrentProjectDirectory()
        {
            string pth = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            return projectPath;
        }

        //this method removes specified index from string
        public static string RemoveChar(string str, int n)
        {
            return str.Remove(n, 1);
        }

        public Dictionary<string, string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }
    }
}
