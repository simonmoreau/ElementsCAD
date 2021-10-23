using ElementsCADUI.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementsCADUI.MainWindows
{
    public class ErrorViewModel : BindableBase
    {
        private bool ErrorThrown = false;
        public ErrorViewModel()
        {
            BackToLoginCommand = new RelayCommand(OnBackToLoginView);
            CopyErrorCommand = new RelayCommand(OnCopyError);
        }

        public event Action NavigateToItself = delegate { };

        public void ThrowError(Exception ex)
        {
            if (!ErrorThrown)
            {
                Debug.WriteLine(DateTime.Now.ToString() + " - " + $"ErrorViewModel: " + ex.Message);
                ErrorThrown = true;
                // Navigate to itself
                NavigateToItself();

                // display error 
                ErrorMessage = ex.Message;
                ErrorDetails = ex.ToString();
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { SetProperty(ref _errorMessage, value); }
        }

        private string _errorDetails;
        public string ErrorDetails
        {
            get { return _errorDetails; }
            set { SetProperty(ref _errorDetails, value); }
        }

        public RelayCommand CopyErrorCommand { get; private set; }

        private void OnCopyError()
        {
            System.Windows.Clipboard.SetText(_errorDetails);
        }

        public event Action BackToLoginView = delegate { };
        public RelayCommand BackToLoginCommand { get; private set; }

        private void OnBackToLoginView()
        {
            BackToLoginView();
            ErrorThrown = true;
        }
    }
}
