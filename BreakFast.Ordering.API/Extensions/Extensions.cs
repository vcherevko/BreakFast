using BreakFast.Ordering.API.Services;
using BreakFast.Ordering.Domain.Repositories;
using BreakFast.Ordering.Infrastructure;
using BreakFast.Ordering.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BreakFast.Ordering.API.Extensions;

public static partial class Extensions
{
	public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
	{
		// Add services to the container.
		builder.Services.AddControllers();
		builder.Services.AddAuthorization();

		builder.Services.AddScoped<IServiceManager, ServiceManager>();
		builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

		builder.Services.AddDbContextPool<OrderingContext>(optionsBuilder =>
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

		return builder;
	}
}
