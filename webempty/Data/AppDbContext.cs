using Microsoft.EntityFrameworkCore;
using webempty.Models;



namespace webempty.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext()
		{

		}
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{

		}
		public DbSet<model1> Product {  get; set; }
		public DbSet<Category> Category { get; set; }

		public DbSet<ProductImages> ProductImages { get; set; }
	}
}
