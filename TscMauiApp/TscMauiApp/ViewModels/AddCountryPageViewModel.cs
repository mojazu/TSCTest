using System;
using CommunityToolkit.Mvvm.ComponentModel;
using TscMauiApp.Models;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.ViewModels;

[QueryProperty(nameof(Country), "Country")]
public partial class AddCountryPageViewModel : BaseViewModel
{
    private readonly ICountryService _countryService;

    [ObservableProperty]
    private bool _isEditing = false;

    [ObservableProperty]
    private string _title = "Add Country";

    //Holds the country only while editing. Expected from navigation parameters.
    [ObservableProperty]
    private Country _country;

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string? _alpha2;

    [ObservableProperty]
    private string? _alpha3;

    [ObservableProperty]
    private string? _areaCode;

    [ObservableProperty]
    private string? _iso3166_2;

    public AddCountryPageViewModel(INavigationService navigationService, IDialogService dialogService, ICountryService countryService)
        : base(navigationService, dialogService)
    {
        _countryService = countryService;
    }

    partial void OnCountryChanging(Country value)
    {
        if (value == null) return;
        IsEditing = true;
        Title = "Edit Country";
        Name = value.Name;
        Alpha2 = value.Alpha2;
        Alpha3 = value.Alpha3;
        AreaCode = value.Code;
        Iso3166_2 = value.Iso3166_2;
    }
}
