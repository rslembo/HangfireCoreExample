﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.SqlServer;
using HangfireCoreExample.Application.Services;
using HangfireCoreExample.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HangfireCoreExample.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("SqlServerDatabase")));

            RegisterDependencies(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }

        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IBackgroundJobClient>(
                x => new BackgroundJobClient(new SqlServerStorage(
                    Configuration.GetConnectionString("SqlServerDatabase"))
            ));

            services.AddScoped<IJobCreator, JobCreator>();
        }
    }
}
