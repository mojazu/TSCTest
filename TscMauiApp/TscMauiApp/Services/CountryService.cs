using System;
using System.Text;
using Newtonsoft.Json;
using TscMauiApp.Models;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.Services;

public class CountryService : ICountryService
{
    private const string BaseUrl = "https://api-exam.tsc-dev.xyz";
    private const string EndpointCountries = "/countries";
    private const string EndpointCountry = "/countries/{0}";
    private const string EndpointStates = "/countries/{0}/states";

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

    public async Task<List<CountrySubdivision>> GetCountrySubdivisions(int countryId)
    {
        var subdivisions = new List<CountrySubdivision>();
        var subdivisionsUri = string.Format(EndpointStates, countryId);
        var uri = new Uri($"{BaseUrl}{subdivisionsUri}");
        try
        {
            HttpResponseMessage response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                subdivisions = JsonConvert.DeserializeObject<List<CountrySubdivision>>(content);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
        return subdivisions;
    }

    public async Task<bool> AddCountry(Country country)
    {
        var uri = new Uri($"{BaseUrl}{EndpointCountries}");
        try
        {
            string json = JsonConvert.SerializeObject(country);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(uri, content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> EditCountry(Country country)
    {
        var countryUri = string.Format(EndpointCountry, country.Id);
        var uri = new Uri($"{BaseUrl}{countryUri}");
        try
        {
            string json = JsonConvert.SerializeObject(country);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(uri, content);

            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteCountry(int countryId)
    {
        var countryUri = string.Format(EndpointCountry, countryId);
        var uri = new Uri($"{BaseUrl}{countryUri}");
        try
        {
            HttpResponseMessage response = await _client.DeleteAsync(uri);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
