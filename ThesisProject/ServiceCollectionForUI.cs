using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThesisProject.Internal;
using ThesisProject.Internal.Containers;
using ThesisProject.Internal.Controllers;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.Windows;

namespace ThesisProject
{
    public static class ServiceCollectionForUI
    {
        public static void ConfigureForUI(this IServiceCollection services, IConfigurationSection configuration)
        {
            services.Configure<MenuUpdaterOptions>(configuration);
            services.AddScoped<MainWindow>();

            services.AddScoped<LoginWindow>();

            services.AddScoped<FileInfoWindow>();

            services.AddScoped<IMenuUpdater, MenuUpdater>();

            services.AddSingleton<IDevicesContainer, DevicesContainer>();
            services.AddSingleton<IFilesContainer, FilesContainer>();

            services.AddSingleton<IMainWindowController, MainWindowController>();
        }
    }
}
