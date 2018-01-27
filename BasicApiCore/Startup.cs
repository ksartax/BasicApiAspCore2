using BasicApiCore.Entities;
using BasicApiCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace BasicApiCore
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional:true, reloadOnChange:true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif

            services.AddScoped<ICastleRepository, CastleRepository>();

            var connectionString = Startup.Configuration["connectionStrings:castleDBConnectionString"];
            services.AddDbContext<CastleContext>(o => o.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, CastleContext castleContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();
            app.UseMvc();
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Castle, Models.CastleWithoutDetailsDto>();
                cfg.CreateMap<Entities.Castle, Models.CastleDto>();
                cfg.CreateMap<Entities.CastleDetail, Models.CastleDetailDto>();
                cfg.CreateMap<Models.CastleDetailForCreateDto, Entities.CastleDetail>();
                cfg.CreateMap<Models.CastleDetailForUpdateDto, Entities.CastleDetail>();
                cfg.CreateMap<Entities.CastleDetail, Models.CastleDetailForUpdateDto>();
            });

            castleContext.EnsureSeedDataForContext();
        }
    }
}
