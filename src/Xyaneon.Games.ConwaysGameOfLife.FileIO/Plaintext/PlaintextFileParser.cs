using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

public static class PlaintextFileParser
{
    /// <summary>
    /// Parses the provided collection of lines as a Plaintext file.
    /// </summary>
    /// <param name="lines">The collection of lines to parse as a Plaintext file.</param>
    /// <returns>A new <see cref="PlaintextFileContents"/> containing the parsed data.</returns>
    /// <exception cref="FormatException"><paramref name="lines"/> does not conform to the Plaintext file format.</exception>
    public static PlaintextFileContents ParseLines([DisallowNull] IEnumerable<string> lines)
    {
        bool nameLineWasParsed = false;
        bool descriptionWasParsed = false;
        var descriptionBuilder = new StringBuilder();

        string name = "";
        string description = "";

        int lineLength = -1;
        var stateLines = new Queue<bool[]>();

        int lineNumber = 0;
        foreach (string line in lines)
        {
            if (!nameLineWasParsed)
            {
                name = ParseNameLine(line);
                nameLineWasParsed = true;
            }
            else if (line.StartsWith(PlaintextFileConstants.DescriptionLineStartingChar))
            {
                if (!descriptionWasParsed)
                {
                    string descriptionLine = ParseDescriptionLine(line);
                    descriptionBuilder.AppendLine(descriptionLine);
                }
                else
                {
                    throw new FormatException($"Line {lineNumber + 1} does not conform to the Plaintext format.");
                }
            }
            else
            {
                descriptionWasParsed = true;
                bool[] parsedStateLine = ParseStateLine(line);
                if (lineLength != -1 && parsedStateLine.Length != lineLength)
                {
                    var message = $"Inconsistent state line length at line {lineNumber + 1} (expected {lineLength}, but got {parsedStateLine.Length}).";
                    throw new FormatException(message);
                }
                stateLines.Enqueue(parsedStateLine);
                lineLength = parsedStateLine.Length;
            }

            lineNumber++;
        }

        description = descriptionBuilder.ToString();

        bool[,] state = new bool[stateLines.Count, lineLength];

        int row = 0;
        foreach (bool[] parsedLine in stateLines)
        {
            for (int column = 0; column < lineLength; column++)
            {
                state[row, column] = parsedLine[column];
            }

            row++;
        }

        return new PlaintextFileContents(name, description, state);
    }

    public static string ParseNameLine([DisallowNull] string line)
    {
        if (!line.StartsWith(PlaintextFileConstants.NameLinePrefix))
        {
            throw new FormatException($"The line does not conform to the Plaintext name format (missing '{PlaintextFileConstants.NameLinePrefix}' prefix).");
        }

        return line.Substring(PlaintextFileConstants.NameLinePrefix.Length).Trim();
    }

    public static string ParseDescriptionLine([DisallowNull] string line)
    {
        if (!line.StartsWith("!"))
        {
            throw new FormatException("The line does not conform to the Plaintext description format.");
        }

        return line.TrimStart(PlaintextFileConstants.DescriptionLineStartingChar);
    }

    public static bool[] ParseStateLine([DisallowNull] string line)
    {
        bool[] parsedStateLine = new bool[line.Length];

        for (int position = 0; position < line.Length; position++)
        {
            bool isAlive = line[position] switch
            {
                PlaintextFileConstants.AliveCellChar => true,
                PlaintextFileConstants.DeadCellChar => false,
                _ => throw new FormatException($"Unrecognized character at position {position}.")
            };

            parsedStateLine[position] = isAlive;
        }

        return parsedStateLine;
    }
}
