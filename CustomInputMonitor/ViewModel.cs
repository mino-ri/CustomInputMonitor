using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace CustomInputMonitor;

public sealed class ViewModel : ViewModelBase, IDisposable
{
    private readonly DispatcherTimer _timer;
    private readonly ActionCommand _addCommand;
    private readonly ActionCommand _removeCommand;

    private bool _inputting;
    public bool Inputting { get => _inputting; set => SetValue(ref _inputting, value); }

    private double _width = 488;
    public double Width { get => _width; set => SetValue(ref _width, value); }

    private double _height = 248;
    public double Height { get => _height; set => SetValue(ref _height, value); }

    public ICommand AddCommand => _addCommand;
    public ICommand RemoveCommand => _removeCommand;

    public ResettableObservableCollection<InputViewModel> Inputs { get; } = new();

    public ViewModel()
    {
        _timer = new DispatcherTimer(DispatcherPriority.Render);
        _timer.Tick += Tick;
        _timer.Interval = TimeSpan.FromSeconds(1.0 / 30.0);

        _addCommand = new ActionCommand(BeginAddCommand, false);
        _removeCommand = new ActionCommand(RemoveInput, false);
    }

    public void BeginAddCommand(object? value)
    {
        Inputting = true;
        _addCommand.SetCanExecute(false);
        _removeCommand.SetCanExecute(false);
    }

    public void AddInput(object? value)
    {
        var input = value switch
        {
            InputViewModel inputViewModel => inputViewModel,
            Key key => new InputViewModel { TargetKey = key },
            MouseButton button => new InputViewModel { TargetButton = button },
            _ => new InputViewModel(),
        };
        input.Caption = GetCaption(value);
        Inputs.Add(input);

        Inputting = false;
        _addCommand.SetCanExecute(true);
        _removeCommand.SetCanExecute(true);
    }

    public void RemoveInput(object? value)
    {
        if (value is InputViewModel inputViewModel)
        {
            Inputs.Remove(inputViewModel);
        }
    }

    public void Initialize()
    {
        async Task InitializeCore()
        {
            var layout = await DataPath.LoadJsonAsync<InputLayout>(DataPath.Layout);
            var inputs = layout?.Inputs ?? Array.Empty<InputViewModel>();

            Height = Math.Max(layout?.Height ?? 248d, 48d);
            Width = Math.Max(layout?.Width ?? 488d, 48d);
            Inputs.Reset(inputs);

            _addCommand.SetCanExecute(true);
            _removeCommand.SetCanExecute(true);
            _timer.Start();
        }

        _timer.Stop();
        InitializeCore().Forget();
    }

    public void Dispose()
    {
        DataPath.SaveJson(DataPath.Layout, new InputLayout
        {
            Inputs = Inputs.ToArray(),
            Height = Height,
            Width = Width,
        });
        _timer.Stop();
    }

    private void Tick(object? sender, EventArgs e)
    {
        foreach (var input in Inputs)
        {
            if (input.TargetKey != Key.None)
            {
                input.IsPressed = Keyboard.IsKeyDown(input.TargetKey);
            }
            // ウィンドウ外で押されたボタンが取得できない
            else if (input.TargetButton != (MouseButton)(-1))
            {
                var targetState = input.TargetButton switch
                {
                    MouseButton.Left => Mouse.PrimaryDevice.LeftButton,
                    MouseButton.Middle => Mouse.PrimaryDevice.MiddleButton,
                    MouseButton.Right => Mouse.PrimaryDevice.RightButton,
                    MouseButton.XButton1 => Mouse.PrimaryDevice.XButton1,
                    MouseButton.XButton2 => Mouse.PrimaryDevice.XButton2,
                    _ => MouseButtonState.Released,
                };

                input.IsPressed = targetState == MouseButtonState.Pressed;
            }
        }
    }

    private string GetCaption(object? value)
    {
        if (value is Key key)
        {
            return key switch
            {
                Key.D0 => "0",
                Key.D1 => "1",
                Key.D2 => "2",
                Key.D3 => "3",
                Key.D4 => "4",
                Key.D5 => "5",
                Key.D6 => "6",
                Key.D7 => "7",
                Key.D8 => "8",
                Key.D9 => "9",
                Key.NumPad0 => "0",
                Key.NumPad1 => "1",
                Key.NumPad2 => "2",
                Key.NumPad3 => "3",
                Key.NumPad4 => "4",
                Key.NumPad5 => "5",
                Key.NumPad6 => "6",
                Key.NumPad7 => "7",
                Key.NumPad8 => "8",
                Key.NumPad9 => "9",
                Key.Add => "+",
                Key.Subtract => "-",
                Key.Multiply => "*",
                Key.Divide => "/",
                Key.OemBackslash => "\\",
                Key.OemCloseBrackets => "]",
                Key.OemComma => ",",
                Key.OemMinus => "-",
                Key.OemOpenBrackets => "[",
                Key.OemPeriod => ".",
                Key.OemPipe => "|",
                Key.OemPlus => "+",
                Key.OemQuestion => "?",
                Key.OemQuotes => "\"",
                Key.OemSemicolon => ";",
                Key.OemTilde => "'",
                _ => key.ToString(),
            };
        }

        return "" + value;
    }
}
