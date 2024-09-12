using System;

namespace TscMauiApp.Services.Interfaces;

public interface INavigationService
{
    Task NavigateToAsync(string route, IDictionary<string, object>? routeParameters = null);
}
