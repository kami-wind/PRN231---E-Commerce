using BusinessObjects_Layer;

namespace Web_Client.APIService;

public class CategoryService
{
    private readonly HttpClient _httpClient;

    public CategoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:7135/api/categories");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Category>>();
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7135/api/categories/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Category>();
    }

    public async Task CreateCategoryAsync(Category category)
    {
        var response = await _httpClient.PostAsJsonAsync("https://localhost:7135/api/categories", category);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateCategoryAsync(int id, Category category)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7135/api/categories/{id}", category);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7135/api/categories/{id}");
        response.EnsureSuccessStatusCode();
    }
}
