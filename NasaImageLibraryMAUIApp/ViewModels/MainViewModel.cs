using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NasaImageLibraryMAUIApp.Models;
using NasaImageLibraryMAUIApp.Services;

namespace NasaImageLibraryMAUIApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly INasaApiService apiService;
        public ObservableCollection<SearchItem> SearchItems { get; set; }
        [ObservableProperty] private string searchQuery;
        [ObservableProperty] private bool isBusy;
        private string[] defaultTerms = { "galaxy", "black hole", "astronaut", "iss", "jupiter" };
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
            if (IsBusy) return;
            IsBusy = true;

            try
            {
                var query = string.IsNullOrWhiteSpace(SearchQuery) ? defaultTerms[new Random().Next(defaultTerms.Length)] : SearchQuery;

                var result = await apiService.GetSearchResult(query);

                SearchItems.Clear();
                foreach (var item in result)
                {
                    SearchItems.Add(item);
                }
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
    }
}
