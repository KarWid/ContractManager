namespace ContractManager
{
    using System;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MongoDB.Driver;
    using ContractManager.Common.Services;
    using ContractManager.Repository;
    using ContractManager.Repository.Entities;
    using ContractManager.Repository.Repositories;
    using ContractManager.Validators;
    using ContractManager.ViewModels.Paragraphs;
    using ModelConstants = ContractManager.Models.Constants;

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
            services.AddControllersWithViews();

            services.AddSingleton<IMongoClient>(
                _ => new MongoClient(Configuration.GetConnectionString(ModelConstants.ConnectionStrings.CONTRACT_MANAGER_DB)));
            services.AddScoped(
                s => new ContractManagerDbContext(
                    s.GetRequiredService<IMongoClient>(), 
                    Configuration[ModelConstants.AppSettings.CONTRACT_MANAGER_DB_NAME]));

            // services
            services.AddScoped<IRepository<ParagraphEntity>, ParagraphRepository>();
            services.AddScoped<IParagraphService, ParagraphService>();

            // validators
            services.AddScoped<IValidator<ParagraphVM>, ParagraphValidator>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
