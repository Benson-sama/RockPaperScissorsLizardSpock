namespace RockPaperScissorsLizardSpock.Client.Services;

public interface IUrlService
{
    string BaseUri { get; }

    string GameHubAddress { get; }
}
