namespace RockPaperScissorsLizardSpock.Model.SignalR;

// TODO: Change method signatures to actual needs.
public interface IGameClient
{
    Task InvalidUsername();

    Task ReceiveCurrentPlayerList(IEnumerable<string> playerList);

    Task ReceiveChallengeFrom(string playerName);

    Task ReceiveGameResult();

    Task AnnounceWinner();
}
