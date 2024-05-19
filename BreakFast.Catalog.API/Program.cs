using BreakFast.Catalog.API.APIs;
using BreakFast.Catalog.API.Extentsions;
using BreakFast.Catalog.API.Infrastructures;

namespace BreakFast.Catalog.API;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.AddServiceDefaults();
		builder.AddOpenApi();

		// dotnet ef -p BreakFast.Catalog.API -s BreakFast.Catalog.API migrations add "Initial_migration" -o "Infrastructures/Migrations"
		var app = builder.Build();
		app.ConfigureExceptionHandlingMiddleware();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapCatalogApiV1();

		app.Services.ApplyMigration<CatalogContext>();
		app.Run();
	}
}
