using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BreakFast.Ordering.Infrastructure;

public static class MigrationManager
{
	public static void ApplyMigration<TContext>(this IServiceProvider app)
		where TContext : DbContext
	{
		using var scope = app.CreateScope();
		var scopeServices = scope.ServiceProvider;
		var logger = scopeServices.GetRequiredService<ILogger<TContext>>();
		var dbContext = scopeServices.GetRequiredService<TContext>();

		try
		{
			if (!dbContext.Database.GetPendingMigrations().Any())
			{
				return;
			}

			logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

			dbContext.Database.Migrate();
		}
		catch (Exception e)
		{
			logger.LogError(e, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
			throw;
		}
	}
}
