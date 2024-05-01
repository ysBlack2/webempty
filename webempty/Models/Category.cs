using System.ComponentModel.DataAnnotations;

namespace webempty.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<model1> Products { get; set;}=new HashSet<model1>();


	}
}
