using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    [RelayCommand]
    private async Task SaveCountryAsync()
    {
        if (!Validate())
        {
            await _dialogService.ShowAlert("Error", "The country name is required.", "OK");
            return;
        }

        if (!IsEditing)
        {
            await AddCountryAsync();
        }
        else
        {
            await EditCountryAsync();
        }
    }

    private async Task AddCountryAsync()
    {
        try
        {
            IsBusy = true;

            var country = new Country
            {
                Name = Name,
                Alpha2 = Alpha2,
                Alpha3 = Alpha3,
                Code = AreaCode,
                Iso3166_2 = Iso3166_2
            };
            var success = await _countryService.AddCountry(country);

            IsBusy = false;

            if (!success)
            {
                await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
                return;
            }

            await _dialogService.ShowAlert("Success", "The country was successfully added.", "OK");
            await _navigationService.PopAsync(forceRefresh: true);
        }
        catch (Exception)
        {
            await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
            IsBusy = false;
        }
    }

    private async Task EditCountryAsync()
    {
        try
        {
            IsBusy = true;

            Country.Name = Name;
            Country.Alpha2 = Alpha2;
            Country.Alpha3 = Alpha3;
            Country.Code = AreaCode;
            Country.Iso3166_2 = Iso3166_2;

            var success = await _countryService.EditCountry(Country);

            IsBusy = false;

            if (!success)
            {
                await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
                return;
            }

            await _dialogService.ShowAlert("Success", "The country was successfully edited.", "OK");
            await _navigationService.PopAsync(forceRefresh: true);
        }
        catch (Exception)
        {
            await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
            IsBusy = false;
        }
    }

    private bool Validate()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }
}
