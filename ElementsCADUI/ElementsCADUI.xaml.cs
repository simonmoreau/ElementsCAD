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

namespace ElementsCADUI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ElementsCADUI : UserControl
    {
        private MainWindows.ErrorViewModel _errorViewModel;

        public ElementsCADUI()
        {

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            MainWindows.StartingView startingView = new MainWindows.StartingView();
            MainWindows.StartingViewModel StartingViewModel = (MainWindows.StartingViewModel)startingView.DataContext;
            _errorViewModel = StartingViewModel.ErrorViewModel;

            InitializeComponent();

            mainGrid.Children.Add(startingView);

            InitializeComponent();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = new Exception("UnhandledException : " + e.ExceptionObject.ToString());

            _errorViewModel.ThrowError(exception);
        }
    }
}
