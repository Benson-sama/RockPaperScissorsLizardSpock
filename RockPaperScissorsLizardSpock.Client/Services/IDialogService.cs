namespace RockPaperScissorsLizardSpock.Client.Services;

public interface IDialogService
{
    Task ShowMessageAsync(string message, string? title = null);
}
