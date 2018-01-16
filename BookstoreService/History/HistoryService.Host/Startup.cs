using BookstoreService.Base.Auth;
using BookstoreService.Base.ServiceCommunicator;
using HistoryService.DataAccessLayer.Contexts;
using HistoryService.DataAccessLayer.Contexts.DbInit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HistoryService.Host
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
			services.AddDbContext<HistoryDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("HistoryConnection")));

			services.AddTransient<IHistoryRepository, HistoryRepository>();

			services.AddTransient<IServiceCommunicator, ServiceCommunicator>();
			services.AddTransient<IServiceCommunicationFactory, ServiceCommunicationFactory>();

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, HistoryDbContext context)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
			StartupConfigureAuth.ConfigureAuth(app);
			app.UseMvc();
			HistoryDbInitializer.Initialize(context);
		}
	}
}