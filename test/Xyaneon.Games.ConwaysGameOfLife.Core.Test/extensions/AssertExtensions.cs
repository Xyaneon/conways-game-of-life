using System.Text;

namespace Xyaneon.Games.ConwaysGameOfLife.Core.Test.Extensions;

public static class AssertExtensions
{
    private const char AliveCellChar = 'O';
    private const char DeadCellChar = '.';

    public static void StatesAreEqual(this Assert assert, bool[,] expectedState, bool[,] actualState)
    {
        if (expectedState.SequenceEquals(actualState))
        {
            return;
        }

        throw new AssertFailedException(CreateNonequalStatesMessage(expectedState, actualState));
    }

    private static string CreateNonequalStatesMessage(bool[,] expectedState, bool[,] actualState)
    {
        return (new StringBuilder())
            .AppendLine("Game of Life states are not equal.")
            .AppendLine("Expected state:")
            .AppendLine(ConvertStateToString(expectedState))
            .AppendLine("Actual state:")
            .AppendLine(ConvertStateToString(actualState))
            .ToString();
    }

    private static string ConvertStateToString(bool[,] state)
    {
        var stringBuilder = new StringBuilder();

        for (int row = 0; row < state.GetLength(0); row++)
        {
            for (int column = 0; column < state.GetLength(1); column++)
            {
                stringBuilder.Append(state[row, column] ? AliveCellChar : DeadCellChar);
            }

            stringBuilder.Append(System.Environment.NewLine);
        }

        return stringBuilder.ToString();
    }
}
