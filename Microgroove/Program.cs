using Microgroove.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microgroove
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Utility util = new Utility();

            List<List<string>> lines = util.ReadAllLinesIntoAList(args[0]);

            string json = util.ProcessLines(lines);
            
            Console.Write(json);
            Console.ReadKey();
        }

        

    }
}
