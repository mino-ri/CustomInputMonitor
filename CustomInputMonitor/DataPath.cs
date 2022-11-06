using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomInputMonitor;

public static class DataPath
{
    private static string AppPath => AppContext.BaseDirectory;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        WriteIndented = true,
    };

    private static string? _layout;
    public static string Layout { get => _layout ?? GetLayoutPath(); set => _layout = value ?? GetLayoutPath(); }

    public static string Theme => GetDataPath("theme.json");

    public static string GetDataPath(string relativePath) => Path.Combine(AppPath, relativePath);

    public static T? LoadJson<T>(string path)
    {
        try
        {
            using var stram = File.OpenRead(path);
            return JsonSerializer.Deserialize<T>(stram, JsonSerializerOptions);
        }
        catch (Exception ex)
        {
            Debug.Print(ex.ToString());
            return default;
        }
    }

    public static async Task<T?> LoadJsonAsync<T>(string path)
    {
        try
        {
            using var stram = File.OpenRead(path);
            return await JsonSerializer.DeserializeAsync<T>(stram, JsonSerializerOptions);
        }
        catch (Exception ex)
        {
            Debug.Print(ex.ToString());
            return default;
        }
    }

    public static void SaveJson<T>(string path, T value)
    {
        try
        {
            using var stram = File.Create(path);
            JsonSerializer.Serialize(stram, value, JsonSerializerOptions);
        }
        catch (Exception ex)
        {
            Debug.Print(ex.ToString());
        }
    }

    private static string GetLayoutPath()
    {
        var args = Environment.GetCommandLineArgs();
        if (args.Length >= 2 && File.Exists(args[1]))
        {
            return args[1];
        }

        return GetDataPath("layout.cinm");
    }
}
