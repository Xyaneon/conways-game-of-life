namespace Xyaneon.Games.ConwaysGameOfLife.Avalonia.Models;

public class GameOfLifeState
{
    public GameOfLifeState(bool[,] state)
    {
        this._state = state;
    }

    private bool[,] _state;

    public int ColumnCount { get => _state.GetLength(1); }

    public int RowCount { get => _state.GetLength(0); }

    public bool IsCellAliveAt(int row, int column) => _state[row, column];

    public bool[,] To2DArray()
    {
        int rowCount = _state.GetLength(0);
        int columnCount = _state.GetLength(1);
        bool[,] newState = new bool[RowCount, ColumnCount];

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                newState[row, column] = _state[row, column];
            }
        }

        return newState;
    }
}
