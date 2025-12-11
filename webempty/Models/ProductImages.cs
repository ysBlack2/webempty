using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webempty.Models
{
	public class ProductImages
	{
		[Key]
		public int Id { get; set; }
		public string Path { get; set; }

		public int ProductId { get; set; }

		[ForeignKey(nameof(ProductId))]
		public model1? Product { get; set; }
	}
}
