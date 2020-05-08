using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.Data;
using WebApi.Domain.Interfaces;
using WebApi.Service.Helpers;

namespace WebApi.Service
{
    public class Startup
    {
        private AppSettings _settings;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");

            _settings = appSettingsSection.Get<AppSettings>();
            services.Configure<AppSettings>(appSettingsSection);

            services.AddControllers();

            services.AddSingleton<IUserRepository>(new UserRepository(_settings.DbConnectionString));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_settings.VersionId, new OpenApiInfo { Title = _settings.ApplicationName, Version = _settings.VersionId });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{_settings.VersionId}/swagger.json", _settings.ApplicationName);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
