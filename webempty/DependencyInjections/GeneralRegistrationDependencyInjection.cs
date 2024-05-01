using Microsoft.EntityFrameworkCore;
using System.Reflection;
using webempty.Data;
using webempty.Mapping;

namespace webempty.DependencyInjections
{
	public static class GeneralRegistrationDependencyInjection
	{
		public static IServiceCollection AddGeneralDependencyInjection(this IServiceCollection services, IConfiguration configuration)
		{
			//connction to database
			services.AddDbContext<AppDbContext>(options =>
				options.UseNpgsql(configuration.GetConnectionString("DbConnections")));
			services.AddControllersWithViews();

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				options.IOTimeout = TimeSpan.FromMinutes(5);
				options.IdleTimeout = TimeSpan.FromMinutes(5);
				options.Cookie.Path = "/";
				options.Cookie.IsEssential = true;
				options.Cookie.HttpOnly = true;
				options.Cookie.Name = ".webempty";


			}



			);



			return services;
		}
	}
}
