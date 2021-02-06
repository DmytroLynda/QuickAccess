using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThesisProject.Internal;
using ThesisProject.Internal.Containers;
using ThesisProject.Internal.Controllers;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.Options;
using ThesisProject.Internal.Providers;
using ThesisProject.Internal.Windows;

namespace ThesisProject
{
    public static class ServiceCollectionForUI
    {
        public static void ConfigureForUI(this IServiceCollection services, IConfigurationSection configuration)
        {
            services.Configure<MenuUpdaterOptions>(configuration);

            services.AddSingleton<MainWindow>();
            services.AddSingleton<LoginWindow>();
            services.AddTransient<ConfigurationWindow>();
            services.AddTransient<FileInfoWindow>();

            services.AddTransient<IMenuUpdater, MenuUpdater>();

            services.AddSingleton<IDevicesContainer, DevicesContainer>();
            services.AddSingleton<IFilesContainer, FilesContainer>();

            services.AddSingleton<IMainWindowController, MainWindowController>();
            services.AddSingleton<ILoginWindowController, LoginWindowController>();
            services.AddSingleton<IUserSettingsController, UserSettingsController>();
            services.AddSingleton<IConfigurationWindowController, ConfigurationWindowController>();

            services.AddSingleton<IUserLoginProvider, UserLoginProvider>();

            services.AddSingleton<IWindowManager, WindowManager>();
        }
    }
}
