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
using ElementsCADUI;

namespace ElementsCAD
{
    /// <summary>
    /// Interaction logic for MainPaletteSet.xaml
    /// </summary>
    public partial class MainPaletteSet : UserControl
    {
        public MainPaletteSet()
        {
            ElementsCADUI.ElementsCADUI elementsCADUI = new ElementsCADUI.ElementsCADUI();
            InitializeComponent();
        }
    }
}
