
using Microsoft.EntityFrameworkCore;
using WEBAPI002.Models;

namespace WEBAPI002
{
	public class Program
	{
		private const string nameContext = "Connection";
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString(nameContext);
			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<Test003Context>(option => {
				option.UseSqlServer(connectionString);
			});
			//AddCorse
			builder.Services.AddCors(options =>{
				options.AddPolicy("corseAngular", builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseCors("corseAngular");
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
