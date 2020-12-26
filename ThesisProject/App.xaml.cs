using ClientLogic;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Server;
using System;
using System.Windows;

namespace ThesisProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //First: Data layer configuratin.
            services.ConfigureForData();

            //Second: Application layer configuration
            services.ConfigureForServer();
            services.ConfigureForClientLogic();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {

        }
    }
}
