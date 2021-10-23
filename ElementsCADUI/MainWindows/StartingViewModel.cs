using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ElementsCADUI.Services;

namespace ElementsCADUI.MainWindows
{
    public class StartingViewModel : BindableBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        private ServiceProvider _serviceProvider;

        private BindableBase _currentViewModel;
        public BindableBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set { SetProperty(ref _currentViewModel, value); }
        }
        public ErrorViewModel ErrorViewModel { get; }

        public StartingViewModel()
        {
            // create a new ServiceCollection 
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            // create a new ServiceProvider
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _mainWindowViewModel = _serviceProvider.GetService<MainWindowViewModel>();
            ErrorViewModel = _serviceProvider.GetRequiredService<ErrorViewModel>();
            ErrorViewModel.NavigateToItself += NavToErrorView;

            CurrentViewModel = _mainWindowViewModel;

        }

        private void NavToErrorView()
        {
            CurrentViewModel = ErrorViewModel;
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MainWindowViewModel>();
            serviceCollection.AddSingleton<ErrorViewModel>();
        }
    }


}