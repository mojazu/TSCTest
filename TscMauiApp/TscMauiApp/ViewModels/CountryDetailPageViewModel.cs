using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TscMauiApp.Models;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.ViewModels;

[QueryProperty(nameof(Country), "Country")]
public partial class CountryDetailPageViewModel : BaseViewModel
{
    private readonly ICountryService _countryService;

    [ObservableProperty]
    private Country _country;

    [ObservableProperty]
    private List<CountrySubdivision> _subdivisions;

    public CountryDetailPageViewModel(INavigationService navigationService, IDialogService dialogService, ICountryService countryService)
        : base(navigationService, dialogService)
    {
        _countryService = countryService;
    }

    async partial void OnCountryChanged(Country value)
    {
        await LoadSubdivisions();
    }

    private async Task LoadSubdivisions()
    {
        try
        {
            IsBusy = true;
            var subdivisions = await _countryService.GetCountrySubdivisions(Country.Id.Value);
            IsBusy = false;

            if (subdivisions != null)
            {
                Subdivisions = subdivisions;
            }
        }
        catch (Exception)
        {
            await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
        }
    }

    [RelayCommand]
    private async Task AddSubdivisionAsync()
    {
        await _navigationService.NavigateToAsync("/AddSubdivision");
    }

    [RelayCommand]
    private async Task EditSubdivisionAsync(CountrySubdivision subdivision)
    {
        Console.WriteLine("PENDING - Edit subdivision:");
    }

    [RelayCommand]
    private async Task DeleteSubdivisionAsync(CountrySubdivision subdivision)
    {
        var result = await _dialogService.ShowAlert("Delete Subdivision", $"Are you sure you want to delete {subdivision.Name}?", "Delete", "Cancel");
        if (!result) return;

        Console.WriteLine("PENDING - Delete subdivision:");
    }
}
