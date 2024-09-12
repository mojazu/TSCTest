using TscMauiApp.ViewModels;

namespace TscMauiApp.Views;

public partial class CountriesPage : ContentPage
{
	public CountriesPage(CountriesPageViewModel viewModel)
	{
		BindingContext = viewModel;
		
		InitializeComponent();
	}
}