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
    /// Interaction logic for BooleanToggle.xaml
    /// </summary>
    public partial class BooleanToggle : UserControl
    {
        public BooleanToggle()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(bool), typeof(BooleanToggle), new
PropertyMetadata(true, new PropertyChangedCallback(OnValueChanged)));

        public bool Value
        {
            get { return (bool)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            BooleanToggle UserControl1Control = d as BooleanToggle;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            bool? value = e.NewValue as bool?;
            // TextLabel.Text = e.NewValue? as 
            checkbox.IsChecked = value;
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(InputBooleanToggle), typeof(BooleanToggle), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputBooleanToggle Input
        {
            get { return (InputBooleanToggle)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            BooleanToggle UserControl1Control = d as BooleanToggle;
            UserControl1Control.OnInputChanged(e);
        }

        private void OnInputChanged(DependencyPropertyChangedEventArgs e)
        {
            InputBooleanToggle inputBooleanToggle = e.NewValue as InputBooleanToggle;
            if (inputBooleanToggle != null)
            {
                
            }
            // TextLabel.Text = e.NewValue?.ToString();
        }
    }
}
