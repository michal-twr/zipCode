using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Core;

namespace Infrastructure;

public class ZippopotamClient : IZipApiClient
{
    private readonly HttpClient _httpClient;

    public ZippopotamClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetZipName(string zipCode)
    {
        var zipData = await _httpClient.GetFromJsonAsync<ZippopotamApiResponse>($"https://api.zippopotam.us/us/{zipCode}");
        return $"{zipData.Places[0].PlaceName} - {zipData.Places[0].State} {zipData.Country}";

    }
}

public class ZippopotamApiResponse
{
    [JsonPropertyName("country abbreviation")]
    public string Country { get; set; }
    
    [JsonPropertyName("places")]
    public IList<ZippopotamApiResponsePlaces> Places { get; set; }
}

public class ZippopotamApiResponsePlaces
{
    [JsonPropertyName("state abbreviation")]
    public string State { get; set; }
    
    [JsonPropertyName("place name")]
    public string PlaceName { get; set; }
    
}
