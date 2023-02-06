namespace RockPaperScissorsLizardSpock.Model.Moves;

public static class MoveFactory
{
    public static Move GetRandomMove()
    {
        return Random.Shared.Next(5) switch
        {
            0 => new RockMove(),
            1 => new PaperMove(),
            2 => new ScissorsMove(),
            3 => new LizardMove(),
            4 => new SpockMove(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
