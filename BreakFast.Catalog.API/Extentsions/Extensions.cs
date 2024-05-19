using BreakFast.Catalog.API.Infrastructures;
using Microsoft.EntityFrameworkCore;

namespace BreakFast.Catalog.API.Extentsions;

public static partial class Extensions
{
	public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
	{
		// Add services to the container.
		builder.Services.AddAuthorization();

		builder.Services.AddDbContextPool<CatalogContext>(optionsBuilder =>
		{
			var connectionString = builder.Configuration.GetConnectionString("SQLExpress");
			optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
			{
				sqlOptions.EnableRetryOnFailure(
					maxRetryCount: 10,
					maxRetryDelay: TimeSpan.FromSeconds(30),
					errorNumbersToAdd: null);
			});
		});

		builder.Services.AddScoped<CatalogContext>();

		return builder;
	}
}
