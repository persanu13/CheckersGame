using Dame.Core;
using Dame.MVVM.View;
using Dame.MVVM.ViewModels;
using Dame.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;
        public App() 
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowVM>()
            });

            services.AddSingleton<MainWindowVM>();
            services.AddSingleton<MenuVM>();
            services.AddSingleton<GameVM>();
            services.AddSingleton<MainMenuVM>();
            services.AddSingleton<NewGameMenuVM>();
            services.AddSingleton<LoadGameVM>();
            services.AddSingleton<HelpMenuVM>();
            services.AddSingleton<AboutVM>();
            services.AddSingleton<RulesVM>();
            services.AddSingleton<StatisticsVM>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(servicesProvider =>
            viewModelType => (ViewModel)servicesProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow =  _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
