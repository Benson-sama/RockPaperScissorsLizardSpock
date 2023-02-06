namespace RockPaperScissorsLizardSpock.Client.Services;

public class UrlService : IUrlService
{
    public string BaseUri => "https://localhost:7185";

    public string GameHubAddress => $"{BaseUri}/gamehub";
}
