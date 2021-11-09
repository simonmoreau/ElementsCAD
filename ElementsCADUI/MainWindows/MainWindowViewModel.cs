﻿using ElementsCADUI.Services;
using ElementsCADUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace ElementsCADUI.MainWindows
{
    class MainWindowViewModel : BindableBase
    {
        private MainWindows.ErrorViewModel _errorViewModel;

        public MainWindowViewModel(MainWindows.ErrorViewModel errorViewModel)
        {
            _errorViewModel = errorViewModel;
            _functions = new ObservableCollection<FunctionDisplayable>();
            AddFunctionCommand = new RelayCommand(OnAddFunctionCommand);
            RemoveFunctionCommand = new RelayCommand<FunctionDisplayable>(OnRemoveFunctionCommand);
            RunFunctionsCommand = new RelayCommand(OnRunFunctionsCommand);
        }

        public RelayCommand RunFunctionsCommand { get; private set; }

        private void OnRunFunctionsCommand()
        {
            List<Function> functions = new List<Function>();
            foreach (FunctionDisplayable functionDisplayable in Functions)
            {
                Function function = new Function();
                function.Directory = functionDisplayable.Directory;
                function.DllName = functionDisplayable.FunctionDefinition.Name.Replace(" ", "");
                function.InputsValues = new Dictionary<string, object>();

                foreach (InputDisplayable inputDisplayable in functionDisplayable.Inputs)
                {
                    function.InputsValues.Add(inputDisplayable.Name, inputDisplayable.Value);
                }

                functions.Add(function);
            }

            string json = JsonConvert.SerializeObject(functions);

            string path = @"G:\My Drive\05 - Travail\Revit Dev\Hypar\functionDefs.json";
            File.WriteAllText(path, json);
        }

        public RelayCommand AddFunctionCommand { get; private set; }

        private void OnAddFunctionCommand()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"G:\My Drive\05 - Travail\Revit Dev\Hypar\HyparJson";
            if (openFileDialog.ShowDialog() == true)
            {
                FunctionDefinition functionDefinition = FunctionDefinition.FromJson(File.ReadAllText(openFileDialog.FileName));

                string directoryPath = Path.GetDirectoryName(openFileDialog.FileName);
                Functions.Add(new FunctionDisplayable(functionDefinition, directoryPath));
            }
        }

        public RelayCommand<FunctionDisplayable> RemoveFunctionCommand { get; private set; }

        private void OnRemoveFunctionCommand(FunctionDisplayable functionDisplayable)
        {
            Functions.Remove(functionDisplayable);
        }

        private ObservableCollection<FunctionDisplayable> _functions;
        public ObservableCollection<FunctionDisplayable> Functions
        {
            get { return _functions; }
            set { SetProperty(ref _functions, value); }
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
