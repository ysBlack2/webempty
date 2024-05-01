using AutoMapper;
using Kirk.Lib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using webempty.Models;
using webempty.Services.Implementations;
using webempty.Services.Interfaces;
using webempty.ViewModels.Products;
namespace webempty.Controllers
{
	public class ProductController : Controller
	{
		private readonly IProductService _productService; 
		private readonly IFileService _fileService;
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;
		//now by interfaces:
		public ProductController(IProductService productService,IFileService fileService, ICategoryService categoryService, IMapper mapper) 
		{
			_productService = productService;
			_fileService = fileService;
			_categoryService = categoryService;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<IActionResult> Index(string? search)
		{
			//var originProduct = new model1();
			//var product = HttpContext.Session.Get("product");
			//if(product!=null)
			//{
			//	originProduct=JsonSerializer.Deserialize<model1>(product);
			//}

			//ViewBag.username = originProduct?.Name;

			ViewBag.currentSearch = search;
			var products = _productService.getproductsAsQueryable(search);
			var result = await _mapper.ProjectTo<GetProductListViewModel>(products).ToListAsync();
			ViewBag.count = result.Count();
			return View(result);
		}
		[HttpGet]
		public async Task<IActionResult> SearchproductList(string? searchText)
		{
			//var originProduct = new model1();
			//var product = HttpContext.Session.Get("product");
			//if (product != null)
			//{
			//	originProduct = JsonSerializer.Deserialize<model1>(product);
			//}

			//ViewBag.username = originProduct?.Name;

			ViewBag.currentSearchajax = searchText;
			var products = _productService.getproductsAsQueryable(searchText);
			var result = await _mapper.ProjectTo<GetProductListViewModel>(products).ToListAsync();
			ViewBag.count = result.Count();
			return Json(new { result = result });
		}
		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var product =await _productService.getproduct(id);
			return View(product);
		}
		[HttpGet]
		public async Task<IActionResult> Create() 
		{
			ViewData["categories"] = new SelectList(await _categoryService.GetCategoriesAsync(),"Id","Name");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(AddProductViewModel model)
		{
			try
			{
				if (ModelState.IsValid) {


					var product = _mapper.Map<model1>(model);

					await _productService.addproduct(product,model.Files);

					return RedirectToAction(nameof(Index));	
				}
				ViewData["categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
				return View(model);
			}catch (Exception) 
			{
				ViewData["categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
				return View(); 
			
			}
			
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id) 
		{

			var product = await _productService.getproduct(id);
			if (product == null)
			{
				NotFound();
			}
			var response = _mapper.Map<UpdateProductViewModel>(product);
			ViewData["categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
			return View(response);

		}
		[HttpPost]
		public async Task<IActionResult> Update(UpdateProductViewModel model)
		{
			try
			{
				var product = await _productService.getproductnoinclude(model.Id);
				if (product == null)
					return NotFound();
			
				var newproduct = _mapper.Map(model, product);
				var result = await _productService.Updateproduct(newproduct,model.Files);
				// Update fields
				if(result!="Success")
				{
					ModelState.AddModelError(string.Empty, result);
					ViewData["categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
					return View(model);
				}
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				ViewData["categories"] = new SelectList(await _categoryService.GetCategoriesAsync(), "Id", "Name");
				return View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id) 
		{
			var product = await _productService.getproductnoinclude(id);
			if (product == null)
			{
				NotFound();
			}
			return View(product);
		}
		[HttpPost,ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirm(int id)
		{
			try
			{
				var product =await  _productService.getproductnoinclude(id);
				if (product == null)
				{
					NotFound();
				}
				await _productService.deleteproduct(product);
				return RedirectToAction(nameof (Index));
				
			}catch(Exception) { return View(); }
			
			
		}
		[HttpPost]
		public async Task<IActionResult> IsProductNameExist(string name)
		{
			var result = await _productService.IsProductNameExistAsync(name);
			if (result)
				return Json(false);
			return Json(true);
		}
		[HttpPost]
		public async Task<IActionResult> IsProductNameExistexclude(string name,int id)
		{
			var result = await _productService.IsProductNameExistexcludeAsync(name,id);
			if (result)
				return Json(false);
			return Json(true);
		}
	}
}
