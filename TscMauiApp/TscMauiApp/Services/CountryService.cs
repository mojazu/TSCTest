using System;
using Newtonsoft.Json;
using TscMauiApp.Models;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.Services;

public class CountryService : ICountryService
{
    private const string BaseUrl = "https://api-exam.tsc-dev.xyz";
    private const string EndpointCountries = "/countries";

    private HttpClient _client;

    public CountryService()
    {
        _client = new HttpClient();
    }

    public async Task<List<Country>> GetCountries()
    {
        var countries = new List<Country>();
        var uri = new Uri($"{BaseUrl}{EndpointCountries}");
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<CountriesResponse>(content);
                countries = jsonResponse?.Records;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return countries;
    }
}
