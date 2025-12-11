using Microsoft.EntityFrameworkCore;
using System.IO;
using webempty.Data;
using webempty.Models;
using webempty.Repositories.Interfaces;
using webempty.Services.Interfaces;
using webempty.UnitOfWorks;


namespace webempty.Services.Implementations
{
	public class ProductService : IProductService
	{
		#region Fields
		private readonly IUnitOfWork _unitOfWork;
		private readonly IFileService _fileService;
		private readonly IProductRepository _repository;
		private readonly IProductImagesRepository _productImagesrepository;

		#endregion


		#region Constructors
		public ProductService(IFileService fileService, IProductRepository repository, IProductImagesRepository productImagesrepository)
		{

			_fileService = fileService;
			_repository = repository;
			_productImagesrepository = productImagesrepository;
		}


		#endregion
		#region Implement functions
		public async Task<string> addproduct(model1 product,List<IFormFile>? files)
		{
			var pathlist = new List<string>();
			var trans = await _repository.BeginTransactionAsync();
			try
			{
				await _repository.AddAsync(product);
				var result = await Addproductimages(files,product.Id);
				if (result.Item1 == null&&result.Item2!="Success")
				{
					return result.Item2;
				}
				pathlist = result.Item1;
				await trans.CommitAsync();
				return "Success";
			}
			catch(Exception e) {

				await trans.RollbackAsync();
				foreach(var file in pathlist)
				{
					_fileService.DeletephysicalFile(file);
				}
				return e.Message + "--" + e.InnerException; }
		
		}

		public async Task<string> deleteproduct(model1 product)
		{
			try {

				//string path = product.Path;
				await _repository.Deletesync(product);
						
				//_fileService.DeletephysicalFile(path);
				return "Success";
			}
			catch(Exception e) { return e.Message + "--" + e.InnerException; }
			
		}

		public async Task<model1?> getproduct(int id)
		{
			 return await _repository.GetAsQueryable().Include(x=>x.ProductImages).FirstOrDefaultAsync(x=>x.Id==id);

		}
		public async Task<model1?> getproductnoinclude(int id)
		{
			return await _repository.GetByIdAsync(id);

		}
		public async Task<List<model1>> getproducts()
		{
			return await _repository.GetListAsync();
		}

		public async Task<string> Updateproduct(model1 product, List<IFormFile>? files)
		{
			var pathlist = new List<string>();
			var trans = await _repository.BeginTransactionAsync();
			try
			{

				await _repository.Updatesync(product);

				if (files != null && files.Count() > 0)
				{
					var productimages = await _productImagesrepository.GetAsQueryable().Where(x => x.ProductId == product.Id).ToListAsync();
					if (productimages.Count()>0)
					{
					var paths=productimages.Select(x => x.Path).ToList();
					await _productImagesrepository.DeleteRangeAsync(productimages);
					//delete files physically
					foreach (var file in paths)
					{
						_fileService.DeletephysicalFile(file);
					}
					}
					var result = await Addproductimages(files, product.Id);
					if (result.Item1 == null && result.Item2 != "Success")
					{
						return result.Item2;
					}
					pathlist = result.Item1;
				}

				
				
				await trans.CommitAsync();
				return "Success";
			}
			catch (Exception e)
			{

				await trans.RollbackAsync();
				foreach (var file in pathlist)
				{
					_fileService.DeletephysicalFile(file);
				}
				return e.Message + "--" + e.InnerException;
			}

		}
		private async Task<(List<string>?,string)> Addproductimages(List<IFormFile>? files,int productid)
		{
			var pathlist = new List<string>();
			if (files != null && files.Count > 0)
			{
				foreach (var file in files)
				{
					var path = await _fileService.Upload(file, "/images/");
					if (!path.StartsWith("/images/"))
					{
						return (null,path);
					}
					pathlist.Add(path);

				}





				var productimages = new List<ProductImages>();
				foreach (var file in pathlist)
				{
					var productimage = new ProductImages();
					productimage.ProductId = productid;
					productimage.Path = file;
					productimages.Add(productimage);
				}
				await _productImagesrepository.AddRangeAsync(productimages);
			}
			return (pathlist,"Success");
		}
		public async Task<bool> IsProductNameExistAsync(string name)
		{
			return await _repository.GetAsQueryable().AnyAsync(x => x.Name == name);
		}
		public async Task<bool> IsProductNameExistexcludeAsync(string name,int id)
		{
			return await _repository.GetAsQueryable().AnyAsync(x => x.Name == name&&x.Id!=id);
		}

		public IQueryable<model1> getproductsAsQueryable(string? search)
		{
			var products = _repository.GetAsQueryable();

			if (!string.IsNullOrWhiteSpace(search))
			{
				products = products.Where(x => x.Name.Contains(search));
			}

			return products;
		}

		#endregion

	}
}
