using System.ComponentModel.DataAnnotations;

namespace webempty.ViewModels.Categories
{
	public class AddCategoryViewModel
	{
		[Required]
		public string Name { get; set; }
	
	}
}
