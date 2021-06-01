using CryptoCamWebAPI.Scheduler;
using CryptoCamWebAPI.WebServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoCamWebAPI
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

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            services.AddSingleton<IExchangeRates_API, WebServices.CoinGecko.CoinGecko_API>();

            
            // Schedule a job to keep the exchange rates updated 
            this.jobScheduler(services);




            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CryptoCamWebAPI", Version = "v1" });
            });
            


        }

        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoCamWebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private async void jobScheduler(IServiceCollection serviceCollection)
        {
            
            serviceCollection.AddScoped<UpdateRepositoriesJob>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // construct a scheduler factory
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler scheduler = await factory.GetScheduler();

            scheduler.JobFactory = new UpdateRepositoriesJobFactory(serviceProvider);

            await scheduler.Start();

            // define the job and tie it to UpdateRepositoriesJob class
            IJobDetail job = JobBuilder.Create<UpdateRepositoriesJob>()
                .WithIdentity("updateRepositoriesJob", "group1")
                .Build();

            // Trigger the job to run at a frecuency
            TimeSpan interval = new TimeSpan(0, 30, 0);

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithInterval(interval)
                    .RepeatForever())
            .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
