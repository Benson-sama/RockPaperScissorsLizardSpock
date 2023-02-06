using Microsoft.Extensions.Logging;
using RockPaperScissorsLizardSpock.Client.Pages;
using RockPaperScissorsLizardSpock.Client.Services;
using RockPaperScissorsLizardSpock.Client.ViewModels;

namespace RockPaperScissorsLizardSpock.Client;

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
            })
            .Services
                .AddTransient<IDialogService, DialogService>()
                .AddTransient<IUrlService, UrlService>()
                .AddSingleton<GameViewModel>()
                .AddSingleton<HomePage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
