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
using ElementsCADUI.Models;

namespace ElementsCADUI.InputControls
{
    /// <summary>
    /// Interaction logic for IntegerField.xaml
    /// </summary>
    public partial class IntegerField : UserControl
    {
        public IntegerField()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(int), typeof(IntegerField), new
PropertyMetadata(0, new PropertyChangedCallback(OnValueChanged)));

        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            IntegerField UserControl1Control = d as IntegerField;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            // TextLabel.Text = e.NewValue?.ToString();
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(InputIntegerField), typeof(IntegerField), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputIntegerField Input
        {
            get { return (InputIntegerField)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            IntegerField UserControl1Control = d as IntegerField;
            UserControl1Control.OnInputChanged(e);
        }

        private void OnInputChanged(DependencyPropertyChangedEventArgs e)
        {
            // TextLabel.Text = e.NewValue?.ToString();
        }
    }
}
