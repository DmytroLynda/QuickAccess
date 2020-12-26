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
        private readonly IServiceProvider _serviceProvider;
        private readonly IServer _server;
        private readonly IFileService _fileService;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            _serviceProvider = services.BuildServiceProvider();

            _server = _serviceProvider.GetService<IServer>();
            _fileService = _serviceProvider.GetService<IFileService>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //First: Data layer configuratin.
            services.ConfigureForData();

            //Second: Application layer configuration
            services.ConfigureForServer();
            services.ConfigureForClientLogic();

            services.AddLogging();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {

        }
    }
}
