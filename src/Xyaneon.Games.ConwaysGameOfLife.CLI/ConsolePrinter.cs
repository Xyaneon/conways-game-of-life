using Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

namespace Xyaneon.Games.ConwaysGameOfLife.CLI
{
    public static class ConsolePrinter
    {
        private const char AliveCellChar = 'O';
        private const char DeadCellChar = '.';

        public static void PrintFileInformation(PlaintextFileContents contents)
        {
            Console.WriteLine($"Name: {contents.Name}");
            Console.WriteLine();
            Console.WriteLine("Description:");
            Console.WriteLine();
            Console.WriteLine(contents.Description);
        }

        public static void PrintInteractiveOptions()
        {
            Console.WriteLine("--- Press a key: (n): next, (q): quit ---");
        }

        public static void PrintState(bool[,] state)
        {
            for (int row = 0; row < state.GetLength(0); row++)
            {
                for (int column = 0; column < state.GetLength(1); column++)
                {
                    Console.Write(state[row, column] ? AliveCellChar : DeadCellChar);
                }
                
                Console.WriteLine();
            }
        }

        public static void PrintTick(int tick)
        {
            Console.WriteLine($"Tick #{tick}");
        }
    }
}