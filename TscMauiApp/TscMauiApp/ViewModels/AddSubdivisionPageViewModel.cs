using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TscMauiApp.Models;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.ViewModels;

[QueryProperty(nameof(Country), "Country")]
[QueryProperty(nameof(Subdivision), "Subdivision")]
public partial class AddSubdivisionPageViewModel : BaseViewModel
{
    private readonly ICountryService _countryService;

    [ObservableProperty]
    private bool _isEditing = false;

    [ObservableProperty]
    private string _title = "Add Subdivision";

    [ObservableProperty]
    private Country _country;

    [ObservableProperty]
    private CountrySubdivision _subdivision;

    [ObservableProperty]
    private string _name;

    public AddSubdivisionPageViewModel(INavigationService navigationService, IDialogService dialogService, ICountryService countryService)
        : base(navigationService, dialogService)
    {
        _countryService = countryService;
    }

    partial void OnSubdivisionChanging(CountrySubdivision value)
    {
        if (value == null) return;
        IsEditing = true;
        Title = "Edit Subdivision";
        Name = value.Name;
    }

    [RelayCommand]
    private async Task SaveSubdivisionAsync()
    {
        if (!Validate())
        {
            await _dialogService.ShowAlert("Error", "The subdivision name is required.", "OK");
            return;
        }

        if (!IsEditing)
        {
            await AddSubdivisionAsync();
        }
        else
        {
            await EditSubdivisionAsync();
        }
    }

    private async Task AddSubdivisionAsync()
    {
        try
        {
            IsBusy = true;

            var subdivision = new CountrySubdivision
            {
                Name = Name
            };
            var success = await _countryService.AddSubdivision(subdivision, Country.Id.Value);

            IsBusy = false;

            if (!success)
            {
                await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
                return;
            }

            await _dialogService.ShowAlert("Success", "The subdivision was successfully added.", "OK");
            await _navigationService.PopAsync(forceRefresh: true);
        }
        catch (Exception)
        {
            await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
            IsBusy = false;
        }
    } 

    private async Task EditSubdivisionAsync()
    {
        try
        {
            IsBusy = true;
            Subdivision.Name = Name;
            var success = await _countryService.EditSubdivision(Subdivision, Country);
            IsBusy = false;

            if (!success)
            {
                await _dialogService.ShowAlert("Error", "An error has occurred.", "OK");
                return;
            }

            await _dialogService.ShowAlert("Success", "The subdivision was successfully edited.", "OK");
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
