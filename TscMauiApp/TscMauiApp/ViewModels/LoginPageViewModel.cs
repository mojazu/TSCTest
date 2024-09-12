using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.ViewModels;

public partial class LoginPageViewModel : BaseViewModel
{
    private const string TestValidEmail = "test@domain.com";
    private const string TestValidPassword = "abc123";

    [ObservableProperty]
    private string? _email;

    [ObservableProperty]
    private string? _password;

    public LoginPageViewModel(INavigationService navigationService, IDialogService dialogService)
        : base(navigationService, dialogService)
    {
        // Email = TestValidEmail;
        // Password = TestValidPassword;
    }

    [RelayCommand]
    private async Task LogInAsync()
    {
        if (!ValidateUser())
        {
            await _dialogService.ShowAlert("Error", "Invalid credentials.", "OK");
            return;
        }
        //TODO: Navigate to countries page
    }

    private bool ValidateUser()
    {
        return TestValidEmail.Equals(Email) && TestValidPassword.Equals(Password);
    }
}
