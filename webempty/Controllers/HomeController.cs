using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using webempty.Models;

namespace webempty.Controllers
{
	public class HomeController : Controller
	{
		private int userId = 1;
		private string username = "moho";
		public IActionResult Index()
		{
			//Response.Cookies.Append("userId",userId.ToString());
			//Response.Cookies.Append("username", username);
			//var product = new model1()
			//{
			//	Id = 1,Name="Sugar"
			//};
			//HttpContext.Session.Set("product",JsonSerializer.SerializeToUtf8Bytes(product));
			//HttpContext.Session.SetInt32("userId",1);
			//HttpContext.Session.SetString("username", "Yusuf");


			ViewBag.Title = "Home";
			ViewData["Welcome"] = "Welcome";
			model1 model1 = new model1() { Id = 1, Name = "Yusuf" };

			TempData["userId"] = 1;
			TempData["username"] = "Moho";

			return View(model1);
		}
		public IActionResult Privacy()
		{
			return View();
		}
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
