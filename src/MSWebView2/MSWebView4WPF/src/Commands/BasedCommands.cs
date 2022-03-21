using System.Windows.Input;

namespace MSWebView4WPF.Commands
{
    internal static class BasedCommands
    {
        public static RoutedCommand FileBrowserCommand { get; } = new();

        public static RoutedCommand FolderBrowserCommand { get; } = new();

        public static RoutedCommand LoadImageCommand { get; } = new();
    }
}
