using RockPaperScissorsLizardSpock.Model.Moves;

namespace RockPaperScissorsLizardSpock.Model;

public static class RuleBook
{
    public static Move? DecideWinner(Move firstMove, Move secondMove)
    {
        return (firstMove, secondMove) switch
        {
            (RockMove, RockMove) => null,
            (RockMove, PaperMove) => secondMove,
            (RockMove, ScissorsMove) => firstMove,
            (RockMove, LizardMove) => firstMove,
            (RockMove, SpockMove) => secondMove,

            (PaperMove, RockMove) => firstMove,
            (PaperMove, PaperMove) => null,
            (PaperMove, ScissorsMove) => secondMove,
            (PaperMove, LizardMove) => secondMove,
            (PaperMove, SpockMove) => firstMove,

            (ScissorsMove, RockMove) => secondMove,
            (ScissorsMove, PaperMove) => firstMove,
            (ScissorsMove, ScissorsMove) => null,
            (ScissorsMove, LizardMove) => firstMove,
            (ScissorsMove, SpockMove) => secondMove,

            (LizardMove, RockMove) => secondMove,
            (LizardMove, PaperMove) => firstMove,
            (LizardMove, ScissorsMove) => secondMove,
            (LizardMove, LizardMove) => null,
            (LizardMove, SpockMove) => firstMove,

            (SpockMove, RockMove) => firstMove,
            (SpockMove, PaperMove) => secondMove,
            (SpockMove, ScissorsMove) => firstMove,
            (SpockMove, LizardMove) => secondMove,
            (SpockMove, SpockMove) => null,

            _ => throw new ArgumentOutOfRangeException(
                $"{nameof(firstMove)}, {nameof(secondMove)}",
                "Both moves must be of types Rock, Paper, Scissors, Lizard or Spock.")
        };
    }
}
