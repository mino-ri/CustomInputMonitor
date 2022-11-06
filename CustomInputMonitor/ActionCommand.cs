using System;
using System.Windows.Input;

namespace CustomInputMonitor;

public class ActionCommand : ICommand
{
    private readonly Action<object?> _action;
    private bool _canExecute;

    public event EventHandler? CanExecuteChanged;

    public ActionCommand(Action<object?> action, bool canExecute = false)
    {
        _action = action;
        _canExecute = canExecute;
    }

    public void SetCanExecute(bool value)
    {
        if (_canExecute == value) return;
        _canExecute = value;
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CanExecute(object? parameter) => _canExecute;


    public void Execute(object? parameter) => _action(parameter);
}
