using Microsoft.EntityFrameworkCore;
using Talabat.Core.repositry.contract;
using Talabat.Repositry;
using Talabat.Repositry.Data;

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
			builder.Services.AddSwaggerGen();
			// AddDbContex it follow sql server package so we should take referance from repositry
			builder.Services.AddDbContext<StoreContex>((option) =>
			{
				option.UseLazyLoadingProxies()
				.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			builder.Services.AddScoped(typeof(IGenericRepositry<>), typeof(GenericRepositry<>));
			#endregion

			var app = builder.Build();
			#region update database explictly
			using var scope=app.Services.CreateScope(); //using keyword => to dispose scope or dispose all object that order from scope
			//container has all services with life time scope
			var service=scope.ServiceProvider;
			//use to take object from this container
			var _dbcontext=service.GetRequiredService<StoreContex>();
			//ask clr to create object from DbContext explictly
			var loggerFactory=service.GetRequiredService<ILoggerFactory>();
			try
			{
				await _dbcontext.Database.MigrateAsync();
				await StoreContextSeeding.SeedingAsync(_dbcontext);
			}
			catch (Exception ex)
			{
				var logger=loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "error when appling migration");
               
            }

			#endregion

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
