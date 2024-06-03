using BusinessObjects_Layer;

public class ProductService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7135/api/products";

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        var response = await _httpClient.GetAsync(_baseUrl);
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        return products;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Product>();
    }

    public async Task CreateProductAsync(Product product)
    {
        var response = await _httpClient.PostAsJsonAsync(_baseUrl, product);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Request failed with status code {response.StatusCode} and message: {errorContent}");
        }
    }

    public async Task UpdateProductAsync(int id, Product product)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", product);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Request failed with status code {response.StatusCode} and message: {errorContent}");
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var response = await _httpClient.GetAsync("https://localhost:7135/api/categories");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<IEnumerable<Category>>();
    }

    //public async Task<Category> GetCategoryByIdAsync(int id)
    //{
    //    var response = await _httpClient.GetAsync($"api/categories/{id}");
    //    response.EnsureSuccessStatusCode();
    //    return await response.Content.ReadAsAsync<Category>();
    //}
}
