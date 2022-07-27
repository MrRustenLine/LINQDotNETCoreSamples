using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LINQSamples.src.Auxiliary
{
    public static class FileReader
    {
        public static string Read(string fileName)
        {
            string basePath = Environment.CurrentDirectory;
            string relativePath = "./data/";
            string fullPath = Path.GetFullPath(basePath + relativePath + fileName);
            if (File.Exists(fullPath))
            {
                return File.ReadAllText(fullPath);
            }
            else
            {
                throw new Exception(fullPath + " does not exist.");
            }
        }
    }
}
