using TscMauiApp.ViewModels;

namespace TscMauiApp.Views;

public partial class CountriesPage : ContentPage
{
	public CountriesPage(CountriesPageViewModel viewModel)
	{
		BindingContext = viewModel;
		
		InitializeComponent();
	}

    private void CountryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
		(sender as CollectionView)!.SelectedItem = null;
    }
}