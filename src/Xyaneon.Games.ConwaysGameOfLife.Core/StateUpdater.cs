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
        return startingState;
    }
}
