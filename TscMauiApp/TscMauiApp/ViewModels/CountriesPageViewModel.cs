using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TscMauiApp.Models;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.ViewModels;

public partial class CountriesPageViewModel : BaseViewModel
{
    private readonly ICountryService _countryService;

    [ObservableProperty]
    private List<Country> _countries;

    public CountriesPageViewModel(INavigationService navigationService, IDialogService dialogService, ICountryService countryService)
        : base(navigationService, dialogService)
    {
        _countryService = countryService;

        _ = LoadCountries();
    }

    private async Task LoadCountries()
    {
        try
        {
            IsBusy = true;
            var countries = await _countryService.GetCountries();
            IsBusy = false;

            if (countries != null)
            {
                Countries = countries;
            }
        }
        catch (Exception)
        {
            await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
            IsBusy = false;
            _ = LoadCountries();
        }
    }

    [RelayCommand]
    private async Task CountrySelectedAsync(Country country)
    {
        if (country == null) return;

        await _navigationService.NavigateToAsync(
            "CountryDetail",
            new Dictionary<string, object> { { "Country", country } });
    }

    [RelayCommand]
    private async Task AddNewCountryAsync()
    {
        await _navigationService.NavigateToAsync("/AddCountry");
    }

    [RelayCommand]
    private async Task EditCountryAsync(Country country)
    {
        await _navigationService.NavigateToAsync(
            "/AddCountry",
            new Dictionary<string, object> { { "Country", country } });
    }

    [RelayCommand]
    private async Task DeleteCountryAsync(Country country)
    {
        var result = await _dialogService.ShowAlert("Delete Country", $"Are you sure you want to delete {country.Name}?", "Delete", "Cancel");
        if (!result) return;

        try
        {
            IsBusy = true;
            var success = await _countryService.DeleteCountry(country.Id);
            IsBusy = false;

            if (!success)
            {
                await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
                return;
            }
            _ = LoadCountries();
        }
        catch (Exception)
        {
            await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
            IsBusy = false;
        }
    }
}
