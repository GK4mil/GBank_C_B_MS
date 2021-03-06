using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GBank.Infrastructure.Persistence;
using GBank.Application.Common.Interfaces;
using GBank.Infrastructure.Services;
using System.Configuration;
using MediatR;
using System.Reflection;
using GBank.Application;
using Plain.RabbitMQ;
using RabbitMQ.Client;
using Microsoft.OpenApi.Models;
using GBankAdminService.Application.Common.Interfaces;
using GBankAdminService.Infrastructure.Services;
using Nest;

namespace GBank.API
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "mycorspolicy";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                  });
            });

            services.AddGBankPersistenceEFServices(Configuration);

            services.AddMediatR(typeof(GBank.Application.Functions.Authentication.Command.LoginCommand).GetTypeInfo().Assembly);
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IPasswordHashService, PasswordHashService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var settings = new ConnectionSettings(new Uri(@"http://192.168.5.3:9200"));
            services.AddSingleton<IElasticClient>(new ElasticClient(settings));


            services.AddControllers();

            


            JwtBearerOptions options(JwtBearerOptions jwtBearerOptions, string audience)
            {
                jwtBearerOptions.RequireHttpsMetadata = false;
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Superlongsupersecret!")),
                    ValidIssuer = "GBank",
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(1) //1 minute tolerance for the expiration date
                };
                if (audience == "access")
                {
                    jwtBearerOptions.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                }
                return jwtBearerOptions;
            }

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtBearerOptions => options(jwtBearerOptions, "access"))
            .AddJwtBearer("refresh", jwtBearerOptions => options(jwtBearerOptions, "refresh"));

            //Rabbit MQ
            services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@192.168.5.3:5672"));
            services.AddSingleton<Plain.RabbitMQ.IPublisher>(
                x => new Publisher(x.GetService<IConnectionProvider>(),
                "transfer_exchange", ExchangeType.Topic));

            //Rabbit MQ   

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyTestService", Version = "v1", });
            });

            //Elastic
            var settings_elastic = new ConnectionSettings(new Uri(@"http://192.168.5.3:9200"));
            settings_elastic.DisableDirectStreaming();
            services.AddSingleton<IElasticClient>(new ElasticClient(settings_elastic));


            //Elastic
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestService");
            });
        }
    }
}
