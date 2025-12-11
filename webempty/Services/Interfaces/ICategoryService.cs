using webempty.Models;

namespace webempty.Services.Interfaces
{
	public interface ICategoryService
	{

		public Task<List<Category>> GetCategoriesAsync();
		public IQueryable<Category> getcategorysAsQueryable(string? search);

		public Task<Category?> getcategory(int id);
		public Task<Category?> getcategorynoinclude(int id);
		public Task<string> addcategoryasync(Category category);
		public Task<string> Updatecategoryasync(Category category);
		public Task<string> deletecategoryasync(Category category);
		public Task<bool> IscategoryNameExistAsync(string name);
		public Task<bool> IscategoryNameExistexcludeAsync(string name, int id);

	}
}
