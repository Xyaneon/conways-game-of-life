using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

public static class PlaintextFileReader
{
    public static PlaintextFileContents ReadFile([DisallowNull] FileInfo fileInfo)
    {
        List<string> fileLines = File.ReadAllLines(fileInfo.FullName).ToList();
        return PlaintextFileParser.ParseLines(fileLines);
    }
}