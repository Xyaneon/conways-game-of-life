using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System.Reactive;

namespace Xyaneon.Games.ConwaysGameOfLife.Avalonia.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        QuitCommand = ReactiveCommand.Create(RunQuitCommand);
    }

    public string Greeting => "Welcome to Avalonia!";

    public ReactiveCommand<Unit, Unit> QuitCommand { get; }

    void RunQuitCommand()
    {
        if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
}
