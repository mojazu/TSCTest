using System;

namespace TscMauiApp.Models;

public class CountriesResponse
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public List<Country> Records { get; set; }
}
