using Microsoft.EntityFrameworkCore;
using webempty.Models;
using webempty.ViewModels.Categories;
using webempty.ViewModels.Identity.Roles;



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

		public DbSet<webempty.ViewModels.Categories.GetCategoriesListViewModel> GetCategoriesListViewModel { get; set; } = default!;

		public DbSet<webempty.ViewModels.Categories.GetCategoryByIdViewModel> GetCategoryByIdViewModel { get; set; } = default!;

		public DbSet<webempty.ViewModels.Identity.Roles.GetRolesViewModel> GetRolesViewModel { get; set; } = default!;
	}
}
