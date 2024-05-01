using SimpleProject.UnitOfWorks;
using webempty.Repositories.Implementations;
using webempty.Repositories.Interfaces;
using webempty.SharedRepositories;
using webempty.UnitOfWorks;

namespace webempty.DependencyInjections
{
	public static class RepositoryDependencyhInjection
	{
		public static IServiceCollection AddRepositoryDependencyInjection(this IServiceCollection services)
		{


			services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
			services.AddTransient<IProductRepository, ProductRepository>();
			services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddTransient<IProductImagesRepository, ProductImagesRepository>();
			services.AddTransient<ICategoryRepository, CategoryRepository>();

			return services;
		}
	}
}
