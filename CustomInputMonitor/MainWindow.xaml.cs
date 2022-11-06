using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CustomInputMonitor;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Window_ContentRendered(object sender, EventArgs e)
    {
        WindowChrome.CaptionHeight = ActualHeight - 16;
        ((ViewModel)DataContext).Initialize();
    }

    private void Window_Closed(object sender, EventArgs e)
    {
        ((ViewModel)DataContext).Dispose();
    }

    private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
    {
        var x = (int)e.HorizontalChange / 12 * 12;
        var y = (int)e.VerticalChange / 12 * 12;

        if (x == 0 && y == 0) return;
        if (sender is not FrameworkElement { DataContext: InputViewModel vm }) return;

        vm.Left += x;
        vm.Top += y;
    }

    private void ThumbSize_DragDelta(object sender, DragDeltaEventArgs e)
    {
        var x = (int)e.HorizontalChange / 12 * 12;
        var y = (int)e.VerticalChange / 12 * 12;

        if (x == 0 && y == 0) return;
        if (sender is not FrameworkElement { DataContext: InputViewModel vm }) return;

        vm.Width = Math.Max(24.0, vm.Width + x);
        vm.Height = Math.Max(24.0, vm.Height + y);
    }

    private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        WindowChrome.CaptionHeight = ActualHeight - 16;
    }

    private void Window_Deactivated(object sender, EventArgs e)
    {
        Keyboard.ClearFocus();
    }

    private void CaptionTextBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is not TextBox { DataContext: InputViewModel vm }) return;
        if (!(bool)e.NewValue)
        {
            vm.Editting = false;
        }
    }

    private void Thumb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        if (sender is not Thumb { DataContext: InputViewModel vm } thumb) return;
        vm.Editting = true;
        Dispatcher
            .InvokeAsync(() =>
            {
                var textBox = (TextBox)thumb.FindName("CaptionTextBox");
                textBox.Focus();
                textBox.SelectAll();
            })
            .Task.Forget();
    }

    private void CaptionTextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (sender is not FrameworkElement { DataContext: InputViewModel vm }) return;
        vm.Editting = false;
    }

    private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        SystemCommands.CloseWindow(this);
    }

    private void Window_DragEnter(object sender, DragEventArgs e)
    {
        e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)
            ? DragDropEffects.Copy
            : DragDropEffects.None;
        e.Handled = true;
    }

    private void Window_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetData(DataFormats.FileDrop) is not string[] files || files.Length != 1) return;

        DataPath.Layout = files[0];
        ((ViewModel)DataContext).Initialize();
        e.Handled = true;
    }
}
