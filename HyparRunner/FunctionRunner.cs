using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elements;
using Elements.Serialization.glTF;
using Hypar.Functions.Execution;
using Hypar.Functions.Execution.AWS;
using System.IO;

namespace HyparRunner
{
    class FunctionRunner
    {

        public void RunFunction(Function function)
        {

            Init();

            string functionName = function.DllName; //.FunctionDefinition.Name.Replace(" ", "");

            string dllPath = Path.Combine(function.Directory, functionName + ".dll");
            string dependenciesPathNew = Path.Combine(function.Directory, functionName + ".Dependencies.dll");

            // Load function dll
            Assembly DLL = Assembly.LoadFile(dllPath);
            // Load dependencies
            string dependenciesPath = Path.GetDirectoryName(dllPath) + "\\" + Path.GetFileNameWithoutExtension(dllPath) + ".Dependencies.dll";

            Assembly dependenciesDLL = null;
            if (File.Exists(dependenciesPath))
            {
                dependenciesDLL = Assembly.LoadFile(dependenciesPath);
            }

            Type[] assemblyTypes = DLL.GetExportedTypes();

            Type functionType = assemblyTypes.Where(t => t.Name == functionName).FirstOrDefault();
            Type functionTypeOutputs = assemblyTypes.Where(t => t.Name == functionName + "Outputs").FirstOrDefault();
            Type functionTypeInputs = assemblyTypes.Where(t => t.Name == functionName + "Inputs").FirstOrDefault();


            if (dependenciesDLL != null)
            {
                Type[] dependenciesTypes = dependenciesDLL.GetExportedTypes();

                if (functionTypeOutputs == null)
                {
                    functionTypeOutputs = dependenciesTypes.Where(t => t.Name == functionName + "Outputs").FirstOrDefault();
                }

                if (functionTypeInputs == null)
                {
                    functionTypeInputs = dependenciesTypes.Where(t => t.Name == functionName + "Inputs").FirstOrDefault();
                }
            }


            if (functionType != null)
            {
                // var c = Activator.CreateInstance(functionType);
                // string bucketName = , string uploadsBucket = , Dictionary<string, string> modelInputKeys = null, string gltfKey = string elementsKey = , string ifcKey = 

                Dictionary<string, string> modelInputKeys = new Dictionary<string, string>();
                object[] s3Args = { "hypar-executions", "hypar-uploads", modelInputKeys, "model.glb", "model.json", "model.ifc" };
                object[] functionArgs = function.InputsValues.Values.ToArray();

                object[] args = new object[functionArgs.Length + s3Args.Length];
                functionArgs.CopyTo(args, 0);
                s3Args.CopyTo(args, functionArgs.Length);

                object inputs = Activator.CreateInstance(functionTypeInputs, args);

                S3Args inputsBase = inputs as S3Args;

                foreach (KeyValuePair<string, object> keyValuePair in function.InputsValues)
                {
                    PropertyInfo propertyInfo = inputs.GetType().GetProperty(keyValuePair.Key);
                    propertyInfo.SetValue(inputs, keyValuePair.Value, null);
                }

                

                Dictionary<string, Model> inputModels = new Dictionary<string, Model>();

                object outputs = functionType.InvokeMember(
                    "Execute", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public,
                 null, null, new object[] { inputModels, inputs });

                ResultsBase outputsBase = outputs as ResultsBase;


                string OUTPUT = @"G:\My Drive\05 - Travail\Revit Dev\Hypar\Output/";
                outputsBase.Model.ToGlTF(OUTPUT + "Output.glb");
            }
        }


        Dictionary<string, Assembly> assemblies;

        public void Init()
        {

            assemblies = new Dictionary<string, Assembly>();

            AppDomain.CurrentDomain.AssemblyLoad += new AssemblyLoadEventHandler(CurrentDomain_AssemblyLoad);

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {

            Assembly assembly = null;

            assemblies.TryGetValue(args.Name, out assembly);

            return assembly;

        }

        void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {

            Assembly assembly = args.LoadedAssembly;
            assemblies[assembly.FullName] = assembly;
        }
    }
}
