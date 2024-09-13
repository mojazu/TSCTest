using System;
using Newtonsoft.Json;

namespace TscMauiApp.Models;

public class CountrySubdivision
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
}
