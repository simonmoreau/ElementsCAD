using ElementsCADUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElementsCADUI.InputControls
{
    /// <summary>
    /// Interaction logic for NumberField.xaml
    /// </summary>
    public partial class NumberField : UserControl
    {
        public NumberField()
        {
            InitializeComponent();
            SetBinding();
        }

        private void SetBinding()
        {
            Binding myBinding = new Binding();
            myBinding.Source = this;
            myBinding.Path = new PropertyPath("Value");
            NumberRangeRule numberRangeRule = new NumberRangeRule();
            //numberRangeRule.Min = Input?.Minimum ?? default(double);
            //numberRangeRule.Max = Input?.Maximum ?? default(double);
            myBinding.ValidationRules.Add(numberRangeRule);
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(textBox, TextBox.TextProperty, myBinding);
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(double), typeof(NumberField), new
PropertyMetadata(0.0, new PropertyChangedCallback(OnValueChanged)));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            NumberField UserControl1Control = d as NumberField;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            // TextLabel.Text = e.NewValue?.ToString();
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(InputNumberField), typeof(NumberField), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputNumberField Input
        {
            get { return (InputNumberField)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            NumberField UserControl1Control = d as NumberField;
            UserControl1Control.OnInputChanged(e);
        }

        private void OnInputChanged(DependencyPropertyChangedEventArgs e)
        {
            InputNumberField inputNumberField = e.NewValue as InputNumberField;

            if (inputNumberField != null)
            {
                SetBinding();
            }
            // TextLabel.Text = e.NewValue?.ToString();
        }
    }
}
