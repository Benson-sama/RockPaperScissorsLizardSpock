using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using RockPaperScissorsLizardSpock.Client.Services;
using RockPaperScissorsLizardSpock.Model.Messages;
using RockPaperScissorsLizardSpock.Model.SignalR;

namespace RockPaperScissorsLizardSpock.Client.ViewModels;

public partial class GameViewModel : ObservableObject, IGameClient
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

    [ObservableProperty]
    private ObservableCollection<ChallengeMessage> _challengeMessages = new();

    [ObservableProperty]
    private ObservableCollection<WinnerAnnouncedMessage> _winnerAnnouncedMessages = new();

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
        _hubConnection.On(nameof(InvalidUsername), InvalidUsername);
        _hubConnection.On<IEnumerable<string>>(nameof(ReceiveCurrentPlayerList), ReceiveCurrentPlayerList);
        _hubConnection.On<ChallengeMessage>(nameof(ReceiveChallenge), ReceiveChallenge);
        _hubConnection.On<WinnerAnnouncedMessage>(nameof(AnnounceWinner), AnnounceWinner);

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

    public Task InvalidUsername()
    {
        return _dispatcher.DispatchAsync(()
            => _dialogService.ShowMessageAsync(title: "Game Server", message: "Username already taken."));
    }

    public Task ReceiveCurrentPlayerList(IEnumerable<string> playerList)
    {
        return _dispatcher.DispatchAsync(()
            => PlayerList = playerList);
    }

    public Task ReceiveChallenge(ChallengeMessage message)
    {
        return _dispatcher.DispatchAsync(()
            => ChallengeMessages.Add(message));
    }

    public Task AnnounceWinner(WinnerAnnouncedMessage message)
    {
        return _dispatcher.DispatchAsync(()
            => WinnerAnnouncedMessages.Add(message));
    }

    private Task HubConnectionClosed(Exception? arg)
    {
        return _dispatcher.DispatchAsync(()
            => _dialogService.ShowMessageAsync(title: "Game Server", message: "Connection closed."));
    }

    private ValueTask CloseConnectionAsync() =>
        _hubConnection?.DisposeAsync() ?? ValueTask.CompletedTask;
}
