using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extentions;
using Talabat.APIs.Helpers;
using Talabat.APIs.Middelwares;
using Talabat.Core;
using Talabat.Core.Entities.Identity;
using Talabat.Core.repositry.contract;
using Talabat.Core.service.contract;
using Talabat.Repositry;
using Talabat.Repositry.Data;
using Talabat.Repositry.Identity;
using Talabat.Repositry.Identity.Seeding;
using Talabat.Service;

namespace Talabat.APIs
{
	public class Program
	{
		
		public async static Task Main(string[] args)
		{

			#region Configuration Service
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.CustomSchemaIds(type => type.FullName); // This uses the full namespace and class name as the schemaId
			});
			// AddDbContex it follow sql server package so we should take referance from repositry
			builder.Services.AddDbContext<StoreContex>((option) =>
			{
				option.UseLazyLoadingProxies()
				.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
				option.EnableSensitiveDataLogging();
				option.LogTo(Console.WriteLine);
			});
			builder.Services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddScoped<IOrderServices, OrderServices>();
			builder.Services.AddScoped<IPaymentService, PaymentService>();

			builder.Services.AddAutoMapper(typeof(MappingProfiles));
			builder.Services.Configure<ApiBehaviorOptions>((option) =>
			{
				option.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var Errors = actionContext.ModelState.Where(x => x.Value.Errors.Count() > 0)
						.SelectMany(e => e.Value.Errors)
						.Select(m => m.ErrorMessage).ToList();
					var response = new ValidationErrorApiResponse()
					{
						errors = Errors
					};
					return new BadRequestObjectResult(response);
				};
			});
			builder.Services.AddSingleton<IConnectionMultiplexer>((option) =>
			{
				var connection = builder.Configuration.GetConnectionString("RedisConneciton"); // get the conection string 
				return ConnectionMultiplexer.Connect(connection); // make connection with redis
			});
			builder.Services.AddScoped(typeof(IBasketRepository),typeof(BasketRepository));
			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			});
			builder.Services.AddIdentityServices(builder.Configuration);
			
			#endregion

			var app = builder.Build();
			#region update database explictly
			using var scope = app.Services.CreateScope(); //using keyword => to dispose scope or dispose all object that order from scope
														  //container has all services with life time scope
			var service = scope.ServiceProvider;
			//use to take object from this container
			var _dbcontext = service.GetRequiredService<StoreContex>();
			var _IdentityDbContest = service.GetRequiredService<AppIdentityDbContext>();
			//ask clr to create object from DbContext explictly
			var loggerFactory = service.GetRequiredService<ILoggerFactory>();
			try
			{
				var userManger=service.GetRequiredService<UserManager<AppUser>>();	
				await _dbcontext.Database.MigrateAsync();
				await _IdentityDbContest.Database.MigrateAsync();
				await AppIdentityDbContextSeeding.SeedIndentityAsync(userManger);
				await StoreContextSeeding.SeedingAsync(_dbcontext);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "error when appling migration");

			}

			#endregion

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMiddleware<ExceptionMiddleware>();
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();
			app.UseStatusCodePagesWithReExecute("/errors/{0}");

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
