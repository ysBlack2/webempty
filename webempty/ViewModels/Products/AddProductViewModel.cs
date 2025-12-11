using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using webempty.Models;
using Microsoft.AspNetCore.Mvc;

namespace webempty.ViewModels.Products
{
	public class AddProductViewModel
	{
		[Required(ErrorMessage = "NameIsRequired")]
		[Remote("IsProductNameExist", "Product", HttpMethod = "Post", ErrorMessage = "Name Is Already Exist")]
		public string Name { get; set; }
		[Range(1, double.MaxValue)]
		public int Price { get; set; }
		[NotMapped]
		public List<IFormFile>? Files { get; set; }
		public int CategoryId { get; set; }

	}
}
