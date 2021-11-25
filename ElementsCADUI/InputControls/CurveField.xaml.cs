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
    public partial class CurveField : UserControl
    {
        public CurveField()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(Elements.Geometry.Curve), typeof(CurveField), new
PropertyMetadata(null, new PropertyChangedCallback(OnValueChanged)));

        public Elements.Geometry.Curve Value
        {
            get { return (Elements.Geometry.Curve)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        private static void OnValueChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            CurveField UserControl1Control = d as CurveField;
            UserControl1Control.OnValueChanged(e);
        }

        private void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            // TextLabel.Text = e.NewValue?.ToString();
        }

        public static readonly DependencyProperty InputProperty =
DependencyProperty.Register("Input", typeof(InputCurveField), typeof(CurveField), new
PropertyMetadata(null, new PropertyChangedCallback(OnInputChanged)));

        public InputCurveField Input
        {
            get { return (InputCurveField)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        private static void OnInputChanged(DependencyObject d,
           DependencyPropertyChangedEventArgs e)
        {
            CurveField UserControl1Control = d as CurveField;
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

            Type curveType = Input.GetType();

            if (curveType == typeof(InputLineField))
            {
                Elements.Geometry.Line line = new Elements.Geometry.Line();
                line.Start = new Elements.Geometry.Vector3(0, 0, 0);
                line.End = new Elements.Geometry.Vector3(pointPosition.X, pointPosition.Y, 0);
                Value = line;


                button.IsChecked = false;
            }
            else if (curveType == typeof(InputPolylineField))
            {
                Elements.Geometry.Vector3[] vertices =
                {
                    new Elements.Geometry.Vector3(0, 0, 0),
                    new Elements.Geometry.Vector3(pointPosition.X, pointPosition.Y, 0)
            };
                Elements.Geometry.Polyline polyline = new Elements.Geometry.Polyline(vertices);

                Value = polyline;


                button.IsChecked = false;
            }
            else if (curveType == typeof(InputPolygonField))
            {
                Elements.Geometry.Vector3[] vertices =
{
                    new Elements.Geometry.Vector3(0, 0, 0),
                    new Elements.Geometry.Vector3(pointPosition.X, pointPosition.Y, 0)
            };

                Elements.Geometry.Polygon polygon = new Elements.Geometry.Polygon(vertices);
                Value = polygon;

                button.IsChecked = false;
            }
        }

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {

        }
    }
}
