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
    /// Interaction logic for GeometryField.xaml
    /// </summary>
    public partial class PointField : UserControl
    {
        public PointField()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(Elements.Geometry.Vector3), typeof(PointField), new
PropertyMetadata(new Elements.Geometry.Vector3(), new PropertyChangedCallback(OnValueChanged)));

        public Elements.Geometry.Vector3 Value
        {
            get { return (Elements.Geometry.Vector3)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            PointField UserControl1Control = d as PointField;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            // TextLabel.Text = e.NewValue?.ToString();
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(InputCurveField), typeof(PointField), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputPointField Input
        {
            get { return (InputPointField)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            PointField UserControl1Control = d as PointField;
            UserControl1Control.OnInputChanged(e);
        }

        private void OnInputChanged(DependencyPropertyChangedEventArgs e)
        {
            InputCurveField inputGeometryField = e.NewValue as InputCurveField;

            if (inputGeometryField != null)
            {

                // SetBinding();
            }
            // TextLabel.Text = e.NewValue?.ToString();
        }

        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointPosition = e.GetPosition(drawingCanvas);

            Value = new Elements.Geometry.Vector3(pointPosition.X, pointPosition.Y, 0);
            button.IsChecked = false;

            //if (this.penSelection.SelectedBrush != null)
            //{

            //    _line = new Polyline();
            //    _line.Stroke = this.penSelection.SelectedBrush;
            //    _line.StrokeThickness = 2.0;

            //    drawingCanvas.Children.Add(_line);
            //}
        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //if (this.penSelection.SelectedBrush != null && _line != null)
            //{
            //    if (e.LeftButton == MouseButtonState.Pressed)
            //    {
            //        Point currentPoint = e.GetPosition(drawingCanvas);
            //        if (_startPoint != currentPoint)
            //        {
            //            _line.Points.Add(currentPoint);
            //        }
            //    }
            //}
        }
    }
}
