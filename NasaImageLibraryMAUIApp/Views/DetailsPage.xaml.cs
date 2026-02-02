using NasaImageLibraryMAUIApp.ViewModels;

namespace NasaImageLibraryMAUIApp.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}