using RockPaperScissorsLizardSpock.Model.Moves;

namespace RockPaperScissorsLizardSpock.Model;

public class Game
{
    private Move? _firstPlayerMove;
    private bool _isFirstPlayerMoveFinal;
    private Move? _secondPlayerMove;
    private bool _isSecondPlayerMoveFinal;

    public Game(string firstPlayerName, string secondPlayerName)
    {
        if (string.IsNullOrWhiteSpace(firstPlayerName))
            throw new ArgumentNullException(firstPlayerName);

        if (string.IsNullOrWhiteSpace(secondPlayerName))
            throw new ArgumentNullException(secondPlayerName);

        FirstPlayerName = firstPlayerName;
        SecondPlayerName = secondPlayerName;
    }

    public string FirstPlayerName { get; }

    public Move? FirstPlayerMove
    {
        get => _firstPlayerMove;
        set
        {
            if (_isFirstPlayerMoveFinal)
                throw new InvalidOperationException();

            _firstPlayerMove = value;
            _isFirstPlayerMoveFinal = true;
        }
    }

    public string SecondPlayerName { get; }

    public Move? SecondPlayerMove
    {
        get => _secondPlayerMove;
        set
        {
            if (_isSecondPlayerMoveFinal)
                throw new InvalidOperationException();

            _secondPlayerMove = value;
            _isSecondPlayerMoveFinal = true;
        }
    }

    public bool BothMovesSelected
    {
        get => FirstPlayerMove is not null && SecondPlayerMove is not null;
    }

    public string DecideWinner()
    {
        if (FirstPlayerMove is null || SecondPlayerMove is null)
            return "ongoing";

        Move? winner = RuleBook.DecideWinner(FirstPlayerMove, SecondPlayerMove);

        if (winner is null)
            return "tie";
        else if (winner == FirstPlayerMove)
            return FirstPlayerName;
        else if (winner == SecondPlayerMove)
            return SecondPlayerName;
        else
            throw new ArgumentOutOfRangeException();
    }
}
