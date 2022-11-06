using System.Windows;
using System.Windows.Media;

namespace CustomInputMonitor;

public class ThemeDefinition
{
    public static readonly ThemeDefinition Instance;

    public string Background { get; set; } = "";
    public string Text { get; set; } = "";
    public string Released { get; set; } = "";
    public string Pressed { get; set; } = "";
    public string Font { get; set; } = "";

    static ThemeDefinition()
    {
        Instance = DataPath.LoadJson<ThemeDefinition>(DataPath.Theme) ?? new ThemeDefinition
        {
            Background = "#FF000000",
            Text = "#FFFFFFFF",
            Released = "#FF2F4F4F",
            Pressed = "#FFD2691E",
            Font = "Meiryo UI",
        };
    }
}


public static class Theme
{
    public static Brush Background { get; }
    public static Brush Text { get; }
    public static Brush Released { get; }
    public static Brush Pressed { get; }
    public static Brush Close { get; }
    public static FontFamily Font { get; }

    static Theme()
    {
        static Brush? CreateBrush(string text, BrushConverter converter)
        {
            try
            {
                var brush = converter.ConvertFromInvariantString(text) as Brush;
                brush?.Freeze();
                return brush;
            }
            catch
            {
                return null;
            }
        }

        var converter = new BrushConverter();
        var definition = ThemeDefinition.Instance;
        Background = CreateBrush(definition.Background, converter) ?? Brushes.Black;
        Text = CreateBrush(definition.Text, converter) ?? Brushes.White;
        Released = CreateBrush(definition.Released, converter) ?? Brushes.DarkSlateGray;
        Pressed = CreateBrush(definition.Pressed, converter) ?? Brushes.Chocolate;
        Close = CreateBrush("#FFE81123", converter)!;
        FontFamily? font;
        try
        {
            font = (FontFamily?)new FontFamilyConverter().ConvertFromInvariantString(definition.Font);
        }
        catch
        {
            font = null;
        }

        Font = font ?? SystemFonts.MessageFontFamily;
    }
}
