using AutoMapper;
using ClientLogic;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Server;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ThesisProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ConfigureServices(IServiceCollection services)
        {
            //Data layer configuratin.
            services.ConfigureForData();

            //Application layer configuration
            services.ConfigureForServer();
            services.ConfigureForClientLogic();

            //UI layer configuration
            services.ConfigureForUI();

            //External dependencies
            services.AddLogging();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var server = serviceProvider.GetService<IServer>();
            await server.StartAsync("http://127.0.0.1:65432/");
        }
    }
}
