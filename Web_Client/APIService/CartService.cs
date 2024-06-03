using BusinessObjects_Layer;

namespace Web_Client.APIService;

public class CartService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7135/api/cart"; // Replace with your API URL

    public CartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task AddToCartAsync(Cart cart)
    {
        var response = await _httpClient.PostAsJsonAsync(_baseUrl, cart);
        response.EnsureSuccessStatusCode();
    }
}
