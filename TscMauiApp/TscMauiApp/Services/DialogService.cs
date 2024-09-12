using System;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.Services;

public class DialogService : IDialogService
{
    public Task ShowAlert(string title, string message, string button)
    {
        return Shell.Current.DisplayAlert(title, message, button);
    }

    public Task<bool> ShowAlert(string title, string message, string acceptButton, string cancelButton)
    {
        return Shell.Current.DisplayAlert(title, message, acceptButton, cancelButton);
    }
}
