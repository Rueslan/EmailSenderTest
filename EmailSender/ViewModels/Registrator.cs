using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.ViewModels
{
    internal static class Registrator
    {
        public static IServiceCollection RegisteViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddTransient<SenderEmailListEditViewModel>();
            services.AddTransient<TargetEmailListEditViewModel>();
            return services;
        }
    }
}
