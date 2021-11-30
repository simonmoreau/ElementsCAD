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
                    _inputs.Add(GetInputFromInputSchema(property));
                }
            }

            if (functionDefinition.Inputs != null)
            {
                int order = 0;
                
                foreach (InputElement inputElement in functionDefinition.Inputs)
                {

                    _inputsValues.Add(inputElement.InputClass.Name.ToString(), null);

                    _inputs.Add(GetInputFromInputElement(inputElement, order));

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

        public static InputDisplayable GetInputFromInputElement(InputElement inputElement, int order)
        {
            switch (inputElement.InputClass.Type)
            {
                case InputType.Boolean:
                    return new InputBooleanToggle(inputElement, order);
                case InputType.Choice:
                    return new InputDisplayable(inputElement, order);
                case InputType.Data:
                    return new InputDisplayable(inputElement, order);
                case InputType.Geometry:
                    switch (inputElement.InputClass.PrimitiveType)
                    {
                        case PrimitiveType.Polygon:
                            return new InputPolygonField(inputElement, order);
                        case PrimitiveType.Polyline:
                            return new InputPolylineField(inputElement, order);
                        default:
                            return new InputDisplayable(inputElement, order);
                    }
                case InputType.List:
                    return new InputDisplayable(inputElement, order);
                case InputType.Location:
                    return new InputDisplayable(inputElement, order);
                case InputType.Number:
                    return new InputNumberField(inputElement, order);
                case InputType.Range:
                    return new InputNumberSlider(inputElement, order);
                case InputType.String:
                    return new InputStringField(inputElement, order);
                default:
                    return new InputDisplayable(inputElement, order);
            }
        }

        public static InputDisplayable GetInputFromInputSchema(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property)
        {
            HyparFunctionInputSchemaMetaSchemaValue value = property.Value;

            if (value.HyparStyle == HyparStyle.Number)
            {
                return new InputNumberField(property);
            }
            else if (value.HyparStyle == HyparStyle.Standard)
            {
                return new InputDisplayable(property);
            }
            else if (value.HyparStyle == HyparStyle.Matrix)
            {
                return new InputDisplayable(property);
            }
            else if (value.HyparStyle == HyparStyle.Row)
            {
                return new InputDisplayable(property);
            }
            else
            {
                if (value.Type.HasValue)
                {
                    switch (value.Type.Value.Enum)
                    {
                        case SimpleTypes.Array:
                            return new InputListField(property);
                        case SimpleTypes.Boolean:
                            return new InputBooleanToggle(property);
                        case SimpleTypes.Integer:
                            return new InputIntegerField(property);
                        case SimpleTypes.Null:
                            return new InputDisplayable(property);
                        case SimpleTypes.Number:
                            return new InputNumberSlider(property);
                        case SimpleTypes.Object:
                            return new InputDisplayable(property);
                        case SimpleTypes.String:
                            if (value.Enum != null)
                            {
                                return new InputSelectField(property);
                            }
                            else
                            {
                                return new InputStringField(property);
                            }
                        default:
                            return new InputDisplayable(property);
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
                                return new InputPointField(property);
                            case "Line.json":
                                return new InputLineField(property);
                            case "Polyline.json":
                                return new InputPolylineField(property);
                            case "Polygon.json":
                                return new InputPolygonField(property);
                            case "Color.json":
                                return new InputColorField(property);
                            default:
                                return new InputDisplayable(property);
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException("Ref is null");
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
