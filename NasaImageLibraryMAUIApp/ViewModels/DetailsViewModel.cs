using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NasaImageLibraryMAUIApp.Services;

namespace NasaImageLibraryMAUIApp.ViewModels
{
    [QueryProperty(nameof(NasaId), "id")]
    [QueryProperty(nameof(Title), "title")]
    [QueryProperty(nameof(Description), "description")]
    [QueryProperty(nameof(ThumbnailUrl), "thumb")]
    public partial class DetailsViewModel : ObservableObject
    {
        private readonly INasaApiService apiService;

        public DetailsViewModel(INasaApiService apiService)
        {
            this.apiService = apiService;
        }

        [ObservableProperty] string nasaId;
        [ObservableProperty] string title;
        [ObservableProperty] string description;

        [ObservableProperty] string thumbnailUrl;

        [ObservableProperty] string highResImageUrl;
        [ObservableProperty] bool isBusy;

        async partial void OnNasaIdChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            
            await LoadHighResImage(value);
        }

        private async Task LoadHighResImage(string id)
        {
            IsBusy = true;

            HighResImageUrl = ThumbnailUrl;

            var image = await apiService.GetHighResImageUrlAsync(id);

            if(!string.IsNullOrEmpty(image)) HighResImageUrl = image;

            IsBusy = false;
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task ShareImage()
        {
            if (string.IsNullOrEmpty(HighResImageUrl))
                return;

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Uri = HighResImageUrl,              
                Title = "Dela NASA-bild",          
                Text = $"Kolla in den här bilden: {Title}" 
            });
        }
    }
}
