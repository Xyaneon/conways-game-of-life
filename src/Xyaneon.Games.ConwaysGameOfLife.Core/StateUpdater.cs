namespace Xyaneon.Games.ConwaysGameOfLife.Core;

/// <summary>
/// Updates the simulation state according to the rules of Conway's Game of
/// Life.
/// </summary>
public class StateUpdater
{
    /// <summary>
    /// Given a Game of Life state, returns the next simulation state.
    /// </summary>
    /// <param name="startingState">
    /// A 2D boolean array representing the initial starting state.
    /// </param>
    /// <returns>
    /// A new 2D boolean array representing the next simulation state.
    /// </returns>
    /// <remarks>
    /// This method will produce the updated state following the rules of
    /// Conway's Game of Life. In this implementation, alive cells are
    /// represented as true, and dead cells are represented as false.
    /// </remarks>
    public static bool[,] GetNextState(bool[,] startingState)
    {
        int rowCount = startingState.GetLength(0);
        int columnCount = startingState.GetLength(1);
        bool[,] newState = new bool[rowCount, columnCount];

        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                int livingNeighborsCount = StateInspector.CountLivingNeighbors(startingState, row, column);

                if (startingState[row, column])
                {
                    newState[row, column] = livingNeighborsCount == 2 || livingNeighborsCount == 3;
                }
                else
                {
                    newState[row, column] = livingNeighborsCount == 3;
                }
            }
        }

        return newState;
    }
}
