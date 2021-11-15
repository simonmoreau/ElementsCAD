using ColorPicker.Models;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Elements.Geometry;

namespace ElementsCADUI.InputControls
{
    /// <summary>
    /// Interaction logic for ColorField.xaml
    /// </summary>
    public partial class ColorField : UserControl
    {
        public ColorField()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(Color), typeof(ColorField), new
PropertyMetadata(new Color(), new PropertyChangedCallback(OnValueChanged)));

        public Color Value
        {
            get { return (Color)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            ColorField UserControl1Control = d as ColorField;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            Color? nullableColor = e.NewValue as Color?;

            if (nullableColor != null)
            {
                Color color = nullableColor ?? new Color();
                System.Windows.Media.SolidColorBrush solidColorBrush = new System.Windows.Media.SolidColorBrush();

                solidColorBrush.Color = System.Windows.Media.Color.FromScRgb((float)color.Alpha, (float)color.Red, (float)color.Green, (float)color.Blue);

                TogglePopupButton.Background = solidColorBrush;
                // TogglePopupButton.Content = solidColorBrush.ToString();
            }

            // ClrPcker
            // TextLabel.Text = e.NewValue?.ToString();
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(InputColorField), typeof(ColorField), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputColorField Input
        {
            get { return (InputColorField)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            ColorField UserControl1Control = d as ColorField;
            UserControl1Control.OnInputChanged(e);
        }

        private void OnInputChanged(DependencyPropertyChangedEventArgs e)
        {
            InputColorField inputColorField = e.NewValue as InputColorField;

            if (inputColorField != null)
            {
                // Value = inputColorField.Value;
            }
            // TextLabel.Text = e.NewValue?.ToString();
        }

        private void main_ColorChanged(object sender, RoutedEventArgs e)
        {
            ColorRoutedEventArgs colorRoutedEventArgs = e as ColorRoutedEventArgs;

            if (colorRoutedEventArgs != null)
            {

                System.Windows.Media.Color newColor = colorRoutedEventArgs.Color;

                Value = new Color(newColor.ScR, newColor.ScG, newColor.ScB, newColor.ScA);
            }

        }
    }
}
