namespace RockPaperScissorsLizardSpock.Model.Moves;

public static class MoveParser
{
    public static Move Parse(string text)
    {
        return text.ToLower() switch
        {
            "rock" => new RockMove(),
            "paper" => new PaperMove(),
            "scissors" => new ScissorsMove(),
            "lizard" => new LizardMove(),
            "spock" => new SpockMove(),
            _ => throw new ArgumentOutOfRangeException(nameof(text), "The move text cannot be parsed into an instance of Move.")
        };
    }
}
