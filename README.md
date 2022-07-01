# Conway's Game of Life

![dotnet workflow](https://github.com/Xyaneon/conways-game-of-life/actions/workflows/dotnet.yml/badge.svg)
[![License](https://img.shields.io/github/license/Xyaneon/conways-game-of-life)][License]

This project implements [Conway's Game of Life](https://en.wikipedia.org/wiki/Conway's_Game_of_Life) as a .NET 6 solution with C# 10. This solution includes both a console and a GUI app.

## How to run the GUI app

![Screenshot of GUI app on Windows 10][GUI app screenshot]

The graphical version of the application is implemented using [Avalonia UI].
It has been tested to work at least on Ubuntu 22.04 LTS (Jammy Jellyfish) and Windows 10 21H1.

To run the GUI app, you will first need [.NET 6][Install .NET 6] installed. Once this is set up, navigate into the repository root and run the following command:

```Bash
dotnet run --project src/Xyaneon.Games.ConwaysGameOfLife.Avalonia/Xyaneon.Games.ConwaysGameOfLife.Avalonia.csproj
```

The app can currently load Game of Life files in the [Plaintext file format](https://conwaylife.com/wiki/Plaintext) (`.cells` extension). You can either supply your own, or try one of the included examples in the `samples/` directory.

## How to run the console app

![Screenshot of CLI app in Ubuntu Terminal][CLI app screenshot]

### Running the project with `dotnet`

One of the main projects in this solution is a console app.

To run the console app, you will first need [.NET 6][Install .NET 6] installed. Once this is set up, navigate into the repository root and run the following command:

```Bash
dotnet run --project src/Xyaneon.Games.ConwaysGameOfLife.CLI/Xyaneon.Games.ConwaysGameOfLife.CLI.csproj --file <file_name_here>
```

The app can currently load Game of Life files in the [Plaintext file format](https://conwaylife.com/wiki/Plaintext) (`.cells` extension). You can either supply your own, or try one of the included examples in the `samples/` directory. For example, here's the same command with the `samples/blinker.cells` path provided:

```Bash
dotnet run --project src/Xyaneon.Games.ConwaysGameOfLife.CLI/Xyaneon.Games.ConwaysGameOfLife.CLI.csproj --file samples/blinker.cells
```

The console app is interactive. It will print the updated state into the terminal each time you press any key, until you press <kbd>Q</kbd> to quit.

### Creating a standalone executable

The above instructions will give you a framework-dependent executable you can quickly run if `dotnet` is installed locally. It is also possible to create a standalone command line executable that should not need .NET to be installed to run.

To do this, run the following command (replace `linux-x64` with your target platform if needed):

```Bash
dotnet publish -c Release -r linux-x64 --self-contained true
```

This will create an executable file named **`Xyaneon.Games.ConwaysGameOfLife.CLI`** in the `src/Xyaneon.Games.ConwaysGameOfLife.CLI/bin/Release/net6.0/linux-x64/publish` directory.

You can then place this executable wherever you like, and run it from within the same directory with a command like the following:

```Bash
./Xyaneon.Games.ConwaysGameOfLife.CLI --file <your pattern file path>
```

## Background

This project is my entry into a sort of coding competition among friends.
We were each tasked with creating our own implementation of Conway's Game of Life, with any technology or features of our choosing.
Our deadline was June 30, 2022.

Other entries I could link to are listed below:
- https://github.com/jquesada2/game-of-life
- https://github.com/kjarosz/HaskellGameOfLife

## License

This project is free and open-source software (FOSS) released under an MIT license. See [LICENSE.txt][License] for details.


[Avalonia UI]: http://avaloniaui.net/
[CLI app screenshot]: https://github.com/Xyaneon/conways-game-of-life/blob/main/screenshots/cli-screenshot.png
[GUI app screenshot]: https://github.com/Xyaneon/conways-game-of-life/blob/main/screenshots/gui-screenshot.png
[Install .NET 6]: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
[License]: https://github.com/Xyaneon/conways-game-of-life/blob/main/LICENSE.txt
