using System;
using TscMauiApp.Models;

namespace TscMauiApp.Services.Interfaces;

public interface ICountryService
{
    public Task<List<Country>> GetCountries();
    public Task<List<CountrySubdivision>> GetCountrySubdivisions(int countryId);
    public Task<bool> AddCountry(Country country);
    public Task<bool> EditCountry(Country country);
    public Task<bool> DeleteCountry(int countryId);
}
