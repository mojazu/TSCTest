﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TscMauiApp.Services;
using TscMauiApp.Services.Interfaces;
using TscMauiApp.ViewModels;
using TscMauiApp.Views;

namespace TscMauiApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.RegisterServices()
			.RegisterViews()
			.RegisterViewModels();

		RegisterRoutes();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<INavigationService, NavigationService>();
		builder.Services.AddSingleton<IDialogService, DialogService>();
		builder.Services.AddSingleton<ICountryService, CountryService>();

		return builder;
	}

	public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<CountriesPage>();

		return builder;
	}

	public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
	{
		builder.Services.AddTransient<LoginPageViewModel>();
		builder.Services.AddTransient<CountriesPageViewModel>();

		return builder;
	}

	private static void RegisterRoutes()
	{
		Routing.RegisterRoute("Login", typeof(LoginPage));
		Routing.RegisterRoute("Countries", typeof(CountriesPage));
	}
}
