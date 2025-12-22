using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webempty.ViewModels.Products
{
	public class UpdateProductViewModel
	{
		//lol
		public int Id { get; set; }
		[Required(ErrorMessage = "NameIsRequired")]
		[Remote("IsProductNameExistexclude", "model1", HttpMethod = "Post",AdditionalFields ="Id" ,ErrorMessage = "Name is Already Exist")]
		public required string Name { get; set; }
		[Range(1, double.MaxValue)]
		public int Price { get; set; }
		[NotMapped]
		public List<IFormFile>? Files { get; set; }
		public List<string>? CurrentPaths { get; set; }
		public int CategoryId { get; set; }

	}
}
