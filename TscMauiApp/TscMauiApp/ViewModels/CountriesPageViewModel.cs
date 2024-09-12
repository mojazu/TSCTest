using System;
using CommunityToolkit.Mvvm.ComponentModel;
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
}
