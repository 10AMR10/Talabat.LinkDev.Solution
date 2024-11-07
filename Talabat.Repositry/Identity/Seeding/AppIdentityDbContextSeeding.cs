using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repositry.Identity.Seeding
{
	public static class AppIdentityDbContextSeeding
	{
		public static async Task SeedIndentityAsync(UserManager<AppUser> userManager)
		{
			if (userManager.Users.Count()==0)
			{
				var user = new AppUser()
				{
					DisplayName = "Amr Khaled",
					Email = "amrkhaled.ak154.ak@gmail.com",
					UserName = "AmrKhaled.ak",
					PhoneNumber = "01227004687"
				};
				await userManager.CreateAsync(user, "P@$$0rd"); // to use CreateAsync() of UserManger we should Excute the constructor it has Interfaces his Implement
			}
		}
	}
}
