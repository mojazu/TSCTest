using TscMauiApp.ViewModels;

namespace TscMauiApp.Views;

public partial class CountryDetailPage : ContentPage
{
	public CountryDetailPage(CountryDetailPageViewModel viewModel)
	{
		BindingContext = viewModel;
		
		InitializeComponent();
	}
}