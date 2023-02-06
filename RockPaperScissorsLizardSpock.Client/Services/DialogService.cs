namespace RockPaperScissorsLizardSpock.Client.Services;

public class DialogService : IDialogService
{
    public async Task ShowMessageAsync(string message, string? title = null)
    {
        if (Application.Current is null || Application.Current.MainPage is null)
            return;

        await Application.Current.MainPage.DisplayAlert(title ?? "", message, "Oak");
    }
}
