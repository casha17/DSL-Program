using Cinema.Configuration;
using Cinema.Handlers;
using Cinema.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using AutoMapper;
using Cinema.Mapping;
using Cinema.Persistence.Models;
using Cinema.RequestModels;

namespace Cinema
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration config)
        {
            _configuration = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            // Add Configurations
            services.Configure<PersistenceConfiguration>(_configuration.GetSection(nameof(PersistenceConfiguration)));
            
            // Register MongoDB database
            services.AddSingleton<IMongoClient>(ctx => new MongoClient(ctx.GetService<IOptions<PersistenceConfiguration>>().Value.MongoClusterConnectionString));
            
            // Register handlers
			services.AddScoped<IClientHandler, ClientHandler>();
			services.AddScoped<IAdminHandler, AdminHandler>();
			services.AddScoped<ISeatHandler, SeatHandler>();
			services.AddScoped<ICinemaHallHandler, CinemaHallHandler>();
			services.AddScoped<INightPlanHandler, NightPlanHandler>();
			services.AddScoped<ICinemaBookingHandler, CinemaBookingHandler>();

            // Register repositories
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ISeatRepository, SeatRepository>();
            services.AddScoped<ICinemaHallRepository, CinemaHallRepository>();
			services.AddScoped<INightPlanRepository, NightPlanRepository>();
			services.AddScoped<ICinemaBookingRepository, CinemaBookingRepository>();

			//Register Mapper
			var mapperConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});
			
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);
			
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });
        
        	//Add swagger
        	services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
//Swaggerrr
        	app.UseSwagger();
        	//Enable middleware to serve ui
        	app.UseSwaggerUI(c =>
        	{
        	    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booker app");
     		});        	
        	
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
