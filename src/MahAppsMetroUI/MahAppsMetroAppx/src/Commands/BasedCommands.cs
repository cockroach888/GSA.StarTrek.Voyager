using System.Windows.Input;

namespace MahAppsMetroAppx.Commands
{
    internal static class BasedCommands
    {
        public static RoutedCommand FileBrowserCommand { get; } = new();

        public static RoutedCommand LoadImageCommand { get; } = new();
    }
}
