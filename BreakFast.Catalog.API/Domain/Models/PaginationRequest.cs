namespace BreakFast.Catalog.API.Domain.Models;

public record PaginationRequest(int PageSize = 10, int PageIndex = 0);
