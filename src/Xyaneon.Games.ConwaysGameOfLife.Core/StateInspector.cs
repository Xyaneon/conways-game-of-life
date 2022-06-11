namespace Xyaneon.Games.ConwaysGameOfLife.Core;

using System.ComponentModel;

/// <summary>
/// Provides methods for inspecting the state of a Conway's Game of Life
/// simulation.
/// </summary>
public static class StateInspector
{
    private const int ErrorCoordinate = -1;

    /// <summary>
    /// Counts how many living neighbors the given cell has.
    /// </summary>
    /// <param name="state">The Game of Life state.</param>
    /// <param name="row">The cell's zero-based row.</param>
    /// <param name="column">The cell's zero-based column.</param>
    /// <returns>The number of living neighbors the cell has.</returns>
    public static int CountLivingNeighbors(bool[,] state, int row, int column)
    {
        int count = 0;

        foreach (Direction direction in Enum.GetValues(typeof(Direction)))
        {
            if (HasLivingNeighborInDirection(state, row, column, direction))
            {
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// Indicates whether a cell has a living neighbor in a given direction.
    /// </summary>
    /// <param name="state">The Game of Life state.</param>
    /// <param name="row">The cell's zero-based row.</param>
    /// <param name="column">The cell's zero-based column.</param>
    /// <param name="direction">The direction of the cell's neighbor.</param>
    /// <returns>
    /// <see langword="true"> if the neighbor is alive; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool HasLivingNeighborInDirection(bool[,] state, int row, int column, Direction direction)
    {
        if (TryGetNeighborCoordinates(state, row, column, direction, out int neighborRow, out int neighborColumn))
        {
            return state[neighborRow, neighborColumn];
        }
        return false;
    }

    /// <summary>
    /// Tries to get the coordinates of a cell's neighbor in the given
    /// direction.
    /// </summary>
    /// <param name="state">The Game of Life state.</param>
    /// <param name="row">The cell's zero-based row.</param>
    /// <param name="column">The cell's zero-based column.</param>
    /// <param name="direction">The direction of the cell's neighbor.</param>
    /// <param name="neighborRow">
    /// An output parameter for the neighbor's zero-based row.
    /// If there is no neighbor in this direction, then this will be set to
    /// -1.
    /// </param>
    /// <param name="neighborColumn">
    /// An output parameter for the neighbor's zero-based column.
    /// If there is no neighbor in this direction, then this will be set to
    /// -1.
    /// </param>
    /// <returns>
    /// <see langword="true"> if there is a neighbor in the given direction;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="InvalidEnumArgumentException">
    /// <paramref name="direction"/> is not a valid <see cref="Direction"/> value.
    /// </exception>
    public static bool TryGetNeighborCoordinates(bool[,] state, int row, int column, Direction direction, out int neighborRow, out int neighborColumn)
    {
        bool belowTopEdge = row > 0;
        bool aboveBottomEdge = row < state.GetLength(0) - 1;
        bool afterLeftEdge = column > 0;
        bool beforeRightEdge = column < state.GetLength(1) - 1;

        switch (direction)
        {
            case Direction.Up:

                neighborRow = belowTopEdge ? row - 1 : ErrorCoordinate;
                neighborColumn = belowTopEdge ? column : ErrorCoordinate;

                return belowTopEdge;
            
            case Direction.Down:

                neighborRow = aboveBottomEdge ? row + 1 : ErrorCoordinate;
                neighborColumn = aboveBottomEdge ? column : ErrorCoordinate;

                return aboveBottomEdge;
            
            case Direction.Left:

                neighborRow = afterLeftEdge ? row : ErrorCoordinate;
                neighborColumn = afterLeftEdge ? column - 1 : ErrorCoordinate;

                return afterLeftEdge;
            
            case Direction.Right:

                neighborRow = beforeRightEdge ? row : ErrorCoordinate;
                neighborColumn = beforeRightEdge ? column + 1 : ErrorCoordinate;

                return beforeRightEdge;
            
            case Direction.UpLeft:

                neighborRow = belowTopEdge ? row - 1 : ErrorCoordinate;
                neighborColumn = afterLeftEdge ? column -1 : ErrorCoordinate;

                return belowTopEdge && afterLeftEdge;
            
            case Direction.UpRight:

                neighborRow = belowTopEdge ? row - 1 : ErrorCoordinate;
                neighborColumn = beforeRightEdge ? column + 1 : ErrorCoordinate;

                return belowTopEdge && beforeRightEdge;
            
            case Direction.DownLeft:

                neighborRow = aboveBottomEdge ? row + 1 : ErrorCoordinate;
                neighborColumn = afterLeftEdge ? column - 1 : ErrorCoordinate;

                return aboveBottomEdge && afterLeftEdge;

            case Direction.DownRight:

                neighborRow = aboveBottomEdge ? row + 1 : ErrorCoordinate;
                neighborColumn = beforeRightEdge ? column + 1 : ErrorCoordinate;

                return aboveBottomEdge && beforeRightEdge;

            default:
                throw new InvalidEnumArgumentException(nameof(direction), (int)direction, typeof(Direction));
        }
    }
}