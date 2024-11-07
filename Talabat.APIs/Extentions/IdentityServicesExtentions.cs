using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Repositry.Identity;

namespace Talabat.APIs.Extentions
{
	public static class IdentityServicesExtentions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services)
		{

			services.AddIdentity<AppUser, IdentityRole>()  //  CreateAsync() => interfaces
				.AddEntityFrameworkStores<AppIdentityDbContext>(); // it implement interfaces 

			services.AddAuthentication(); // allow DI for ( user manger , sign manger, role manger)
			services.AddAuthorization();
			return services;
		}
	}
}
