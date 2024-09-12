using System;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.ViewModels;

public partial class CountriesPageViewModel : BaseViewModel
{
    public CountriesPageViewModel(INavigationService navigationService, IDialogService dialogService) 
        : base(navigationService, dialogService)
    {
    }
}
