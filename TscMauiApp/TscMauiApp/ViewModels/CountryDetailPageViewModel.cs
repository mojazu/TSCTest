using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TscMauiApp.Models;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.ViewModels;

[QueryProperty(nameof(ShouldRefresh), "refresh")]
[QueryProperty(nameof(CountryParam), "CountryParam")]
public partial class CountryDetailPageViewModel : BaseViewModel
{
    private readonly ICountryService _countryService;

    [ObservableProperty]
    private Country _countryParam;

    [ObservableProperty]
    private Country _country;

    [ObservableProperty]
    private List<CountrySubdivision> _subdivisions;

    [ObservableProperty]
    private bool _shouldRefresh;

    public CountryDetailPageViewModel(INavigationService navigationService, IDialogService dialogService, ICountryService countryService)
        : base(navigationService, dialogService)
    {
        _countryService = countryService;
    }

    partial void OnCountryParamChanged(Country value)
    {
        if(value == null) return;
        Country = value;
        _ = LoadSubdivisions();
    }

    partial void OnShouldRefreshChanged(bool value)
    {
        if (value)
        {
            _ = LoadSubdivisions();
        }
    }

    private async Task LoadSubdivisions()
    {
        try
        {
            if (Country == null) return;

            IsBusy = true;
            var subdivisions = await _countryService.GetCountrySubdivisions(Country.Id.Value);
            IsBusy = false;

            if (subdivisions != null)
            {
                Subdivisions = subdivisions;
            }
            ShouldRefresh = false;
        }
        catch (Exception)
        {
            await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
        }
    }

    [RelayCommand]
    private async Task AddSubdivisionAsync()
    {
        await _navigationService.NavigateToAsync(
            "AddSubdivision",
            new Dictionary<string, object> { { "Country", Country } });        
    }

    [RelayCommand]
    private async Task EditSubdivisionAsync(CountrySubdivision subdivision)
    {
        await _navigationService.NavigateToAsync(
            "AddSubdivision",
            new Dictionary<string, object> { 
                { "Country", Country },
                { "Subdivision", subdivision }
            }); 
    }

    [RelayCommand]
    private async Task DeleteSubdivisionAsync(CountrySubdivision subdivision)
    {
        var result = await _dialogService.ShowAlert("Delete Subdivision", $"Are you sure you want to delete {subdivision.Name}?", "Delete", "Cancel");
        if (!result) return;

        Console.WriteLine("PENDING - Delete subdivision:");
    }
}
