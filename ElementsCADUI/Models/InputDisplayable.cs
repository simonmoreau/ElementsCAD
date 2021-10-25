using ElementsCADUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsCADUI.Models
{
    public class InputDisplayable : BindableBase
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

    public class InputNumberField : InputDisplayable
    {
        public InputNumberField(InputElement inputElement) : base(inputElement)
        {
            _value = 0;
        }

        private string _hyparUnitType;
        public string HyparUnitType
        {
            get { return _hyparUnitType; }
            set { SetProperty(ref _hyparUnitType, value); }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
    }

    public class InputNumberSlider : InputDisplayable
    {
        public InputNumberSlider(InputElement inputElement) : base(inputElement)
        {
            _minimum = inputElement.InputClass.Min ?? default(double);
            _maximum = inputElement.InputClass.Max ?? default(double);
            _step = inputElement.InputClass.Step ?? default(double);
            _value = inputElement.InputClass.Min ?? default(double);
        }

        private string _hyparUnitType;
        public string HyparUnitType
        {
            get { return _hyparUnitType; }
            set { SetProperty(ref _hyparUnitType, value); }
        }

        private double _value;
        public double Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
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
        public InputIntegerField(InputElement inputElement) : base(inputElement)
        {
            _value = 0;
        }

        private int _value;
        public int Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        private string _hyparUnitType;
        public string HyparUnitType
        {
            get { return _hyparUnitType; }
            set { SetProperty(ref _hyparUnitType, value); }
        }
    }

    class InputBooleanToggle : InputDisplayable
    {
        public InputBooleanToggle(InputElement inputElement) : base(inputElement)
        {
            _value = false;
        }

        private bool _value;
        public bool Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
    }

    class InputStringField : InputDisplayable
    {
        public InputStringField(InputElement inputElement) : base(inputElement)
        {
            _value = "";
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
    }

    class InputSelectDropdown : InputDisplayable
    {
        public InputSelectDropdown(InputElement inputElement) : base(inputElement)
        {
            _value = "";
        }

        private string _value;
        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        private string[] _choices;
        public string[] Choices
        {
            get { return _choices; }
            set { SetProperty(ref _choices, value); }
        }
    }
}
