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
    public partial class SelectField : UserControl
    {
        public SelectField()
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
            BindingOperations.SetBinding(selector, ComboBox.SelectedItemProperty, myBinding);
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(string), typeof(SelectField), new
PropertyMetadata("", new PropertyChangedCallback(OnValueChanged)));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            SelectField UserControl1Control = d as SelectField;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            // TextLabel.Text = e.NewValue?.ToString();
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(InputSelectField), typeof(SelectField), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputSelectField Input
        {
            get { return (InputSelectField)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            SelectField UserControl1Control = d as SelectField;
            UserControl1Control.OnInputChanged(e);
        }

        private void OnInputChanged(DependencyPropertyChangedEventArgs e)
        {
            InputSelectField inputSelectField = e.NewValue as InputSelectField;

            if (inputSelectField != null)
            {
                selector.ItemsSource = inputSelectField.Choices;

                if (inputSelectField.Value != null)
                {
                    selector.SelectedItem = inputSelectField.Value;
                }

                SetBinding();
            }
            // TextLabel.Text = e.NewValue?.ToString();
        }
    }
}
