using TscMauiApp.ViewModels;

namespace TscMauiApp.Views;

public partial class AddSubdivisionPage : ContentPage
{
	public AddSubdivisionPage(AddSubdivisionPageViewModel viewModel)
	{
		BindingContext = viewModel;
		
		InitializeComponent();
	}
}