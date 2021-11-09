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
            string hyparJsonFile = args[0];

            // string hyparJsonFile = functionFolderPath + "\\hypar.json";

            hyparJsonFile = @"G:\My Drive\05 - Travail\Revit Dev\Hypar\functionDefs.json";

            // Open the hypar.json file
            // FunctionDefinition functionFile = FunctionDefinition.FromJson(File.ReadAllText(hyparJsonFile));

            List<Function>  functions = JsonConvert.DeserializeObject<List<Function>>(File.ReadAllText(hyparJsonFile), HyparRunner.Converter.Settings);
            // function.Directory = functionFolderPath;
            // function.FunctionDefinition = functionFile;

            FunctionRunner functionRunner = new FunctionRunner();

            foreach (Function function in functions)
            {
                functionRunner.RunFunction(function);
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
