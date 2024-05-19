using BreakFast.Catalog.API.Infrastructures;

namespace BreakFast.Catalog.API.Services;

public class CatalogServices(CatalogContext context)
{
	public CatalogContext Context { get; } = context;
}
