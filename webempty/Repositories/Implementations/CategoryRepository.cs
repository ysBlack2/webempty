using Microsoft.EntityFrameworkCore;
using webempty.Data;
using webempty.Models;
using webempty.Repositories.Interfaces;
using webempty.SharedRepositories;

namespace webempty.Repositories.Implementations
{
	public class CategoryRepository:GenericRepository<Category>,ICategoryRepository
	{

		#region Fields
		private readonly DbSet<Category> _categories;
		#endregion

		#region Constructors

		public CategoryRepository(AppDbContext context) : base(context)
		{
			_categories = context.Set<Category>();

		}

		#endregion




	}
}
