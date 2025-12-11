using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using webempty.Data;
using webempty.Models;
using webempty.Repositories.Interfaces;
using webempty.SharedRepositories;

namespace webempty.Repositories.Implementations
{
	public class ProductRepository:GenericRepository<model1> ,IProductRepository
	{
		#region Fields
		private readonly DbSet<model1> _products;
		#endregion

		#region Constructors

		public ProductRepository(AppDbContext context):base(context)
		{ 
			_products = context.Set<model1>();
			
		}

		#endregion


	}
}
