using ElementsCADUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsCADUI.MainWindows
{
    class MainWindowViewModel : BindableBase
    {
        private MainWindows.ErrorViewModel _errorViewModel;

        public MainWindowViewModel(MainWindows.ErrorViewModel errorViewModel)
        {
            _errorViewModel = errorViewModel;
            ClickCommand = new RelayCommand(OnClickCommand);
        }

        public RelayCommand ClickCommand { get; private set; }

        private void OnClickCommand()
        {
            _errorViewModel.ThrowError(new Exception("Hello error !"));
        }
    }
}
