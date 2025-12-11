using Microsoft.EntityFrameworkCore;
using webempty.Data;
using webempty.Models;
using webempty.Repositories.Implementations;
using webempty.Repositories.Interfaces;
using webempty.Services.Interfaces;

namespace webempty.Services.Implementations
{
	public class CategoryService : ICategoryService
	{
		#region Fields
		private readonly ICategoryRepository _categoryRepository;

		#endregion
		#region Constructors
		public CategoryService(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;



		}


		#endregion
		#region Functions
		public async Task<List<Category>> GetCategoriesAsync()
		{
			return await _categoryRepository.GetListAsync();


		}

		public async Task<Category?> getcategory(int id)
		{
			return await _categoryRepository.GetAsQueryable().FirstOrDefaultAsync(x=>x.Id==id);
		}

		public async Task<Category?> getcategorynoinclude(int id)
		{
			return await _categoryRepository.GetByIdAsync(id);
		}

		public IQueryable<Category> getcategorysAsQueryable(string? search)
		{
			return _categoryRepository.GetAsQueryable();
		}

		public async Task<bool> IscategoryNameExistAsync(string name)
		{
			return await _categoryRepository.GetAsQueryable().AnyAsync(x => x.Name == name);

		}

		public async Task<bool> IscategoryNameExistexcludeAsync(string name, int id)
		{
			return await _categoryRepository.GetAsQueryable().AnyAsync(x => x.Name == name&&x.Id!=id);

		}

		public async Task<string> Updatecategoryasync(Category category)
		{
			try
			{
				await _categoryRepository.Updatesync(category);
				return "Success";
			}
			catch(Exception ex) 
			{
				return ex.Message + "--" + ex.InnerException;
			}
		}
		public async Task<string> addcategoryasync(Category category)
		{
			try
			{
				await _categoryRepository.AddAsync(category);
				return "Success";
			}
			catch (Exception ex)
			{
				return ex.Message + "--" + ex.InnerException;
			}
		}

		public async Task<string> deletecategoryasync(Category category)
		{
			try
			{
				await _categoryRepository.Deletesync(category);
				return "Success";
			}
			catch (Exception ex)
			{
				return ex.Message + "--" + ex.InnerException;
			}
		}

		#endregion


	}
}
