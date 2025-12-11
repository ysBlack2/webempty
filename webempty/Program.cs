using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using webempty.Data;
using webempty.Mapping;
using webempty.Repositories.Implementations;
using webempty.Repositories.Interfaces;
using webempty.Resources;
using webempty.Services.Implementations;

using webempty.Services.Interfaces;
using webempty.SharedRepositories;

var builder = WebApplication.CreateBuilder(args);

var var1 = builder.Configuration["Key1"];
var var2 = builder.Configuration["Key"];
//var var3 = builder.Configuration.GetSection("ConnectionStrings:test").Value;
var space = "\n";

//builder.Services.AddAuthentication();
//builder.Services.AddAuthorization();

//builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddDbContext<AppDbContext>(options =>options.UseNpgsql(builder.Configuration["ConnectionStrings:DbConnections"]));

builder.Services.AddAutoMapper(typeof(ProductProfile));



builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient <IProductImagesRepository, ProductImagesRepository>();
builder.Services.AddTransient <ICategoryRepository, CategoryRepository>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddDistributedMemoryCache();


builder.Services.AddSession(options=>
{
	options.IOTimeout=TimeSpan.FromMinutes(5);
	options.IdleTimeout=TimeSpan.FromMinutes(5);
	options.Cookie.Path = "/";
	options.Cookie.IsEssential = true;
	options.Cookie.HttpOnly= true;
	options.Cookie.Name = ".webempty";


}



);


#region Localization
builder.Services.AddControllersWithViews()
				.AddViewLocalization().AddDataAnnotationsLocalization(options =>
				{
					options.DataAnnotationLocalizerProvider = (type, factory) =>
					factory.Create(typeof(SharedResources));
				});
	
builder.Services.AddLocalization(opt =>
{
	opt.ResourcesPath = "";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	List<CultureInfo> supportedCultures = new List<CultureInfo>
		{
			new CultureInfo("en-US"),
			new CultureInfo("ar-EG"),

		};

	options.DefaultRequestCulture = new RequestCulture("en-US");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});
#endregion

var app = builder.Build();
app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options!.Value);

#endregion
//app.MapDefaultControllerRoute();
//app.MapControllerRoute(name: "default", pattern: "Product/{*Index}", defaults: new { controller = "Product", action = "Index" });

app.MapControllerRoute(name:"default", pattern:"{Controller=Home}/{Action=Index}/{Id?}");

//app.MapGet("/", () => var1 + space + var2+space + var3);

app.Run();
Console.WriteLine("Runtime DB: " + builder.Configuration.GetConnectionString("DefaultConnection"));
