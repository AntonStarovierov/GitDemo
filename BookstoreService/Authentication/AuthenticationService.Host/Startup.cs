using AuthenticationService.BusinessLayer.Services;
using AuthenticationService.DataAccessLayer.Contexts;
using AuthenticationService.DataAccessLayer.Contexts.DbInit;
using AuthenticationService.DataAccessLayer.Repositories;
using BookstoreService.Base.Auth;
using BookstoreService.Base.Configuration;
using BookstoreService.Base.ServiceCommunicator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuthenticationService.Host
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", false, true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AuthenticationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("AuthConnection")));

			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IUserRepository, UserRepository>();

			services.AddTransient<IServiceCommunicator, ServiceCommunicator>();
			services.AddTransient<IServiceCommunicationFactory, ServiceCommunicationFactory>();

			services.AddTransient<IAuthActionLoggerService, AuthActionsLoggerService>();

			services.Configure<ServiceConfiguration>(Configuration.GetSection("ServicesUrls"));

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AuthenticationDbContext context)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
			StartupConfigureAuth.ConfigureAuth(app);
			app.UseMvc();
			AuthenticationDbInitializer.Initialize(context);
		}
	}
}