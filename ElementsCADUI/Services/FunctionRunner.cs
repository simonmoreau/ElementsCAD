using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elements;
using Elements.Serialization.glTF;
using Hypar.Functions.Execution;
using Hypar.Functions.Execution.AWS;
using System.IO;
using ElementsCADUI.Models;
using System.Security.Policy;

namespace ElementsCADUI.Services
{

    class FunctionRunner
    {

        public ResultsBase RunFunction(Function function, Dictionary<string, Model> inputModels)
        {
            return LoadDll(function, inputModels);
        }

        public ResultsBase LoadDll(Function function, Dictionary<string, Model> inputModels)
        {
            string functionName = function.DllName;

            string dllPath = Path.Combine(function.Directory, functionName + ".dll");

            Init();

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

                //  = new Dictionary<string, Model>();

                object outputs = functionType.InvokeMember(
                    "Execute", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public,
                 null, null, new object[] { inputModels, inputs });

                ResultsBase resultBase = outputs as ResultsBase;

                return resultBase;
            }
            else
            {
                throw new ArgumentNullException("Could not found the output type");
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
