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
            Background = "#FFE1E1E0",
            Text = "#FFFFFFFF",
            Released = "#FF3E6A28",
            Pressed = "#FFF29100",
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
        Background = CreateBrush(definition.Background, converter) ?? CreateBrush("#FFE0E0E0", converter)!;
        Text = CreateBrush(definition.Text, converter) ?? CreateBrush("#FFFFFFFF", converter)!;
        Released = CreateBrush(definition.Released, converter) ?? CreateBrush("#FF3E6A28", converter)!;
        Pressed = CreateBrush(definition.Pressed, converter) ?? CreateBrush("#FFFFAE00", converter)!;
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
