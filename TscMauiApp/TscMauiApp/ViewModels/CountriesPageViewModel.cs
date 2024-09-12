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
        }
    }

    [RelayCommand]
    private async Task CountrySelectedAsync(Country country)
    {
        if (country == null) return;

        //TODO: CountryDetail navigation
    }

    [RelayCommand]
    private async Task AddNewCountryAsync()
    {
        //TODO: AddNewCountry navigation
    }

    [RelayCommand]
    private async Task EditCountryAsync(Country country)
    {
        //TODO: EditCountry navigation
    }

    [RelayCommand]
    private async Task DeleteCountryAsync(Country country)
    {
        var result = await _dialogService.ShowAlert("Delete Country", $"Are you sure you want to delete {country.Name}?", "Delete", "Cancel");
        if (!result) return;

        //TODO: Delete country and refresh list.
    }
}
