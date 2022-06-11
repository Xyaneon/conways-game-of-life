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

        Assert.That.StatesAreEqual(state, actualNewState);
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

        Assert.That.StatesAreEqual(state, actualNewState);
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

        Assert.That.StatesAreEqual(state, actualNewState);
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

        Assert.That.StatesAreEqual(state, actualNewState);
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

        Assert.That.StatesAreEqual(state, actualNewState);
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

        Assert.That.StatesAreEqual(expectedNewStateAtTick1, actualNewStateAtTick1);
        Assert.That.StatesAreEqual(expectedNewStateAtTick2, actualNewStateAtTick2);
    }
}
