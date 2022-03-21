using System.Windows.Input;

namespace MSWebView4WPF.Commands
{
    internal static class BrowserCommands
    {
        public static RoutedCommand GoBackCommand = new();

        public static RoutedCommand GoForwardCommand = new();

        public static RoutedCommand NavigateCommand = new();

        public static RoutedCommand VirtualMappingCommand = new();

        public static RoutedCommand DevToolsCommand = new();
    }
}
