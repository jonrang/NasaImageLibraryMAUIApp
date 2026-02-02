using Microsoft.Extensions.Logging;
using NasaImageLibraryMAUIApp.Services;
using NasaImageLibraryMAUIApp.ViewModels;
using NasaImageLibraryMAUIApp.Views;

namespace NasaImageLibraryMAUIApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<HttpClient>(sp => new HttpClient
            {
                BaseAddress = new Uri("https://images-api.nasa.gov")
            });
            builder.Services.AddSingleton<INasaApiService, NasaApiService>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<DetailsViewModel>();
            builder.Services.AddTransient<DetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
