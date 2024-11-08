using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Extentions
{
	public static class UserMangerExtention
	{
		public static async Task<AppUser> FindEmailWithAddressAsync(this UserManager<AppUser> manager,ClaimsPrincipal User)
		{
			var email= User.FindFirstValue(ClaimTypes.Email);
			var user=await manager.Users.Include(u=> u.address).FirstOrDefaultAsync(x => x.Email == email);
			return user;
		}
	}
}
