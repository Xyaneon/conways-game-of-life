using System.Diagnostics.CodeAnalysis;

namespace Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

/// <summary>
/// Provides methods for reading Plaintext files and returning their contents.
/// </summary>
public static class PlaintextFileReader
{
    /// <summary>
    /// Synchronously reads a Plaintext file and returns its parsed contents.
    /// </summary>
    /// <param name="fileInfo">A <see cref="FileInfo"/> for the file to read.</param>
    /// <returns>A new <see cref="PlaintextFileContents"/> containing the parsed file data.</returns>
    public static PlaintextFileContents ReadFile([DisallowNull] FileInfo fileInfo)
    {
        List<string> fileLines = File.ReadAllLines(fileInfo.FullName).ToList();
        return PlaintextFileParser.ParseLines(fileLines);
    }
}