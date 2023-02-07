using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace RockPaperScissorsLizardSpock.Model.SignalR;

// TODO: Implement GameHub.
public class GameHub : Hub<IGameClient>
{
    private static readonly ConcurrentDictionary<string, string> _connections = new();

    public override Task OnConnectedAsync() => base.OnConnectedAsync();

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var entry = _connections.FirstOrDefault(x => x.Value == Context.ConnectionId);
        _connections.TryRemove(entry);
        return base.OnDisconnectedAsync(exception);
    }

    public async Task PlayWithMe(string playerName)
    {
        if (_connections.ContainsKey(playerName))
        {
            await Clients.Caller.InvalidUsername();
        }
        else
        {
            _connections[playerName] = Context.ConnectionId;
            await Clients.All.ReceiveCurrentPlayerList(_connections.Keys);
        }
    }
}
