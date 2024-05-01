using webempty.Models;

namespace webempty.Services.Interfaces
{
	public interface IProductService
	{
		public Task<List<model1>> getproducts();
		public IQueryable<model1> getproductsAsQueryable(string? search);

		public Task<model1?> getproduct(int id);
		public Task<model1?> getproductnoinclude(int id);
		public Task<string> addproduct(model1 product, List<IFormFile>? files);
		public Task<string> Updateproduct(model1 product,List<IFormFile>? files);
		public Task<string> deleteproduct(model1 product);
		public Task<bool> IsProductNameExistAsync(string name);
		public Task<bool> IsProductNameExistexcludeAsync(string name,int id);
	}
}
