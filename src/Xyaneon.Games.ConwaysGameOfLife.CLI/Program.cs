using System.CommandLine;
using System.IO;

using Xyaneon.Games.ConwaysGameOfLife.CLI.FileHandling;

namespace Xyaneon.Games.ConwaysGameOfLife.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            var fileOption = new Option<FileInfo>(
                "--file",
                "A file containing a starting state. Supports plaintext format (.cells extension)"
            );

            var rootCommand = new RootCommand
            {
                fileOption,
            };

            rootCommand.Description = "A Conway's Game of Life simulation runner";
            rootCommand.SetHandler((FileInfo fileInfo) =>
            {
                LoadPlaintextFile(fileInfo);
            }, fileOption);

            return rootCommand.Invoke(args);
        }

        static void LoadPlaintextFile(FileInfo fileInfo)
        {
            if (fileInfo is null)
            {
                Console.WriteLine("Error: FileInfo is null and cannot be loaded from.");
                return;
            }

            PlaintextFileContents contents = PlaintextFileReader.ReadFile(fileInfo);

            ConsolePrinter.PrintFileInformation(contents);
            Console.WriteLine();

            bool[,] currentState = contents.State;
            int tick = 0;

            ConsolePrinter.PrintTick(tick);
            ConsolePrinter.PrintState(contents.State);
        }
    }
}
