using Microsoft.EntityFrameworkCore;
using Talabat.Repositry.Data;

namespace Talabat.APIs
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//hell session1
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
				option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});
			#endregion

			var app = builder.Build();

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
