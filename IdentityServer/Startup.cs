using System;
using IdentityServer.Data;
using IdentityServer.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UsersContext>(config =>
                {
                    config.UseSqlServer(Configuration.GetConnectionString("UsersConnection"));
                })
                .AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<UsersContext>();


            services.AddIdentityServer(config =>
                {
                    config.UserInteraction.LoginUrl = "Auth/Login";
                })
                .AddAspNetIdentity<User>()
                .AddInMemoryIdentityResources(ConfigurationIS4.GetIdentityResources())
                .AddInMemoryClients(ConfigurationIS4.GetClients())
                .AddInMemoryApiResources(ConfigurationIS4.GetApiResources())
                .AddInMemoryApiScopes(ConfigurationIS4.GetApiScopes())
                .AddDeveloperSigningCredential();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
