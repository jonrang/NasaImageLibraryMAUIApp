using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NasaImageLibraryMAUIApp.Models;
using NasaImageLibraryMAUIApp.Services;
using NasaImageLibraryMAUIApp.Views;

namespace NasaImageLibraryMAUIApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly INasaApiService apiService;
        public ObservableCollection<SearchItem> SearchItems { get; set; }
        [ObservableProperty] private string searchQuery;
        [ObservableProperty] private bool isBusy;

        [ObservableProperty] string startYear;
        [ObservableProperty] string endYear;
        [ObservableProperty] bool showFilters;

        private string[] defaultTerms = { "galaxy", "black hole", "earth", "iss", "jupiter" };

        public MainViewModel(INasaApiService apiService)
        {
            this.apiService = apiService;
            SearchItems = new ObservableCollection<SearchItem>();

            var randomTerm = defaultTerms[new Random().Next(defaultTerms.Length)];

            SearchQuery = randomTerm;
            Task.Run(async () => await PerformSearch());
        }

        [RelayCommand]
        private async Task PerformSearch()
        {
            if (string.IsNullOrWhiteSpace(SearchQuery)) return;
            if (IsBusy) return;


            int? start = int.TryParse(StartYear, out int s) ? s : null;
            int? end = int.TryParse(EndYear, out int e) ? e : null;

            if (start.HasValue && end.HasValue && start > end)
            {
                await Shell.Current.DisplayAlertAsync("Invalid Date", "Start Year cannot be after End Year.", "OK");
                return;
            }

            IsBusy = true;

            try
            {

                var query = string.IsNullOrWhiteSpace(SearchQuery) ? defaultTerms[new Random().Next(defaultTerms.Length)] : SearchQuery;

                var result = await apiService.GetSearchResult(query, start, end);

                SearchItems.Clear();
                foreach (var item in result)
                {
                    SearchItems.Add(item);
                }
                ShowFilters = false;
            }
            catch (Exception ex)
            {

                await Shell.Current.DisplayAlertAsync("Error!", $"Unable to get images: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoToDetails(SearchItem item)
        {
            if (item is null) return;

            var navParameter = new Dictionary<string, object>
            {
                {"id", item.Data[0].NasaId },
                {"title", item.Data[0].Title },
                {"description", item.Data[0].Description },
                {"thumb", item.Links[0].Href }
            };

            await Shell.Current.GoToAsync(nameof(DetailsPage), navParameter);
        }

        [RelayCommand]
        private void ToggleFilters()
        {
            ShowFilters = !ShowFilters;
        }
    }
}
