namespace Xyaneon.Games.ConwaysGameOfLife.FileIO.Test.Plaintext;

using Xyaneon.Games.ConwaysGameOfLife.FileIO.Test.Extensions;
using Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

[TestClass]
public class PlaintextFileParserTest
{
    [TestMethod]
    public void ParseLines_ShouldParseValidFileLinesWithSingleLineDescription()
    {
        var expectedState = new bool[5, 6] {
            { false, false, false, false, false, false },
            { false, false, true, true, false, false },
            { false, true, false, false, true, false },
            { false, false, true, true, false, false },
            { false, false, false, false, false, false },
        };
        IEnumerable<string> fileLines = new List<string>() {
            "!Name:Valid pattern",
            "!This is the only description line.",
            "......",
            "..OO..",
            ".O..O.",
            "..OO..",
            "......",
        };

        PlaintextFileContents actual = PlaintextFileParser.ParseLines(fileLines);

        Assert.AreEqual("Valid pattern", actual.Name);
        Assert.AreEqual($"This is the only description line.{Environment.NewLine}", actual.Description);
        Assert.That.StatesAreEqual(expectedState, actual.State);
    }

    [TestMethod]
    public void ParseLines_ShouldParseValidFileLinesWithMultilineDescription()
    {
        var expectedState = new bool[5, 6] {
            { false, false, false, false, false, false },
            { false, false, true, true, false, false },
            { false, true, false, false, true, false },
            { false, false, true, true, false, false },
            { false, false, false, false, false, false },
        };
        IEnumerable<string> fileLines = new List<string>() {
            "!Name:Valid pattern",
            "!This is description line 1.",
            "!This is description line 2.",
            "......",
            "..OO..",
            ".O..O.",
            "..OO..",
            "......",
        };

        PlaintextFileContents actual = PlaintextFileParser.ParseLines(fileLines);

        Assert.AreEqual("Valid pattern", actual.Name);
        Assert.AreEqual($"This is description line 1.{Environment.NewLine}This is description line 2.{Environment.NewLine}", actual.Description);
        Assert.That.StatesAreEqual(expectedState, actual.State);
    }

    [TestMethod]
    public void ParseLines_ShouldRejectFileLinesWithInvalidNameLineStartingPrefix()
    {
        var expectedState = new bool[5, 6] {
            { false, false, false, false, false, false },
            { false, false, true, true, false, false },
            { false, true, false, false, true, false },
            { false, false, true, true, false, false },
            { false, false, false, false, false, false },
        };
        IEnumerable<string> fileLines = new List<string>() {
            "Invalid name line",
            "!This is description line 1.",
            "!This is description line 2.",
            "......",
            "..OO..",
            ".O..O.",
            "..OO..",
            "......",
        };

        var actual = Assert.ThrowsException<FormatException>(() => PlaintextFileParser.ParseLines(fileLines));

        Assert.AreEqual("The line does not conform to the Plaintext name format (missing '!Name:' prefix).", actual.Message);
    }

    [TestMethod]
    public void ParseLines_ShouldRejectFileLinesWithInvalidDescriptionStartingCharacter()
    {
        var expectedState = new bool[5, 6] {
            { false, false, false, false, false, false },
            { false, false, true, true, false, false },
            { false, true, false, false, true, false },
            { false, false, true, true, false, false },
            { false, false, false, false, false, false },
        };
        IEnumerable<string> fileLines = new List<string>() {
            "!Name:Valid pattern",
            "$This is description line 1.",
            "!This is description line 2.",
            "......",
            "..OO..",
            ".O..O.",
            "..OO..",
            "......",
        };

        var actual = Assert.ThrowsException<FormatException>(() => PlaintextFileParser.ParseLines(fileLines));

        Assert.AreEqual("Unrecognized character at position 0.", actual.Message);
    }

    [TestMethod]
    public void ParseLines_ShouldRejectFileLinesWithStateLineLengthMismatch()
    {
        var expectedState = new bool[5, 6] {
            { false, false, false, false, false, false },
            { false, false, true, true, false, false },
            { false, true, false, false, true, false },
            { false, false, true, true, false, false },
            { false, false, false, false, false, false },
        };
        IEnumerable<string> fileLines = new List<string>() {
            "!Name:Valid pattern",
            "!This is description line 1.",
            "!This is description line 2.",
            "......",
            "..OO..",
            ".O..O",
            "..OO..",
            "......",
        };

        var actual = Assert.ThrowsException<FormatException>(() => PlaintextFileParser.ParseLines(fileLines));

        Assert.AreEqual("Inconsistent state line length at line 6 (expected 6, but got 5).", actual.Message);
    }

    [TestMethod]
    public void ParseLines_ShouldRejectFileLinesWithInvalidStateCharacter()
    {
        var expectedState = new bool[5, 6] {
            { false, false, false, false, false, false },
            { false, false, true, true, false, false },
            { false, true, false, false, true, false },
            { false, false, true, true, false, false },
            { false, false, false, false, false, false },
        };
        IEnumerable<string> fileLines = new List<string>() {
            "!Name:Valid pattern",
            "!This is description line 1.",
            "!This is description line 2.",
            "..A...",
            "..OO..",
            ".O..O.",
            "..OO..",
            "......",
        };

        var actual = Assert.ThrowsException<FormatException>(() => PlaintextFileParser.ParseLines(fileLines));

        Assert.AreEqual("Unrecognized character at position 2.", actual.Message);
    }
}