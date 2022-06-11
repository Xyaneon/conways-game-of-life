namespace Xyaneon.Games.ConwaysGameOfLife.Core.Test;

[TestClass]
public class StateInspectorTest
{
    [TestMethod]
    public void CountLivingNeighbors_ShouldReturnCorrectCount()
    {
        const int row = 1;
        const int column = 1;
        var state = new bool[3, 3] {
            { false, false, false },
            { true, false, false },
            { false, true, true },
        };

        int actual = StateInspector.CountLivingNeighbors(state, row, column);

        Assert.AreEqual(3, actual);
    }

    [TestMethod]
    public void HasLivingNeighborInDirection_ShouldReturnTrueForLivingNeighbor()
    {
        const int row = 1;
        const int column = 1;
        var state = new bool[3, 3] {
            { false, false, false },
            { true, false, false },
            { false, false, false },
        };

        bool result = StateInspector.HasLivingNeighborInDirection(state, row, column, Direction.Left);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void HasLivingNeighborInDirection_ShouldReturnFalseForDeadNeighbor()
    {
        const int row = 1;
        const int column = 1;
        var state = new bool[3, 3] {
            { false, false, false },
            { true, false, false },
            { false, false, false },
        };

        bool result = StateInspector.HasLivingNeighborInDirection(state, row, column, Direction.Up);

        Assert.IsFalse(result);
    }

    [TestMethod]
    [DataRow(1, 1, Direction.Up, 0, 1)]
    [DataRow(1, 1, Direction.Left, 1, 0)]
    [DataRow(1, 1, Direction.Right, 1, 2)]
    [DataRow(1, 1, Direction.Down, 2, 1)]
    [DataRow(1, 1, Direction.UpLeft, 0, 0)]
    [DataRow(1, 1, Direction.UpRight, 0, 2)]
    [DataRow(1, 1, Direction.DownLeft, 2, 0)]
    [DataRow(1, 1, Direction.DownRight, 2, 2)]
    public void TryGetNeighborCoordinates_ShouldReturnTrueAndGetCoordinatesAwayFromEdge(int startRow, int startColumn, Direction direction, int expectedRow, int expectedColumn)
    {
        var state = new bool[3, 3] {
            { false, false, false },
            { false, false, false },
            { false, false, false },
        };

        bool result = StateInspector.TryGetNeighborCoordinates(state, startRow, startColumn, direction, out int actualRow, out int actualColumn);

        Assert.IsTrue(result);
        Assert.AreEqual(expectedRow, actualRow);
        Assert.AreEqual(expectedColumn, actualColumn);
    }
    
    [TestMethod]
    [DataRow(0, 1, Direction.Up)]
    [DataRow(1, 0, Direction.Left)]
    [DataRow(1, 2, Direction.Right)]
    [DataRow(2, 1, Direction.Down)]
    [DataRow(0, 0, Direction.UpLeft)]
    [DataRow(0, 2, Direction.UpRight)]
    [DataRow(2, 0, Direction.DownLeft)]
    [DataRow(2, 2, Direction.DownRight)]
    public void TryGetNeighborCoordinates_ShouldReturnFalseAndGetErrorCoordinatesAtEdge(int startRow, int startColumn, Direction direction)
    {
        var state = new bool[3, 3] {
            { false, false, false },
            { false, false, false },
            { false, false, false },
        };

        bool result = StateInspector.TryGetNeighborCoordinates(state, startRow, startColumn, direction, out int actualRow, out int actualColumn);

        Assert.IsFalse(result);
        Assert.AreEqual(-1, actualRow);
        Assert.AreEqual(-1, actualColumn);
    }
}