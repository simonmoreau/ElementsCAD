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
    public partial class ListField : UserControl
    {
        public ListField()
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
            // BindingOperations.SetBinding(selector, ComboBox.SelectedItemProperty, myBinding);
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(string), typeof(ListField), new
PropertyMetadata("", new PropertyChangedCallback(OnValueChanged)));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            ListField UserControl1Control = d as ListField;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            // TextLabel.Text = e.NewValue?.ToString();
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(InputListField), typeof(ListField), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputListField Input
        {
            get { return (InputListField)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            ListField UserControl1Control = d as ListField;
            UserControl1Control.OnInputChanged(e);
        }

        private void OnInputChanged(DependencyPropertyChangedEventArgs e)
        {
            InputListField inputListField = e.NewValue as InputListField;

            if (inputListField != null)
            {
                // listview.ItemsSource = inputListField.Items;

                Binding myBinding = new Binding();
                myBinding.Source = inputListField;
                myBinding.Path = new PropertyPath("InnerList");
                myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                BindingOperations.SetBinding(listview, ListView.ItemsSourceProperty, myBinding);



                // SetBinding();
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            Input.AddInput();
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            InputDisplayable inputDisplayable = (InputDisplayable)((Button)sender).Tag;
            Input.RemoveInput(inputDisplayable);
        }
    }
}
