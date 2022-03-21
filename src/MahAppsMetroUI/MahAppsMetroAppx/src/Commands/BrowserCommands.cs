using System.Windows.Input;

namespace MahAppsMetroAppx.Commands
{
    internal static class BrowserCommands
    {
        public static RoutedCommand GoBackCommand = new();

        public static RoutedCommand GoForwardCommand = new();

        public static RoutedCommand NavigateCommand = new();

        public static RoutedCommand VirtualMappingCommand = new();
    }
}
