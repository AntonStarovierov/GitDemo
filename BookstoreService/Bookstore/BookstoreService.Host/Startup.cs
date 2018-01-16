using BookstoreService.Base.Auth;
using BookstoreService.Base.Configuration;
using BookstoreService.Base.ServiceCommunicator;
using BookstoreService.BusinessLayer.Services;
using BookstoreService.DataAccessLayer.Contexts;
using BookstoreService.DataAccessLayer.Contexts.DbInit;
using BookstoreService.DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BookstoreService.Host
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
			services.AddDbContext<BookDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("BookstoreConnection")));

			services.AddTransient<IBookRepository, BookRepository>();
			services.AddTransient<IBookService, BookService>();

			services.AddTransient<IServiceCommunicator, ServiceCommunicator>();
			services.AddTransient<IServiceCommunicationFactory, ServiceCommunicationFactory>();

			services.AddTransient<IBookActionsLoggerService, BookActionsLoggerService>();

			services.Configure<ServiceConfiguration>(Configuration.GetSection("ServicesUrls"));

			services.AddMvc();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, BookDbContext context)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();
			StartupConfigureAuth.ConfigureAuth(app);
			app.UseMvc();
			BookDbInitializer.Initialize(context);
		}
	}
}