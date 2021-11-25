using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HyparRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string json = args[0];
                Console.WriteLine(json);
                // string hyparJsonFile = functionFolderPath + "\\hypar.json";

                // hyparJsonFile = @"G:\My Drive\05 - Travail\Revit Dev\Hypar\functionDefs.json";

                // Open the hypar.json file
                // FunctionDefinition functionFile = FunctionDefinition.FromJson(File.ReadAllText(hyparJsonFile));

                List<Function> functions = JsonConvert.DeserializeObject<List<Function>>(json, HyparRunner.Converter.Settings);

                if (functions.Count == 0) { throw new Exception("No functions where found"); }
                // function.Directory = functionFolderPath;
                // function.FunctionDefinition = functionFile;

                FunctionRunner functionRunner = new FunctionRunner();

                foreach (Function function in functions)
                {
                    functionRunner.RunFunction(function);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }


        }
    }

    class Function
    {
        public string Directory { get; set; }
        public string DllName { get; set; }
        // public FunctionDefinition FunctionDefinition { get; set; }
        public Dictionary<string, object> InputsValues { get; set; }
    }

}
