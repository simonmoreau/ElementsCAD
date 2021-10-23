using System;
using System.IO;
using System.Collections.Generic;

namespace HyparRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            string functionFolderPath = args[0];

            string hyparJsonFile = functionFolderPath + "\\hypar.json";

            // Open the hypar.json file
            FunctionDefinition functionFile = FunctionDefinition.FromJson(File.ReadAllText(hyparJsonFile));

            Function function = new Function();
            function.Directory = functionFolderPath;
            function.FunctionDefinition = functionFile;

            FunctionRunner functionRunner = new FunctionRunner();
            functionRunner.RunFunction(function);

        }
    }

    class Function
    {
        public string Directory { get; set; }
        public FunctionDefinition FunctionDefinition { get; set; }
        public Dictionary<string, object> InputsValues { get; set; }
    }

}
