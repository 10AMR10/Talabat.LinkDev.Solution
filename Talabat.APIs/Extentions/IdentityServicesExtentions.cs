using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.Identity;
using Talabat.Core.service.contract;
using Talabat.Repositry.Identity;
using Talabat.Service;

namespace Talabat.APIs.Extentions
{
	public static class IdentityServicesExtentions
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddScoped<ITokentService, TokentService>();
			services.AddIdentity<AppUser, IdentityRole>()  //  to the dependency injection (DI) container.
				.AddEntityFrameworkStores<AppIdentityDbContext>(); // This line tells Identity to use Entity Framework Core for storing user and role data in the database.

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["JWT:ValidIssuer"],
						ValidateAudience = true,
						ValidAudience = configuration["JWT:validAudience"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]))
					};
				}); // allow DI for ( user manger , sign manger, role manger)
			
			return services;
		}
	}
}
