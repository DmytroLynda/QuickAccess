using AutoMapper;
using ClientLogic;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerInterface;
using ServerLogic;
using System;
using System.Windows;
using System.Windows.Threading;
using ThesisProject.Internal.Interfaces;

namespace ThesisProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServer _server;

        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        private void ConfigureServices(IServiceCollection services)
        {
            //Data layer configuratin.
            services.ConfigureForData(Configuration.GetSection("Data"));

            //Application layer configuration
            services.ConfigureForClientLogic();
            services.ConfigureForServerLogic();

            //Interface layer configuration
            services.ConfigureForUI(Configuration.GetSection("UI"));
            services.ConfigureForServerInterface(Configuration.GetSection("HttpServer"));

            //External dependencies
            services.AddLogging();
            services.AddAutoMapper((configuration) => 
            {
                configuration.AllowNullDestinationValues = false;
                configuration.AllowNullCollections = false;
            },
            AppDomain.CurrentDomain.GetAssemblies());
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            //Hundles all app exceptions and shows for a user.
            DispatcherUnhandledException += OnCatchUnhandledException;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("usersettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var windowManager = ServiceProvider.GetRequiredService<IWindowsManager>();
            windowManager.ShowLoginWindow(caller: null);

            _server = ServiceProvider.GetRequiredService<IServer>();
            await _server.StartAsync();
        }

        private void OnCatchUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);

            e.Handled = true;
        }
    }
}
