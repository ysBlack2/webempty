using AutoMapper;
using webempty.Models;
using webempty.ViewModels.Categories;

namespace webempty.Mapping
{
	public class CategoryProfile :Profile
	{
		public CategoryProfile()
		{
			CreateMap<Category, GetCategoriesListViewModel>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

			CreateMap<Category, GetCategoryByIdViewModel>();

			CreateMap<AddCategoryViewModel, Category>();

			CreateMap<Category, UpdateCategoryViewModel>();


		}
	}
}
