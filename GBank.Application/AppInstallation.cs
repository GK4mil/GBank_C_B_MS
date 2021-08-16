using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using MediatR;
using GBank.Infrastructure.Services;
using GBank.Application.Common.Interfaces;

namespace GBank.Application
{
    public static class AppInstallation
    {
        public static IServiceCollection AppGBankApplication(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(GBank.Application.Functions.Authentication.Command.LoginCommand).GetTypeInfo().Assembly);
            
            return services;
        }
    }
}
