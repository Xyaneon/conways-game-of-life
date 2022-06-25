namespace Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

/// <summary>
/// Provides constants relevant to Plaintext files.
/// </summary>
public static class PlaintextFileConstants
{
    /// <summary>
    /// The character used to represent a living cell.
    /// </summary>
    public const char AliveCellChar = 'O';

    /// <summary>
    /// The character used to represent a dead cell.
    /// </summary>
    public const char DeadCellChar = '.';

    /// <summary>
    /// The character which comes at the start of each description line.
    /// </summary>
    public const char DescriptionLineStartingChar = '!';

    /// <summary>
    /// The string at the start of the name line.
    /// </summary>
    public const string NameLinePrefix = "!Name:";
}
