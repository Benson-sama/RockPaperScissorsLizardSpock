using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using RockPaperScissorsLizardSpock.Model.Messages;
using RockPaperScissorsLizardSpock.Model.Moves;

namespace RockPaperScissorsLizardSpock.Model.SignalR;

// TODO: Implement GameHub.
public class GameHub : Hub<IGameClient>
{
    private static readonly ConcurrentDictionary<string, string> _connections = new();

    private static readonly ConcurrentDictionary<Guid, Game> _games = new();

    public override Task OnConnectedAsync() => base.OnConnectedAsync();

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var entry = _connections.FirstOrDefault(x => x.Value == Context.ConnectionId);
        _connections.TryRemove(entry);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task PlayWithMe(string playername)
    {
        if (_connections.ContainsKey(playername))
        {
            await Clients.Caller.InvalidUsername();
        }
        else
        {
            _connections[playername] = Context.ConnectionId;
            await Clients.All.ReceiveCurrentPlayerList(_connections.Keys);
        }
    }

    public async Task SendChallenge(string targetPlayername, string moveText)
    {
        if (!_connections.ContainsKey(targetPlayername))
            return;

        if (!_connections.Values.Contains(Context.ConnectionId))
            return;

        Move move = MoveParser.Parse(moveText);
        string targetConnectionId = _connections[targetPlayername];
        string senderPlayerName = _connections.First(x => x.Value == Context.ConnectionId).Key;
        Game game = new(senderPlayerName, targetPlayername) { FirstPlayerMove = move };
        Guid gameId = Guid.NewGuid();
        _games[gameId] = game;
        ChallengeMessage message = new(senderPlayerName, gameId);

        await Clients.Client(targetConnectionId).ReceiveChallenge(message);
    }

    public async Task SendPlay(string targetPlayername, Guid gameId, string moveText)
    {
        if (!_games.ContainsKey(gameId))
            return;

        Game game = _games[gameId];

        game.SecondPlayerMove = MoveParser.Parse(moveText);

        if (!game.BothMovesSelected)
            return;

        string winnerText = game.DecideWinner();

        WinnerAnnouncedMessage message = new()
        {
            FirstPlayername = game.FirstPlayerName,
            SecondPlayername = game.SecondPlayerName,
            GameId = gameId,
            FirstMove = game.FirstPlayerMove!.ToString(),
            SecondMove = game.SecondPlayerMove!.ToString(),
            Winner = winnerText
        };

        await Clients.All.AnnounceWinner(message);
    }
}
