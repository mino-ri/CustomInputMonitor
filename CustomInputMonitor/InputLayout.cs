using System;

namespace CustomInputMonitor;

public class InputLayout
{
    public InputViewModel[] Inputs { get; set; } = Array.Empty<InputViewModel>();
    public double Width { get; set; }
    public double Height { get; set; }
}
