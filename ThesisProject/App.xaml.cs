﻿using AutoMapper;
using ClientLogic;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server;
using System;
using System.Windows;
using ThesisProject.Internal.Windows;

namespace ThesisProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        private void ConfigureServices(IServiceCollection services)
        {
            //Data layer configuratin.
            services.ConfigureForData(Configuration.GetSection("Data"));

            //Application layer configuration
            services.ConfigureForServer(Configuration.GetSection("HttpServer"));
            services.ConfigureForClientLogic();

            //UI layer configuration
            services.ConfigureForUI(Configuration.GetSection("UI"));

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
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            Configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            var server = ServiceProvider.GetService<IServer>();
            await server.StartAsync();
        }
    }
}
