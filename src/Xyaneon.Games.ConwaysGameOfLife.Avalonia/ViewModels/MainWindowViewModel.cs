using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace Xyaneon.Games.ConwaysGameOfLife.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        OpenCommand = ReactiveCommand.CreateFromTask(RunOpenCommand);
        QuitCommand = ReactiveCommand.Create(RunQuitCommand);
    }

    public string Greeting => "Welcome to Avalonia!";

    public ReactiveCommand<Unit, Unit> OpenCommand { get; }
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
