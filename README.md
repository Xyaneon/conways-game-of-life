# Conway's Game of Life

This project implements [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway's_Game_of_Life).

## Usage

The main project in this solution is a console app.

To run it, you will first need .NET 6 installed. Once this is set up, navigate into the repository root and run the following command:

```Bash
dotnet run --project src/Xyaneon.Games.ConwaysGameOfLife.CLI/Xyaneon.Games.ConwaysGameOfLife.CLI.csproj --file <file_name_here>
```

The app can currently load Game of Life files in the [Plaintext file format](https://conwaylife.com/wiki/Plaintext) (`.cells` extension). You can either supply your own, or try one of the included examples in the `samples/` directory. For example, here's the same command with the `samples/blinker.cells` path provided:

```Bash
dotnet run --project src/Xyaneon.Games.ConwaysGameOfLife.CLI/Xyaneon.Games.ConwaysGameOfLife.CLI.csproj --file samples/blinker.cells
```

The console app is interactive. It will print the updated state into the terminal each time you press any key, until you press <kbd>Q</kbd> to quit.

## Background

This project is my entry into a sort of coding competition among friends.

## License

This project is free and open-source software (FOSS) released under an MIT license. See [LICENSE.txt](https://github.com/Xyaneon/conways-game-of-life/blob/main/LICENSE.txt) for details.