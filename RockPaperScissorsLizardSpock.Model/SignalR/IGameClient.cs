using RockPaperScissorsLizardSpock.Model.Messages;

namespace RockPaperScissorsLizardSpock.Model.SignalR;

public interface IGameClient
{
    Task InvalidUsername();

    Task ReceiveCurrentPlayerList(IEnumerable<string> playerList);

    Task ReceiveChallenge(ChallengeMessage message);

    Task AnnounceWinner(WinnerAnnouncedMessage message);
}
