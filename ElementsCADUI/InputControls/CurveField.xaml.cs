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
            InputCurveField inputCurveField = e.NewValue as InputCurveField;

            if (inputCurveField != null)
            {
                Type curveType = inputCurveField.GetType();
                if (curveType == typeof(InputLineField))
                {
                    applyButton.Visibility = Visibility.Collapsed;
                }
                else if (curveType == typeof(InputPolylineField))
                {
                    applyButton.Visibility = Visibility.Visible;
                }
                else if (curveType == typeof(InputPolygonField))
                {
                    applyButton.Visibility = Visibility.Visible;
                }
            }

        }

        private List<Point> points = new List<Point>();

        private void myCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointPosition = e.GetPosition(drawingCanvas);
            points.Add(pointPosition);

            Type curveType = Input.GetType();

            if (curveType == typeof(InputLineField))
            {
                if (points.Count >= 2)
                {
                    Elements.Geometry.Line line = new Elements.Geometry.Line();
                    line.Start = new Elements.Geometry.Vector3(points[0].X, points[0].Y, 0);
                    line.End = new Elements.Geometry.Vector3(points[1].X, points[1].Y, 0);
                    Value = line;
                    points.Clear();
                    button.IsChecked = false;
                }
            }
            else if (curveType == typeof(InputPolylineField))
            {
                Elements.Geometry.Vector3[] vertices = points.Select(p => new Elements.Geometry.Vector3(p.X, p.Y, 0)).ToArray();

                Elements.Geometry.Polyline polyline = new Elements.Geometry.Polyline(vertices);

                Value = polyline;
            }
            else if (curveType == typeof(InputPolygonField))
            {
                Elements.Geometry.Vector3[] vertices = points.Select(p => new Elements.Geometry.Vector3(p.X, p.Y, 0)).ToArray();

                if (vertices.Length > 2)
                {
                    Elements.Geometry.Polygon polygon = new Elements.Geometry.Polygon(vertices);
                    Value = polygon;
                }
            }
        }

        private Line line = new Line();

        private void myCanvas_MouseMove(object sender, MouseEventArgs e)
        {

            // Get the x and y coordinates of the mouse pointer.
            System.Windows.Point position = e.GetPosition(this);
            double pX = position.X - 13;
            double pY = position.Y - 27;

            // Move the ellipse around
            TranslateTransform transform = new TranslateTransform();
            transform.X = pX;
            transform.Y = pY;
            cross.RenderTransform = transform;

            // if there is already a point
            if (points.Count > 0)
            {
                drawingCanvas.Children.Remove(line);

                line.Stroke = System.Windows.Media.Brushes.Black;
                line.X1 = points.Last().X;
                line.Y1 = points.Last().Y;
                line.X2 = pX+6;
                line.Y2 = pY+6;

                drawingCanvas.Children.Add(line);

            }
        }

        private void ApplyClick(object sender, RoutedEventArgs e)
        {
            points.Clear();
            button.IsChecked = false;
        }
    }
}
