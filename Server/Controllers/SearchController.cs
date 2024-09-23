using GoogleSearchApp.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GoogleSearchApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public SearchController(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string query)
        {
            var apiKey = _configuration["GoogleApiKey"];
            var cx = _configuration["GoogleSearchEngineId"];
            var url = $"https://www.googleapis.com/customsearch/v1?key={apiKey}&cx={cx}&q={System.Uri.EscapeDataString(query)}";

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var searchResponse = JsonSerializer.Deserialize<GoogleSearchResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return Ok(searchResponse);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"Chyba při volání Google API: {ex.Message}");
            }
        }
    }
}
