using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Xyaneon.Games.ConwaysGameOfLife.Avalonia.Models;
using Xyaneon.Games.ConwaysGameOfLife.Core;
using Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

namespace Xyaneon.Games.ConwaysGameOfLife.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        IObservable<bool> canRunTickCommand = this.WhenAnyValue(x => x.PatternState)
            .Select(patternState => patternState is not null);

        DeleteGridCommand = ReactiveCommand.Create(RunDeleteGridCommand);
        OpenCommand = ReactiveCommand.CreateFromTask(RunOpenCommand);
        TickCommand = ReactiveCommand.Create(RunTickCommand, canRunTickCommand);
        QuitCommand = ReactiveCommand.Create(RunQuitCommand);
        VisitWebsiteCommand = ReactiveCommand.Create(RunVisitWebsiteCommand);

        _patternState = null;
    }

    private string? _patternDescription;
    private string? _patternName;
    private GameOfLifeState? _patternState;
    private int _tickNumber;

    public ReactiveCommand<Unit, Unit> DeleteGridCommand { get; }
    
    public ReactiveCommand<Unit, Unit> OpenCommand { get; }

    public string? PatternDescription
    {
        get => _patternDescription;
        set => this.RaiseAndSetIfChanged(ref _patternDescription, value);
    }
    
    public string? PatternName
    {
        get => _patternName;
        set => this.RaiseAndSetIfChanged(ref _patternName, value);
    }

    public GameOfLifeState? PatternState
    {
        get => _patternState;
        set => this.RaiseAndSetIfChanged(ref _patternState, value);
    }

    public ReactiveCommand<Unit, Unit> TickCommand { get; }

    public int TickNumber
    {
        get => _tickNumber;
        set => this.RaiseAndSetIfChanged(ref _tickNumber, value);
    }
    
    public ReactiveCommand<Unit, Unit> QuitCommand { get; }
    
    public ReactiveCommand<Unit, Unit> VisitWebsiteCommand { get; }

    void RunDeleteGridCommand()
    {
        SetSimulationData(null);
    }

    public async Task RunOpenCommand()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Open Game of Life pattern file",
                Filters = {
                    new FileDialogFilter {
                        Name = "Plaintext files (*.cells)",
                        Extensions = { "cells" },
                    },
                },
            };

            string[]? paths = await dialog.ShowAsync(desktop.MainWindow);

            if (paths is not null)
            {
                var fileInfo = new FileInfo(paths[0]);
                PlaintextFileContents fileContents = PlaintextFileReader.ReadFile(fileInfo);

                SetSimulationData(new GameOfLifeState(fileContents.State), fileContents.Name, fileContents.Description);
            }
        }
    }

    void RunTickCommand()
    {
        TickState();
    }

    void RunQuitCommand()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }

    void RunVisitWebsiteCommand()
    {
        VisitWebsiteInDefaultBrowser("https://github.com/Xyaneon/conways-game-of-life");
    }

    void SetSimulationData(GameOfLifeState? state, string? name = null, string? description = null)
    {
        PatternState = state;
        PatternName = name;
        PatternDescription = description;
        TickNumber = 0;
    }

    void TickState()
    {
        if (PatternState is not null)
        {
            bool[,] currentState = PatternState.To2DArray();
            bool[,] nextState = StateUpdater.GetNextState(currentState);
            PatternState = new GameOfLifeState(nextState);
            TickNumber++;
        }
    }

    void VisitWebsiteInDefaultBrowser(string url)
    {
        // Below code credit to this blog post:
        // https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
        try
        {
            Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }
}
