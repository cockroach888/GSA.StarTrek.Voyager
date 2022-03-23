using System.Windows.Input;

namespace MSWebView2WPF4Appx1.Commands
{
    internal static class BrowserCommands
    {
        public static RoutedCommand GoBackCommand = new();

        public static RoutedCommand GoForwardCommand = new();

        public static RoutedCommand NavigateCommand = new();

        public static RoutedCommand VirtualMappingCommand = new();

        public static RoutedCommand DevToolsCommand = new();

        public static RoutedCommand ChaoticRandomCommand = new();

        public static RoutedCommand SequentialRandomCommand = new();

        public static RoutedCommand SynchronizedRandomCommand = new();
    }
}
