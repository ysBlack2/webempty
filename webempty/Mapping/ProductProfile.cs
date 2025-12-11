using AutoMapper;
using webempty.Models;
using webempty.ViewModels.Products;

namespace webempty.Mapping
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{

			// CREATE
			CreateMap<AddProductViewModel, model1>();
			//GET List
			CreateMap<model1, GetProductListViewModel>()
			 .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name));

			// UPDATE (GET)
			CreateMap<model1, UpdateProductViewModel>()
				.ForMember(des => des.Name, opt => opt.MapFrom(src => src.Name));
			// UPDATE (POST)
			CreateMap<model1,UpdateProductViewModel>()
				 .ForMember(des => des.CurrentPaths, opt => opt.MapFrom(src => src.ProductImages.Select(x => x.Path).ToList()));

			CreateMap<UpdateProductViewModel, model1>();
		}
	}
}
