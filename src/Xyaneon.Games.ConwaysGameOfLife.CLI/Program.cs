using System.CommandLine;

using Xyaneon.Games.ConwaysGameOfLife.Core;
using Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

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

            bool[,] currentState = contents.State;
            int tick = 0;
            bool quit = false;

            while (!quit)
            {
                Console.WriteLine();
                ConsolePrinter.PrintTick(tick);
                ConsolePrinter.PrintState(currentState);
                Console.WriteLine();
                ConsolePrinter.PrintInteractiveOptions();


                bool keyRecognized = false;
                while (!keyRecognized)
                {
                    ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                    switch (consoleKeyInfo.KeyChar)
                    {
                        case 'q':

                            quit = true;
                            keyRecognized = true;
                            break;

                        case 'n':
                            currentState = StateUpdater.GetNextState(currentState);
                            tick++;
                            keyRecognized = true;
                            break;

                        default:

                            Console.Beep();
                            keyRecognized = false;
                            break;
                    }
                }
            }
        }
    }
}
