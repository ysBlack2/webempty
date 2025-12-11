using Microsoft.EntityFrameworkCore;
using webempty.Data;
using webempty.Models;
using webempty.Repositories.Interfaces;
using webempty.SharedRepositories;

namespace webempty.Repositories.Implementations
{
	public class ProductImagesRepository : GenericRepository<ProductImages>, IProductImagesRepository
	{
		#region Fields
		private readonly DbSet<ProductImages> _productimages;
		#endregion

		#region Constructors

		public ProductImagesRepository(AppDbContext context) : base(context)
		{
			_productimages = context.Set<ProductImages>();

		}

		#endregion


	}
}
