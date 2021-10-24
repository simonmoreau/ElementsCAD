using ElementsCADUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsCADUI.Models
{
    class FunctionDisplayable : BindableBase
    {
        public FunctionDisplayable(FunctionDefinition functionDefinition, string path)
        {
            _directory = path;
            _functionDefinition = new FunctionDefinitionDisplayable(functionDefinition);

            _inputsValues = new Dictionary<string, object>();
            _inputs = new ObservableCollection<InputDisplayable>();
            foreach (InputElement inputElement in functionDefinition.Inputs)
            {
                _inputsValues.Add(inputElement.InputClass.Name.ToString(), null);
                _inputs.Add(new InputDisplayable(inputElement));
            }

            
        }



        private string _directory;
        public string Directory
        {
            get { return _directory; }
            set { SetProperty(ref _directory, value); }
        }

        private FunctionDefinitionDisplayable _functionDefinition;
        public FunctionDefinitionDisplayable FunctionDefinition
        {
            get { return _functionDefinition; }
            set { SetProperty(ref _functionDefinition, value); }
        }

        private Dictionary<string, object> _inputsValues;
        public Dictionary<string, object> InputsValues
        {
            get { return _inputsValues; }
            set { SetProperty(ref _inputsValues, value); }
        }

        private ObservableCollection<InputDisplayable> _inputs;
        public ObservableCollection<InputDisplayable> Inputs
        {
            get { return _inputs; }
            set { SetProperty(ref _inputs, value); }
        }

    }

    class InputDisplayable : BindableBase
    {
        public InputDisplayable(InputElement inputElement)
        {
            _name = inputElement.InputClass.Name.ToString();
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

    }

    class FunctionDefinitionDisplayable : BindableBase
    {
        public FunctionDefinitionDisplayable(FunctionDefinition functionDefinition)
        {

            _name = functionDefinition.Name;
            if (!string.IsNullOrEmpty(functionDefinition.DisplayName)) { _name = functionDefinition.DisplayName; }
            _description = functionDefinition.Description;

        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
    }
}
