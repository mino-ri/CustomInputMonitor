using System.Text.Json.Serialization;

namespace CustomInputMonitor;

public class InputViewModel : ViewModelBase
{
    public InputViewModel()
    {
        _targetKey = VirtualKeyCode.None;
    }

    private VirtualKeyCode _targetKey;
    public VirtualKeyCode TargetKey { get => _targetKey; set => SetValue(ref _targetKey, value); }

    private string _caption = "";
    public string Caption { get => _caption; set => SetValue(ref _caption, value); }

    private double _left;
    public double Left { get => _left; set => SetValue(ref _left, value); }

    private double _top;
    public double Top { get => _top; set => SetValue(ref _top, value); }

    private double _width = 48;
    public double Width { get => _width; set => SetValue(ref _width, value); }

    private double _height = 48;
    public double Height { get => _height; set => SetValue(ref _height, value); }

    private bool _pressed;
    [JsonIgnore]
    public bool IsPressed { get => _pressed; set => SetValue(ref _pressed, value); }

    private bool _editting;
    [JsonIgnore]
    public bool Editting { get => _editting; set => SetValue(ref _editting, value); }
}
