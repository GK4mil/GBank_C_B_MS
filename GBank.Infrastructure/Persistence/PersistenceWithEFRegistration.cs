
using System.Text;
using GBank.Infrastructure.Persistence;
using GBank.Application.Common.Interfaces;
using GBank.Infrastructure.Services;
using System.Configuration;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using GBank.Application.Contracts.Persistence;
using GBank.Infrastructure.Repositories.EF;
using Microsoft.EntityFrameworkCore;

namespace GBank.Infrastructure.Persistence
{
    public static class PersistenceWithEFRegistration
    {
        public static IServiceCollection AddGBankPersistenceEFServices(this IServiceCollection services, IConfiguration configuration)
        {
            {
                services.AddDbContext<GBankDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSQLConnectionString")));

                services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

                services.AddScoped<IUserRepository, UserRepository>();
                services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
                services.AddScoped<IBillRepository, BillRepository>();
                services.AddScoped<INewsRepository, NewsRepository>();

                //services.AddScoped<IPostRepository, PostRepository>();

                return services;
            }
        }
    }
}
