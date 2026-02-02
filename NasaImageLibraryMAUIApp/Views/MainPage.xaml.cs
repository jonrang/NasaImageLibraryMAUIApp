using NasaImageLibraryMAUIApp.ViewModels;

namespace NasaImageLibraryMAUIApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }
}
