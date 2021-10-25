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
    /// Interaction logic for NumberSlider.xaml
    /// </summary>
    public partial class NumberSlider : UserControl
    {
        public NumberSlider()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(double), typeof(NumberSlider), new
PropertyMetadata(1.0, new PropertyChangedCallback(OnValueChanged)));

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            NumberSlider UserControl1Control = d as NumberSlider;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            double? value = e.NewValue as double?;
            // TextLabel.Text = e.NewValue? as 
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(InputNumberSlider), typeof(NumberSlider), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputNumberSlider Input
        {
            get { return (InputNumberSlider)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            NumberSlider UserControl1Control = d as NumberSlider;
            UserControl1Control.OnInputChanged(e);
        }

        private void OnInputChanged(DependencyPropertyChangedEventArgs e)
        {
            InputNumberSlider inputNumberSlider = e.NewValue as InputNumberSlider;
            if (inputNumberSlider != null)
            {
                slider.Minimum = inputNumberSlider.Minimum;
                slider.Maximum = inputNumberSlider.Maximum;
                slider.TickFrequency = inputNumberSlider.Step;
                slider.IsSnapToTickEnabled = true;

            }
            // TextLabel.Text = e.NewValue?.ToString();
        }
    }
}
