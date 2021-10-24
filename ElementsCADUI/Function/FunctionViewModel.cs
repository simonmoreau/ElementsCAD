using ElementsCADUI.Services;
using ElementsCADUI.MainWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsCADUI.Function
{
    class FunctionViewModel : BindableBase
    {
        private ErrorViewModel _errorViewModel;
        FunctionViewModel(ErrorViewModel errorViewModel)
        {
            _errorViewModel = errorViewModel;
        }

    }
}
