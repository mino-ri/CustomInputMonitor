using System.Text.Json.Serialization;
using System.Windows.Input;

namespace CustomInputMonitor;

public class InputViewModel : ViewModelBase
{
    public InputViewModel()
    {
        _targetKey = Key.None;
        _targetButton = (MouseButton)(-1);
    }

    private Key _targetKey;
    public Key TargetKey { get => _targetKey; set => SetValue(ref _targetKey, value); }

    private MouseButton _targetButton;
    public MouseButton TargetButton { get => _targetButton; set => SetValue(ref _targetButton, value); }

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
