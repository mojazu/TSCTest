using TscMauiApp.ViewModels;

namespace TscMauiApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		BindingContext = viewModel;
		
		InitializeComponent();
	}
}