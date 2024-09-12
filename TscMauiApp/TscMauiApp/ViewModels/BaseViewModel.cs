using System;
using CommunityToolkit.Mvvm.ComponentModel;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    protected readonly INavigationService _navigationService;
    protected readonly IDialogService _dialogService;

    [ObservableProperty]
    private bool _isBusy;

    public BaseViewModel(INavigationService navigationService, IDialogService dialogService){
        _navigationService = navigationService;
        _dialogService = dialogService;
    }
}
