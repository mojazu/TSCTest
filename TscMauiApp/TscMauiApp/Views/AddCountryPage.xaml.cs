using TscMauiApp.ViewModels;

namespace TscMauiApp.Views;

public partial class AddCountryPage : ContentPage
{
	public AddCountryPage(AddCountryPageViewModel viewModel)
	{
		BindingContext = viewModel;
		
		InitializeComponent();
	}
}