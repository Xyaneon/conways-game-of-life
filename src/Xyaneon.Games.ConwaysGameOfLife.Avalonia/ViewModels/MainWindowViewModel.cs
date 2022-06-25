﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System.Reactive;

namespace Xyaneon.Games.ConwaysGameOfLife.Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() {
            QuitCommand = ReactiveCommand.Create(RunQuitCommand);
        }

        public string Greeting => "Welcome to Avalonia!";

        public ReactiveCommand<Unit, Unit> QuitCommand { get; }

        void RunQuitCommand()
        {
            Application? currentApplication = Application.Current;
            if (currentApplication != null)
            {
                IApplicationLifetime? lifetime = currentApplication.ApplicationLifetime;
                if (lifetime != null)
                {
                    var classicDesktopLifetime = (IClassicDesktopStyleApplicationLifetime)lifetime;
                    classicDesktopLifetime.Shutdown();
                }
            }
        }
    }
    
}
