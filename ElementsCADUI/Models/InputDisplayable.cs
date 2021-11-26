using ElementsCADUI.Services;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Elements.Geometry;

namespace ElementsCADUI.Models
{
    public class InputDisplayable : BindableBase
    {
        public InputDisplayable(InputElement inputElement, int order)
        {
            _name = inputElement.InputClass.Name.ToString();
            HyparOrder = order;
        }

        public InputDisplayable(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property)
        {
            _name = property.Key;
            _description = property.Value.Description;
            HyparOrder = property.Value.HyparOrder ?? default(int);
        }

        public int HyparOrder { get;}

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

        private object _value;
        public object Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

    }

    public class InputNumberField : InputDisplayable
    {
        public InputNumberField(InputElement inputElement, int order) : base(inputElement, order)
        {
            _minimum = inputElement.InputClass.Min ?? default(double);
            _maximum = inputElement.InputClass.Max ?? default(double);
            _step = inputElement.InputClass.Step ?? default(double);
            Value = inputElement.InputClass.Min ?? default(double);
        }

        public InputNumberField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            _minimum = property.Value.Minimum ?? default(double);
            _maximum = property.Value.Maximum ?? default(double);
            _step = property.Value.MultipleOf ?? default(double);

            double? defaultValue = property.Value.Default as double?;
            Value = defaultValue ?? property.Value.Minimum ?? default(double);
        }

        private string _hyparUnitType;
        public string HyparUnitType
        {
            get { return _hyparUnitType; }
            set { SetProperty(ref _hyparUnitType, value); }
        }

        private double _minimum;
        public double Minimum
        {
            get { return _minimum; }
            set { SetProperty(ref _minimum, value); }
        }

        private double _maximum;
        public double Maximum
        {
            get { return _maximum; }
            set { SetProperty(ref _maximum, value); }
        }

        private double _step;
        public double Step
        {
            get { return _step; }
            set { SetProperty(ref _step, value); }
        }
    }

    public class InputNumberSlider : InputDisplayable
    {
        public InputNumberSlider(InputElement inputElement, int order) : base(inputElement, order)
        {
            _minimum = inputElement.InputClass.Min ?? default(double);
            _maximum = inputElement.InputClass.Max ?? default(double);
            _step = inputElement.InputClass.Step ?? default(double);
            Value = inputElement.InputClass.Min ?? default(double);
        }

        public InputNumberSlider(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            _minimum = property.Value.Minimum ?? default(double);
            _maximum = property.Value.Maximum ?? default(double);
            _step = property.Value.MultipleOf ?? default(double);
            double? defaultValue = property.Value.Default as double?;
            Value = defaultValue ?? property.Value.Minimum ?? default(double);
        }

        private string _hyparUnitType;
        public string HyparUnitType
        {
            get { return _hyparUnitType; }
            set { SetProperty(ref _hyparUnitType, value); }
        }

        private double _minimum;
        public double Minimum
        {
            get { return _minimum; }
            set { SetProperty(ref _minimum, value); }
        }

        private double _maximum;
        public double Maximum
        {
            get { return _maximum; }
            set { SetProperty(ref _maximum, value); }
        }

        private double _step;
        public double Step
        {
            get { return _step; }
            set { SetProperty(ref _step, value); }
        }
    }

    public class InputIntegerField : InputDisplayable
    {
        public InputIntegerField(InputElement inputElement, int order) : base(inputElement, order)
        {
            _step = 1;
            Value = 0;
        }

        public InputIntegerField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            _step = 1;
            int? defaultValue = property.Value.Default as int?;
            Value = defaultValue ?? default(int);
        }

        private string _hyparUnitType;
        public string HyparUnitType
        {
            get { return _hyparUnitType; }
            set { SetProperty(ref _hyparUnitType, value); }
        }


        private int _minimum;
        public int Minimum
        {
            get { return _minimum; }
            set { SetProperty(ref _minimum, value); }
        }

        private int _maximum;
        public int Maximum
        {
            get { return _maximum; }
            set { SetProperty(ref _maximum, value); }
        }

        private int _step;
        public int Step
        {
            get { return _step; }
            set { SetProperty(ref _step, value); }
        }
    }

    public class InputBooleanToggle : InputDisplayable
    {
        public InputBooleanToggle(InputElement inputElement, int order) : base(inputElement, order)
        {
            Value = false;
        }

        public InputBooleanToggle(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            bool? defaultValue = property.Value.Default as bool?;
            Value = defaultValue ?? default(bool);
        }

    }

    public class InputStringField : InputDisplayable
    {
        public InputStringField(InputElement inputElement, int order) : base(inputElement, order)
        {
            Value = "";
        }

        public InputStringField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            Value = property.Value.Default as string;
        }
    }

    public class InputSelectField : InputDisplayable
    {
        public InputSelectField(InputElement inputElement, int order) : base(inputElement, order)
        {
            Value = "";
        }

        public InputSelectField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            Value = property.Value.Default as string;
            Choices = property.Value.Enum.Select(v => v.ToString()).ToArray();
        }

        private string[] _choices;
        public string[] Choices
        {
            get { return _choices; }
            set { SetProperty(ref _choices, value); }
        }
    }

    public class InputColorField : InputDisplayable
    {
        public InputColorField(InputElement inputElement, int order) : base(inputElement, order)
        {
            Value = new Elements.Geometry.Color();
        }

        public InputColorField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            string json = property.Value.Default.ToString();
            Elements.Geometry.Color? defaultColor = JsonConvert.DeserializeObject<Elements.Geometry.Color>(json);

            Value = defaultColor ?? new Elements.Geometry.Color();
        }
    }

    public class InputCurveField : InputDisplayable
    {
        public InputCurveField(InputElement inputElement, int order) : base(inputElement, order)
        {
            Value = null;
        }

        public InputCurveField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            Value = null;
        }
    }

    public class InputLineField : InputCurveField
    {
        public InputLineField(InputElement inputElement, int order) : base(inputElement, order)
        {
            Value = new Elements.Geometry.Line();
        }

        public InputLineField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            if (property.Value.Default != null)
            {
                string json = property.Value.Default.ToString();
                Elements.Geometry.Line defaultLine = JsonConvert.DeserializeObject<Elements.Geometry.Line>(json);

                Value = defaultLine ?? new Elements.Geometry.Line();
            }
            else
            {
                Value = new Elements.Geometry.Line();
            }
        }
    }

    public class InputPolylineField : InputCurveField
    {
        public InputPolylineField(InputElement inputElement, int order) : base(inputElement, order)
        {
            Value = null; // new Elements.Geometry.Polyline();
        }

        public InputPolylineField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            if (property.Value.Default != null)
            {
                string json = property.Value.Default.ToString();
                Elements.Geometry.Polyline defaultLine = JsonConvert.DeserializeObject<Elements.Geometry.Polyline>(json);

                Value = defaultLine;
            }
            else
            {
                Value = null; // new Elements.Geometry.Polyline();
            }
        }
    }

    public class InputPolygonField : InputCurveField
    {
        public InputPolygonField(InputElement inputElement, int order) : base(inputElement, order)
        {
            Value = null;
        }

        public InputPolygonField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            if (property.Value.Default != null)
            {
                string json = property.Value.Default.ToString();
                Elements.Geometry.Polygon defaultLine = JsonConvert.DeserializeObject<Elements.Geometry.Polygon>(json);

                Value = defaultLine;
            }
            else
            {
                Value = null;// new Elements.Geometry.Polygon();
            }
        }
    }

    public class InputPointField : InputDisplayable
    {
        public InputPointField(InputElement inputElement, int order) : base(inputElement, order)
        {
            Value = new Elements.Geometry.Vector3();
        }

        public InputPointField(KeyValuePair<string, HyparFunctionInputSchemaMetaSchemaValue> property) : base(property)
        {
            if (property.Value.Default != null)
            {
                string json = property.Value.Default.ToString();
                Elements.Geometry.Vector3 defaultLine = JsonConvert.DeserializeObject<Elements.Geometry.Vector3>(json);

                if (defaultLine != null)
                {
                    Value = defaultLine;
                }
                else
                {
                    Value = new Elements.Geometry.Vector3();
                }
            }
            else
            {
                Value = new Elements.Geometry.Vector3();
            }
        }
    }
}
