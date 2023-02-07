namespace RockPaperScissorsLizardSpock.Model.Messages;

public record WinnerAnnouncedMessage()
{
    public string FirstPlayername { get; init; } = default!;

    public string SecondPlayername { get; init;} = default!;

    public Guid GameId { get; init; }

    public string FirstMove { get; init; } = default!;

    public string SecondMove { get; init; } = default!;

    public string Winner { get; init; } = default!;
}
