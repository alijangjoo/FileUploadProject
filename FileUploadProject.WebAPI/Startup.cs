using FileUploadProject.WebAPI.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileUploadProject.WebAPI.DataAccess.Repositories;

namespace FileUploadProject.WebAPI
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Configuration = configuration;
            _environment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<ApplicationConfiguration>(Configuration.GetSection("ApplicationConfiguration"));

            var sqlConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<FileContext>(options =>
                options.UseSqlServer(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("FileUploadProject.WebAPI")
                )
            );

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileUploadProject.WebAPI", Version = "v1" });
            });
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<ValidateMimeMultipartContentFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileUploadProject.WebAPI v1"));
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
