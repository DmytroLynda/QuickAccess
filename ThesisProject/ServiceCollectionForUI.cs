using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThesisProject
{
    public static class ServiceCollectionForUI
    {
        public static void ConfigureForUI(this IServiceCollection services)
        {
            services.AddScoped<MainWindow>();
        }
    }
}
