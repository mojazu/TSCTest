using System;
using Newtonsoft.Json;

namespace TscMauiApp.Models;

public class Country
{
    [JsonProperty("id")]
    public int? Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("alpha_2")]
    public string? Alpha2 { get; set; }

    [JsonProperty("alpha_3")]
    public string? Alpha3 { get; set; }

    [JsonProperty("code")]
    public string? Code { get; set; }

    [JsonProperty("iso_3166_2")]
    public string? Iso3166_2 { get; set; }
}
