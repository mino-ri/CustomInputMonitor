using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CustomInputMonitor;

internal static class TaskExtension
{
    internal static async void Forget(this Task task)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Debug.Print(ex.ToString());
        }
    }
}
