using System;
using TscMauiApp.Models;

namespace TscMauiApp.Services.Interfaces;

public interface ICountryService
{
    public Task<List<Country>> GetCountries();
    public Task<List<CountrySubdivision>> GetCountrySubdivisions(int countryId);
}
