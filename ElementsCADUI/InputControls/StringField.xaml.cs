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
    /// Interaction logic for StringField.xaml
    /// </summary>
    public partial class StringField : UserControl
    {
        public StringField()
        {
            InitializeComponent();
            SetBinding();
        }

        private void SetBinding()
        {
            Binding myBinding = new Binding();
            myBinding.Source = this;
            myBinding.Path = new PropertyPath("Value");
            myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            BindingOperations.SetBinding(textBox, TextBox.TextProperty, myBinding);
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(string), typeof(StringField), new
PropertyMetadata("", new PropertyChangedCallback(OnValueChanged)));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            StringField UserControl1Control = d as StringField;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            // TextLabel.Text = e.NewValue?.ToString();
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(StringField), typeof(StringField), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputStringField Input
        {
            get { return (InputStringField)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            StringField UserControl1Control = d as StringField;
            UserControl1Control.OnInputChanged(e);
        }

        private void OnInputChanged(DependencyPropertyChangedEventArgs e)
        {
            InputStringField StringField = e.NewValue as InputStringField;

            if (StringField != null)
            {
                SetBinding();
            }
            // TextLabel.Text = e.NewValue?.ToString();
        }
    }
}
