using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using webempty.Services.Interfaces;
using webempty.ViewModels.Categories;

namespace webempty.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

		public CategoryController(ICategoryService categoryService,IMapper mapper)
		{
			_categoryService = categoryService;
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var categories = await _categoryService.GetCategoriesAsync();
			var result = _mapper.Map<List<GetCategoriesListViewModel>>(categories);
			return View(result);
		}
	}
}
