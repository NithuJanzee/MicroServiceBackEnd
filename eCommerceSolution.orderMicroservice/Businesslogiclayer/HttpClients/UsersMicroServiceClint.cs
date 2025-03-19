using System.Net.Http.Json;
using eCommerce.OrderMicroservice.BusinessLogicLayer.DTO;

namespace eCommerce.OrderMicroservice.Businesslogiclayer.HttpClients;

public class UsersMicroServiceClint
{
    private readonly HttpClient _httpClient;

    public UsersMicroServiceClint(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDTO?> GetUserByUserId(Guid UserID)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"api/Users?id={UserID}");
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException($"Http request failed with status code {response.StatusCode}");
            }
            else
            {
                throw new HttpRequestException($"Http request failed with status code {response.StatusCode}");
            }
        }
        UserDTO? user = await response.Content.ReadFromJsonAsync<UserDTO>();
        if (user == null)
        {
            throw new ArgumentException("Invalid User ID");
        }
        return user;
    }
}
