namespace Xyaneon.Games.ConwaysGameOfLife.Core.Test;

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

        Assert.IsTrue(state.SequenceEquals(actualNewState));
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

        Assert.IsTrue(state.SequenceEquals(actualNewState));
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

        Assert.IsTrue(state.SequenceEquals(actualNewState));
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

        Assert.IsTrue(state.SequenceEquals(actualNewState));
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

        Assert.IsTrue(state.SequenceEquals(actualNewState));
    }
}