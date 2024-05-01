using AutoMapper;
using Microsoft.AspNetCore.Identity;
using webempty.ViewModels.Identity.Roles;

namespace webempty.Mapping
{
	public class RoleProfile:Profile
	{

		public RoleProfile()
		{
			CreateMap<IdentityRole,GetRolesViewModel>();
		}


	}
}
