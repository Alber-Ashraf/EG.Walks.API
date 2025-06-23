using EG.Walks.UI.Models;
using EG.Walks.UI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Reflection;

namespace EG.Walks.UI.Controllers
{
    public class WalksController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public WalksController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? filterOn, string? filterQuery,
        string? sortBy, bool isAscending = true, int page = 1, int pageSize = 10)
        {
            var client = _httpClientFactory.CreateClient();

            // Construct query parameters
            var query = $"?filterOn={filterOn}&filterQuery={filterQuery}&sortBy={sortBy}&isAscending={isAscending}&page={page}&pageSize={pageSize}";
            var response = await client.GetAsync($"https://localhost:7013/api/Walks{query}");

            if (!response.IsSuccessStatusCode)
                return View("Error");

            var walks = await response.Content.ReadFromJsonAsync<List<WalkDto>>();

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
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddWalkViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMassage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7013/api/Walks"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };
            var httpResponseMassage = await client.SendAsync(httpRequestMassage);
            httpResponseMassage.EnsureSuccessStatusCode();

            var responseContent = await httpResponseMassage.Content.ReadFromJsonAsync<WalkDto>();
            if (responseContent is not null)
            {
                return RedirectToAction(nameof(GetAll));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<WalkDto>($"https://localhost:7013/api/Walks/{id}");

            if (response is not null)
            {
                var viewModel = new EditWalkViewModel
                {
                    Id = response.Id,
                    Name = response.Name,
                    Description = response.Description,
                    LengthInKm = response.LengthInKm,
                    WalkImageUrl = response.WalkImageUrl,
                    DifficultyId = response.Difficulty?.Id ?? Guid.Empty,
                    RegionId = response.Region?.Id ?? Guid.Empty
                };

                return View(viewModel);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditWalkViewModel request)
        {
            var client = _httpClientFactory.CreateClient();

            var httpRequestMassage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7013/api/Walks/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMassage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var respose = await httpResponseMessage.Content.ReadFromJsonAsync<EditWalkViewModel>();

            if (respose is not null)
            {
                return RedirectToAction("Edit", "Walks");
            }

            return View();
        }


    }
}
