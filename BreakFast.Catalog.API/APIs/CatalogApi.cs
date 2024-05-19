using BreakFast.Catalog.API.Domain.Models;
using BreakFast.Catalog.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BreakFast.Catalog.API.APIs;

public static class CatalogApi
{
	public static IEndpointRouteBuilder MapCatalogApiV1(this IEndpointRouteBuilder app)
	{
		var api = app.MapGroup("api/v1/catalog");

		api.MapGet("/restaurants", GetAllRestaurantsAsync);
		api.MapGet("/restaurants/{id:int}", GetRestaurantByIdAsync);
		api.MapGet("/restaurants/{id:int}/items", GetItemsByRestaurantIdAsync);

		api.MapPut("/items", UpdateItemAsync);
		api.MapPost("/items", CreateItemAsync);
		api.MapDelete("/items/{id:int}", DeleteItemByIdAsync);

		return app;
	}

	public static async Task<Results<Ok<PaginatedItems<Restaurant>>, BadRequest<string>>> GetAllRestaurantsAsync(
		[AsParameters] PaginationRequest paginationRequest,
		[AsParameters] CatalogServices services)
	{
		await Task.Run(() => "that's all");
		var pageSize = paginationRequest.PageSize;
		var pageIndex = paginationRequest.PageIndex;

		var totalItems = await services.Context.Restaurant
							.LongCountAsync();

		var itemsOnPage = await services.Context.Restaurant
							.OrderBy(c => c.Name)
							.Skip(pageSize * pageIndex)
							.Take(pageSize)
							.ToListAsync();

		return TypedResults.Ok(new PaginatedItems<Restaurant>(pageIndex, pageSize, totalItems, itemsOnPage));
	}

	public static async Task<Results<Ok<Restaurant>, NotFound, BadRequest<string>>> GetRestaurantByIdAsync(
		[AsParameters] CatalogServices services,
		int id)
	{
		if (id <= 0)
		{
			return TypedResults.BadRequest("Id is not valid.");
		}

		var item = await services.Context.Restaurant
			.SingleOrDefaultAsync(ci => ci.Id == id);

		if (item == null)
		{
			return TypedResults.NotFound();
		}

		return TypedResults.Ok(item);
	}

	public static async Task<Ok<PaginatedItems<MenuItem>>> GetItemsByRestaurantIdAsync(
		[AsParameters] PaginationRequest paginationRequest,
		[AsParameters] CatalogServices services,
		int id)
	{
		var pageSize = paginationRequest.PageSize;
		var pageIndex = paginationRequest.PageIndex;

		var root = (IQueryable<MenuItem>)services.Context.MenuItem;
		root = root.Where(c => c.RestaurantId == id);

		var totalItems = await root
			.LongCountAsync();

		var itemsOnPage = await root
			.Skip(pageSize * pageIndex)
			.Take(pageSize)
			.ToListAsync();

		return TypedResults.Ok(new PaginatedItems<MenuItem>(pageIndex, pageSize, totalItems, itemsOnPage));
	}

	public static async Task<Results<Created, NotFound<string>>> UpdateItemAsync(
		[AsParameters] CatalogServices services,
		MenuItem productToUpdate)
	{
		var catalogItem = await services.Context.MenuItem.SingleOrDefaultAsync(i => i.Id == productToUpdate.Id);

		if (catalogItem == null)
		{
			return TypedResults.NotFound($"Item with id {productToUpdate.Id} not found.");
		}

		// Update current product
		var catalogEntry = services.Context.Entry(catalogItem);
		catalogEntry.CurrentValues.SetValues(productToUpdate);
		await services.Context.SaveChangesAsync();

		return TypedResults.Created();
	}

	public static async Task<Created> CreateItemAsync(
		[AsParameters] CatalogServices services,
		MenuItem product)
	{
		var item = new MenuItem
		{
			Id = product.Id,
			Description = product.Description,
			Name = product.Name,
			Price = product.Price,
			RestaurantId = product.RestaurantId
		};

		services.Context.MenuItem.Add(item);
		await services.Context.SaveChangesAsync();

		return TypedResults.Created();
	}

	public static async Task<Results<NoContent, NotFound>> DeleteItemByIdAsync(
		[AsParameters] CatalogServices services,
		int id)
	{
		var item = services.Context.MenuItem.SingleOrDefault(x => x.Id == id);

		if (item is null)
		{
			return TypedResults.NotFound();
		}

		services.Context.MenuItem.Remove(item);
		await services.Context.SaveChangesAsync();
		return TypedResults.NoContent();
	}
}
