namespace Xyaneon.Games.ConwaysGameOfLife.Core.Test;

using System.Text;
using Xyaneon.Games.ConwaysGameOfLife.Core.Test.Extensions;

[TestClass]
public class StateUpdaterTest
{
    [TestMethod]
    public void GetNextState_ShouldPreserveBlockStillLife()
    {
        var state = new bool[4, 4] {
            { false, false, false, false },
            { false, true, true, false },
            { false, true, true, false },
            { false, false, false, false },
        };

        bool[,] actualNewState = StateUpdater.GetNextState(state);

        Assert.IsTrue(state.SequenceEquals(actualNewState), CreateNonequalStatesMessage(state, actualNewState));
    }

    [TestMethod]
    public void GetNextState_ShouldPreserveBeehiveStillLife()
    {
        var state = new bool[5, 6] {
            { false, false, false, false, false, false },
            { false, false, true, true, false, false },
            { false, true, false, false, true, false },
            { false, false, true, true, false, false },
            { false, false, false, false, false, false },
        };

        bool[,] actualNewState = StateUpdater.GetNextState(state);

        Assert.IsTrue(state.SequenceEquals(actualNewState), CreateNonequalStatesMessage(state, actualNewState));
    }
    
    [TestMethod]
    public void GetNextState_ShouldPreserveLoafStillLife()
    {
        var state = new bool[6, 6] {
            { false, false, false, false, false, false },
            { false, false, true, true, false, false },
            { false, true, false, false, true, false },
            { false, false, true, false, true, false },
            { false, false, false, true, false, false },
            { false, false, false, false, false, false },
        };

        bool[,] actualNewState = StateUpdater.GetNextState(state);

        Assert.IsTrue(state.SequenceEquals(actualNewState), CreateNonequalStatesMessage(state, actualNewState));
    }

    [TestMethod]
    public void GetNextState_ShouldPreserveBoatStillLife()
    {
        var state = new bool[5, 5] {
            { false, false, false, false, false },
            { false, true, true, false, false },
            { false, true, false, true, false },
            { false, false, true, false, false },
            { false, false, false, false, false },
        };

        bool[,] actualNewState = StateUpdater.GetNextState(state);

        Assert.IsTrue(state.SequenceEquals(actualNewState), CreateNonequalStatesMessage(state, actualNewState));
    }
    
    [TestMethod]
    public void GetNextState_ShouldPreserveTubStillLife()
    {
        var state = new bool[5, 5] {
            { false, false, false, false, false },
            { false, false, true, false, false },
            { false, true, false, true, false },
            { false, false, true, false, false },
            { false, false, false, false, false },
        };

        bool[,] actualNewState = StateUpdater.GetNextState(state);

        Assert.IsTrue(state.SequenceEquals(actualNewState), CreateNonequalStatesMessage(state, actualNewState));
    }
    
    [TestMethod]
    public void GetNextState_ShouldOscillateBlinker()
    {
        var startingState = new bool[5, 5] {
            { false, false, false, false, false },
            { false, false, false, false, false },
            { false, true, true, true, false },
            { false, false, false, false, false },
            { false, false, false, false, false },
        };
        
        var expectedNewStateAtTick1 = new bool[5, 5] {
            { false, false, false, false, false },
            { false, false, true, false, false },
            { false, false, true, false, false },
            { false, false, true, false, false },
            { false, false, false, false, false },
        };

        var expectedNewStateAtTick2 = startingState;

        bool[,] actualNewStateAtTick1 = StateUpdater.GetNextState(startingState);
        bool[,] actualNewStateAtTick2 = StateUpdater.GetNextState(actualNewStateAtTick1);

        Assert.IsTrue(expectedNewStateAtTick1.SequenceEquals(actualNewStateAtTick1), CreateNonequalStatesMessage(expectedNewStateAtTick1, actualNewStateAtTick1));
        Assert.IsTrue(expectedNewStateAtTick2.SequenceEquals(actualNewStateAtTick2), CreateNonequalStatesMessage(expectedNewStateAtTick2, actualNewStateAtTick2));
    }

    private static string CreateNonequalStatesMessage(bool[,] expectedState, bool[,] actualState)
    {
        return (new StringBuilder())
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
                stringBuilder.Append(state[row, column] ? '0' : '.');
            }

            stringBuilder.Append(System.Environment.NewLine);
        }

        return stringBuilder.ToString();
    }
}