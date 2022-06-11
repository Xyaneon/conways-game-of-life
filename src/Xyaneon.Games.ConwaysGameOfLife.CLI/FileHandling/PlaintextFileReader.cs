using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Xyaneon.Games.ConwaysGameOfLife.CLI.FileHandling;

public static class PlaintextFileReader
{
    public static PlaintextFileContents ReadFile([DisallowNull] FileInfo fileInfo)
    {
        List<string> fileLines = File.ReadAllLines(fileInfo.FullName).ToList();
        return PlaintextFileParser.ParseLines(fileLines);
    }
}