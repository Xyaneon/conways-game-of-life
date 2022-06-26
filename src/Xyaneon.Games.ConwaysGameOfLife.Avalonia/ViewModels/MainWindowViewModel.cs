﻿using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Xyaneon.Games.ConwaysGameOfLife.Avalonia.Models;
using Xyaneon.Games.ConwaysGameOfLife.Core;
using Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

namespace Xyaneon.Games.ConwaysGameOfLife.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        DeleteGridCommand = ReactiveCommand.Create(RunDeleteGridCommand);
        OpenCommand = ReactiveCommand.CreateFromTask(RunOpenCommand);
        TickCommand = ReactiveCommand.Create(RunTickCommand);
        QuitCommand = ReactiveCommand.Create(RunQuitCommand);

        _patternState = null;
    }

    private string? _patternDescription;
    private string? _patternName;
    private GameOfLifeState? _patternState;
    private int _tickNumber;

    public string Greeting => "Welcome to Avalonia!";

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

    void RunDeleteGridCommand()
    {
        PatternName = null;
        PatternDescription = null;
        PatternState = null;
        TickNumber = 0;
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

                PatternName = fileContents.Name;
                PatternDescription = fileContents.Description;
                PatternState = new GameOfLifeState(fileContents.State);
                TickNumber = 0;
            }
        }
    }

    void RunTickCommand()
    {
        if (PatternState is not null)
        {
            bool[,] currentState = PatternState.To2DArray();
            bool[,] nextState = StateUpdater.GetNextState(currentState);
            PatternState = new GameOfLifeState(nextState);
            TickNumber++;
        }
    }

    void RunQuitCommand()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
}
