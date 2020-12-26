using AutoMapper;
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
        private IServiceProvider ServiceProvider { get; set; }
        private IServer Server { get; set; }
        private IFileService FileService { get; set; }

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

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            Server = ServiceProvider.GetService<IServer>();
            FileService = ServiceProvider.GetService<IFileService>();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
