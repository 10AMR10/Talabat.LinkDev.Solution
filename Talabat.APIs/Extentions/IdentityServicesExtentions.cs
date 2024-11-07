using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;
using Talabat.Core.service.contract;
using Talabat.Repositry.Identity;
using Talabat.Service;

namespace Talabat.APIs.Extentions
{
	public static class IdentityServicesExtentions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services)
		{
			services.AddScoped<ITokentService, TokentService>();
			services.AddIdentity<AppUser, IdentityRole>()  //  CreateAsync() => interfaces
				.AddEntityFrameworkStores<AppIdentityDbContext>(); // it implement interfaces 

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(); // allow DI for ( user manger , sign manger, role manger)
			
			return services;
		}
	}
}
