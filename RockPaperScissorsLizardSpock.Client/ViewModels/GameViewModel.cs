using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;
using RockPaperScissorsLizardSpock.Client.Services;

namespace RockPaperScissorsLizardSpock.Client.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private readonly IDispatcher _dispatcher;
    private readonly IDialogService _dialogService;
    private readonly IUrlService _urlService;
    private HubConnection? _hubConnection;

    public GameViewModel(IDispatcher dispatcher, IDialogService dialogService, IUrlService urlService)
    {
        _dispatcher = dispatcher;
        _dialogService = dialogService;
        _urlService = urlService;
    }

    [RelayCommand]
    public async Task Connect()
    {
        await CloseConnectionAsync();
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(_urlService.GameHubAddress)
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.Closed += HubConnectionClosed;

        try
        {
            await _hubConnection.StartAsync();
            await _dialogService.ShowMessageAsync(title: "Game Server", message: "Connected.");
        }
        catch (HttpRequestException ex)
        {
            await _dialogService.ShowMessageAsync(ex.Message);
        }
    }

    private Task HubConnectionClosed(Exception? arg)
    {
        return _dispatcher.DispatchAsync(()
            => _dialogService.ShowMessageAsync(title: "Game Server", message: "Connection closed."));
    }

    private ValueTask CloseConnectionAsync() =>
        _hubConnection?.DisposeAsync() ?? ValueTask.CompletedTask;

}
