using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;

namespace CustomInputMonitor;

public sealed class ViewModel : ViewModelBase, IDisposable
{
    private readonly DispatcherTimer _timer;
    private readonly ActionCommand _addCommand;
    private readonly ActionCommand _removeCommand;
    private readonly byte[] _keyStates0 = new byte[256];
    private readonly byte[] _keyStates1 = new byte[256];
    private bool _keyStateFlag;

    private bool _inputting;
    public bool Inputting { get => _inputting; private set => SetValue(ref _inputting, value); }

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
        _keyStateFlag = false;
        WinApi.GetKeyboardState(_keyStates0);
        _keyStates0.CopyTo(_keyStates1.AsSpan());

        _addCommand.SetCanExecute(false);
        _removeCommand.SetCanExecute(false);
        Inputting = true;
    }

    public void AddInput(object? value)
    {
        var input = value switch
        {
            InputViewModel inputViewModel => inputViewModel,
            VirtualKeyCode key => new InputViewModel { TargetKey = key },
            _ => new InputViewModel(),
        };
        input.Caption = GetCaption(value);
        if (Inputs.Count != 0)
        {
            InputViewModel? maxInput = null;
            foreach (var vm in Inputs)
            {
                if (maxInput is null)
                {
                    maxInput = vm;
                }
                else if (maxInput.Top < vm.Top || maxInput.Top == vm.Top && maxInput.Left < vm.Left)
                {
                    maxInput = vm;
                }
            }

            input.Top = maxInput!.Top;
            input.Left = maxInput.Left + maxInput.Width;
        }

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
            var v = WinApi.GetAsyncKeyState(input.TargetKey);
            input.IsPressed = v < 0;
        }

        if (Inputting)
        {
            _keyStateFlag = !_keyStateFlag;
            var currentKeyState = _keyStateFlag ? _keyStates0 : _keyStates1;
            WinApi.GetKeyboardState(currentKeyState);
            var oldKeyState = _keyStateFlag ? _keyStates1 : _keyStates0;

            for (var i = 0; i < 256; i++)
            {
                if (currentKeyState[i] >= 0b1000_0000 && oldKeyState[i] < 0b1000_0000)
                {
                    AddInput((VirtualKeyCode)i);
                }
            }
        }
    }

    private string GetCaption(object? value)
    {
        if (value is VirtualKeyCode key)
        {
            return key switch
            {
                VirtualKeyCode.D0 => "0",
                VirtualKeyCode.D1 => "1",
                VirtualKeyCode.D2 => "2",
                VirtualKeyCode.D3 => "3",
                VirtualKeyCode.D4 => "4",
                VirtualKeyCode.D5 => "5",
                VirtualKeyCode.D6 => "6",
                VirtualKeyCode.D7 => "7",
                VirtualKeyCode.D8 => "8",
                VirtualKeyCode.D9 => "9",
                VirtualKeyCode.Numpad0 => "0",
                VirtualKeyCode.Numpad1 => "1",
                VirtualKeyCode.Numpad2 => "2",
                VirtualKeyCode.Numpad3 => "3",
                VirtualKeyCode.Numpad4 => "4",
                VirtualKeyCode.Numpad5 => "5",
                VirtualKeyCode.Numpad6 => "6",
                VirtualKeyCode.Numpad7 => "7",
                VirtualKeyCode.Numpad8 => "8",
                VirtualKeyCode.Numpad9 => "9",
                VirtualKeyCode.Add => "+",
                VirtualKeyCode.Subtract => "-",
                VirtualKeyCode.Multiply => "*",
                VirtualKeyCode.Divide => "/",
                VirtualKeyCode.OemComma => ",",
                VirtualKeyCode.OemMinus => "-",
                VirtualKeyCode.OemPeriod => ".",
                VirtualKeyCode.OemPlus => "+",
                VirtualKeyCode.Oem1 => ":",
                VirtualKeyCode.Oem2 => "?",
                VirtualKeyCode.Oem3 => "@",
                VirtualKeyCode.Oem4 => "[",
                VirtualKeyCode.Oem5 => "|",
                VirtualKeyCode.Oem6 => "]",
                VirtualKeyCode.Oem7 => "~",
                VirtualKeyCode.Oem102 => "\\",
                _ => key.ToString(),
            };
        }

        return "" + value;
    }
}
