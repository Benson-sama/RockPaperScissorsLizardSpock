using Microsoft.AspNetCore.SignalR;

namespace RockPaperScissorsLizardSpock.Model.SignalR;

// TODO: Implement GameHub.
public class GameHub : Hub<IGameClient>
{
    public override Task OnConnectedAsync() => base.OnConnectedAsync();

    public override Task OnDisconnectedAsync(Exception exception)
        => base.OnDisconnectedAsync(exception);
}
