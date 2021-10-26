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

            if (functionDefinition.InputSchema != null)
            {
                foreach (KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property in functionDefinition.InputSchema.Properties)
                {
                    HyparFunctionInputSchemaMetaSchemaValue value = property.Value;

                    if (value.HyparStyle == HyparStyle.Number)
                    {
                        _inputs.Add(new InputNumberField(property));
                    }
                    else if (value.HyparStyle == HyparStyle.Standard)
                    {

                    }
                    else if (value.HyparStyle == HyparStyle.Matrix)
                    {

                    }
                    else if (value.HyparStyle == HyparStyle.Row)
                    {

                    }
                    else
                    {
                        if (value.Type.HasValue)
                        {
                            switch (value.Type.Value.Enum)
                            {
                                case SimpleTypes.Array:
                                    break;
                                case SimpleTypes.Boolean:
                                        _inputs.Add(new InputBooleanToggle(property));
                                    break;
                                case SimpleTypes.Integer:
                                    break;
                                case SimpleTypes.Null:
                                    break;
                                case SimpleTypes.Number:
                                    _inputs.Add(new InputNumberSlider(property));
                                    break;
                                case SimpleTypes.Object:
                                    break;
                                case SimpleTypes.String:
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    
                    //typeUnion.Enum
                    //switch ()
                    //{
                    //    default:
                    //        break;
                    //}
                }
            }

            if (functionDefinition.Inputs != null)
            {
                int order = 0;
                foreach (InputElement inputElement in functionDefinition.Inputs)
                {

                    _inputsValues.Add(inputElement.InputClass.Name.ToString(), null);

                    switch (inputElement.InputClass.Type)
                    {
                        case InputType.Boolean:
                            _inputs.Add(new InputBooleanToggle(inputElement, order));
                            break;
                        case InputType.Choice:
                            _inputs.Add(new InputDisplayable(inputElement, order));
                            break;
                        case InputType.Data:
                            _inputs.Add(new InputDisplayable(inputElement, order));
                            break;
                        case InputType.Geometry:
                            _inputs.Add(new InputDisplayable(inputElement, order));
                            break;
                        case InputType.List:
                            _inputs.Add(new InputDisplayable(inputElement, order));
                            break;
                        case InputType.Location:
                            _inputs.Add(new InputDisplayable(inputElement, order));
                            break;
                        case InputType.Number:
                            _inputs.Add(new InputNumberField(inputElement, order));
                            break;
                        case InputType.Range:
                            _inputs.Add(new InputNumberSlider(inputElement, order));
                            break;
                        case InputType.String:
                            _inputs.Add(new InputStringField(inputElement, order));
                            break;
                        default:
                            _inputs.Add(new InputDisplayable(inputElement, order));
                            break;
                    }

                    order++;
                }
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
