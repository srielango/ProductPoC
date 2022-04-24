using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductPoC.Models;
using Newtonsoft.Json;

namespace ProductPoC.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Product>? Products { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://6264155ba6adc673188d5a95.mockapi.io/Products"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    //Products = JsonSerializer.Deserialize<List<Product>>(apiResponse); 
                    Products = JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
        }
    }
}