using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElementsCADUI.InputControls
{
    class NumberRangeRule : ValidationRule
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public NumberRangeRule()
        {
        }


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double number = 0;

            try
            {
                if (((string)value).Length > 0)
                    number = double.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if ((number < Min) || (number > Max))
            {
                return new ValidationResult(false,
                  $"Please enter a number in the range: {Min}-{Max}.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
