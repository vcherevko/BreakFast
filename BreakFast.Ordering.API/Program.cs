using BreakFast.Ordering.API.Extensions;
using BreakFast.Ordering.Infrastructure;

namespace BreakFast.Ordering.API;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.AddServiceDefaults();

		builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
		builder.Services.AddProblemDetails();
		builder.AddOpenApi();

		var app = builder.Build();

		app.UseDefaultOpenApi();
		if (app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseHsts();
		}

		app.UseExceptionHandler();
		app.UseHttpsRedirection();
		app.UseAuthorization();
		app.MapControllers();
		app.Services.ApplyMigration<OrderingContext>();
		app.Run();
	}
}
