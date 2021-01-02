using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisProject.Internal;
using ThesisProject.Internal.Containers;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.Windows;

namespace ThesisProject
{
    public static class ServiceCollectionForUI
    {
        public static void ConfigureForUI(this IServiceCollection services, IConfigurationSection configuration)
        {
            services.Configure<MainWindowOptions>(configuration);
            services.AddScoped<MainWindow>();

            services.AddSingleton<IDevicesContainer, DevicesContainer>();
        }
    }
}
