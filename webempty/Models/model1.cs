using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace webempty.Models
{
	public class model1
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public int Price { get; set; }
		public int CategoryId { get; set; }
		[ForeignKey(nameof(CategoryId))]
		public Category? Category { get; set; }	

		public virtual ICollection<ProductImages> ProductImages { get; set;}=new HashSet<ProductImages>();

	}
}
