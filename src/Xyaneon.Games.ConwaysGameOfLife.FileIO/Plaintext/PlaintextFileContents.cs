using System.Diagnostics.CodeAnalysis;

namespace Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

/// <summary>
/// Represents the contents of a Plaintext (.cells) file.
/// </summary>
/// <remarks>
/// More information about the Plaintext file format can be found at
/// https://conwaylife.com/wiki/Plaintext .
/// </remarks>
public record PlaintextFileContents
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlaintextFileContents"/> class.
    /// </summary>
    /// <param name="name">The name listed on the first line of the file.</param>
    /// <param name="description">The optional description line(s) listed after the <paramref name="name"/>.</param>
    /// <param name="state">The actual Game of Life state saved in the file.</param>
    public PlaintextFileContents([DisallowNull] string name, [AllowNull] string description, [DisallowNull] bool[,] state)
    {
        Name = name;
        Description = description;
        State = state;
    }

    /// <summary>
    /// Gets the name listed on the first line of the file.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Gets the optional description line(s) listed after the <see cref="Name"/>.
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Gets the actual Game of Life state saved in the file.
    /// </summary>
    /// <remarks>
    /// Living cells ('O') will be stored as <see langword="true"/>, and
    /// dead cells ('.') will be stored as <see langword="false"/>.
    /// Cells are stored in the array by row, then column.
    /// </remarks>
    public bool[,] State { get; init; }
}
