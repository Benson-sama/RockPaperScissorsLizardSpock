using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using RockPaperScissorsLizardSpock.Client.Services;
using RockPaperScissorsLizardSpock.Model.SignalR;

namespace RockPaperScissorsLizardSpock.Client.ViewModels;

[ObservableObject]
public partial class GameViewModel : IGameClient
{
    private readonly IDispatcher _dispatcher;
    private readonly IDialogService _dialogService;
    private readonly IUrlService _urlService;
    private HubConnection? _hubConnection;

    // TODO: Make customisable in UI.
    [ObservableProperty]
    private string _username = "Silverstone";

    [ObservableProperty]
    private IEnumerable<string> _playerList = Enumerable.Empty<string>();

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
        _hubConnection.On<IEnumerable<string>>(nameof(ReceiveCurrentPlayerList), ReceiveCurrentPlayerList);

        try
        {
            await _hubConnection.StartAsync();
            await _dialogService.ShowMessageAsync(title: "Game Server", message: "Connected.");
            await _hubConnection.SendAsync("PlayWithMe", Username);
        }
        catch (HttpRequestException e)
        {
            await _dialogService.ShowMessageAsync(e.Message);
        }
    }

    public Task InvalidUsername() => throw new NotImplementedException();

    public Task ReceiveCurrentPlayerList(IEnumerable<string> playerList)
    {
        return _dispatcher.DispatchAsync(()
            => PlayerList = playerList);
    }

    public Task ReceiveChallengeFrom(string playerName) => throw new NotImplementedException();

    public Task ReceiveGameResult() => throw new NotImplementedException();

    public Task AnnounceWinner() => throw new NotImplementedException();

    private Task HubConnectionClosed(Exception? arg)
    {
        return _dispatcher.DispatchAsync(()
            => _dialogService.ShowMessageAsync(title: "Game Server", message: "Connection closed."));
    }

    private ValueTask CloseConnectionAsync() =>
        _hubConnection?.DisposeAsync() ?? ValueTask.CompletedTask;
}
