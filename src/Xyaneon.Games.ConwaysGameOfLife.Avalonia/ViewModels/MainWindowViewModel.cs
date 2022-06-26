﻿using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using Xyaneon.Games.ConwaysGameOfLife.FileIO.Plaintext;

namespace Xyaneon.Games.ConwaysGameOfLife.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        OpenCommand = ReactiveCommand.CreateFromTask(RunOpenCommand);
        QuitCommand = ReactiveCommand.Create(RunQuitCommand);
    }

    private string? _patternDescription;
    private string? _patternName;

    public string Greeting => "Welcome to Avalonia!";

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

    public ReactiveCommand<Unit, Unit> QuitCommand { get; }

    public async Task RunOpenCommand()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var dialog = new OpenFileDialog();
            dialog.Title = "Open Game of Life pattern file";
            var plaintextFileFilter = new FileDialogFilter {
                Name = "Plaintext files (*.cells)",
                Extensions = { "cells" },
            };
            dialog.Filters.Add(plaintextFileFilter);

            var result = await dialog.ShowAsync(desktop.MainWindow);

            if (result is not null)
            {
                foreach (var path in result)
                {
                    System.Console.WriteLine($"path: {path}");
                    var fileInfo = new System.IO.FileInfo(path);
                    PlaintextFileContents fileContents = PlaintextFileReader.ReadFile(fileInfo);
                    PatternName = fileContents.Name;
                    PatternDescription = fileContents.Description;
                }
            }
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
