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
                Dictionary<string, HyparFunctionInputSchemaMetaSchemaValue> properties = functionDefinition.InputSchema.Properties.OrderBy(i => i.Value.HyparOrder).ToDictionary(pair => pair.Key, pair => pair.Value);

                foreach (KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property in properties)
                {
                    AddInputFromInputSchema(property);
                }
            }

            if (functionDefinition.Inputs != null)
            {
                int order = 0;
                
                foreach (InputElement inputElement in functionDefinition.Inputs)
                {

                    _inputsValues.Add(inputElement.InputClass.Name.ToString(), null);

                    AddInputFromInputElement(inputElement, order);

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

        private void AddInputFromInputElement(InputElement inputElement, int order)
        {
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
                    switch (inputElement.InputClass.PrimitiveType)
                    {
                        case PrimitiveType.Polygon:
                            _inputs.Add(new InputPolygonField(inputElement, order));
                            break;
                        case PrimitiveType.Polyline:
                            _inputs.Add(new InputPolylineField(inputElement, order));
                            break;
                        default:
                            break;
                    }
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
        }

        private void AddInputFromInputSchema(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property)
        {
            HyparFunctionInputSchemaMetaSchemaValue value = property.Value;

            if (value.HyparStyle == HyparStyle.Number)
            {
                _inputs.Add(new InputNumberField(property));
            }
            else if (value.HyparStyle == HyparStyle.Standard)
            {
                _inputs.Add(new InputDisplayable(property));
            }
            else if (value.HyparStyle == HyparStyle.Matrix)
            {
                _inputs.Add(new InputDisplayable(property));
            }
            else if (value.HyparStyle == HyparStyle.Row)
            {
                _inputs.Add(new InputDisplayable(property));
            }
            else
            {
                if (value.Type.HasValue)
                {
                    switch (value.Type.Value.Enum)
                    {
                        case SimpleTypes.Array:
                            _inputs.Add(new InputDisplayable(property));
                            break;
                        case SimpleTypes.Boolean:
                            _inputs.Add(new InputBooleanToggle(property));
                            break;
                        case SimpleTypes.Integer:
                            _inputs.Add(new InputIntegerField(property));
                            break;
                        case SimpleTypes.Null:
                            _inputs.Add(new InputDisplayable(property));
                            break;
                        case SimpleTypes.Number:
                            _inputs.Add(new InputNumberSlider(property));
                            break;
                        case SimpleTypes.Object:
                            _inputs.Add(new InputDisplayable(property));
                            break;
                        case SimpleTypes.String:
                            if (value.Enum != null)
                            {
                                _inputs.Add(new InputSelectField(property));
                            }
                            else
                            {
                                _inputs.Add(new InputStringField(property));
                            }
                            break;
                        default:
                            _inputs.Add(new InputDisplayable(property));
                            break;
                    }
                }
                else
                {
                    if (value.Ref != null)
                    {
                        string refSchema = value.Ref.Split('/').Last();
                        switch (refSchema)
                        {
                            case "Vector3.json":
                                _inputs.Add(new InputPointField(property));
                                break;
                            case "Line.json":
                                _inputs.Add(new InputLineField(property));
                                break;
                            case "Polyline.json":
                                _inputs.Add(new InputPolylineField(property));
                                break;
                            case "Polygon.json":
                                _inputs.Add(new InputPolygonField(property));
                                break;
                            case "Color.json":
                                _inputs.Add(new InputColorField(property));
                                break;
                            default:
                                _inputs.Add(new InputDisplayable(property));
                                break;
                        }
                    }
                }
            }
        }
    }

    class FunctionDefinitionDisplayable : BindableBase
    {
        public FunctionDefinitionDisplayable(FunctionDefinition functionDefinition)
        {
            Definition = functionDefinition;
            _name = functionDefinition.Name;
            if (!string.IsNullOrEmpty(functionDefinition.DisplayName)) { _name = functionDefinition.DisplayName; }
            _description = functionDefinition.Description;

        }

        public FunctionDefinition Definition { get; set; }

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
