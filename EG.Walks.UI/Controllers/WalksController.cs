using EG.Walks.UI.Models;
using EG.Walks.UI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EG.Walks.UI.Controllers
{
    public class WalksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WalksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> GetAll(string? filterOn, string? filterQuery,
        string? sortBy, bool isAscending = true, int page = 1, int pageSize = 10)
{
    try
    {
        var client = _httpClientFactory.CreateClient();

        // Construct query parameters
        var query = $"?filterOn={filterOn}&filterQuery={filterQuery}&sortBy={sortBy}&isAscending={isAscending}&page={page}&pageSize={pageSize}";
        var response = await client.GetAsync($"https://localhost:7013/api/Walks{query}");

        if (!response.IsSuccessStatusCode)
            return View("Error");

        var walks = await response.Content.ReadFromJsonAsync<List<WalkResponseDto>>();

        // Create the ViewModel
        var viewModel = new WalkFilterViewModel
        {
            Walks = walks,
            FilterOn = filterOn,
            FilterQuery = filterQuery,
            SortBy = sortBy,
            IsAscending = isAscending,
            Page = page,
            PageSize = pageSize,
            TotalPages = 10
        };

        return View(viewModel);
    }
    catch
    {
        return View("Error");
    }
}

    }
}
