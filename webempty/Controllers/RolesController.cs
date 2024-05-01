using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using webempty.ViewModels.Identity.Roles;

namespace webempty.Controllers
{
	public class RolesController:Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IMapper _mapper;
		public RolesController(RoleManager<IdentityRole> roleManager, IMapper mapper)
		{
			_roleManager = roleManager;
			_mapper = mapper;
		}
		public IActionResult Index()
		{
			var roles = _roleManager.Roles.ToList();
			var result = _mapper.Map<List<GetRolesViewModel>>(roles);
			return View(result);
		}
	}
}
