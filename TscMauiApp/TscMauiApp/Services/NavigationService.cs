using System;
using TscMauiApp.Services.Interfaces;

namespace TscMauiApp.Services;

public class NavigationService : INavigationService
{
    public Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null)
    {
        return routeParameters != null
            ? Shell.Current.GoToAsync(route, routeParameters)
            : Shell.Current.GoToAsync(route);
    }

    public Task PopAsync(bool forceRefresh = false)
    {
        return forceRefresh
            ? Shell.Current.GoToAsync($"..?refresh={forceRefresh}")
            : Shell.Current.GoToAsync($"..");
    }
}
