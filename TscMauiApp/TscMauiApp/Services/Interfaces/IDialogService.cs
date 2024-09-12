using System;

namespace TscMauiApp.Services.Interfaces;

public interface IDialogService
{
    Task ShowAlert(string title, string message, string button);
    Task<bool> ShowAlert(string title, string message, string acceptButton, string cancelButton);
}
