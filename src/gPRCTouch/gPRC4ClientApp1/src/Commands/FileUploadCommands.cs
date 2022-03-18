using System.Windows.Input;

namespace gPRC4ClientApp1.Commands
{
    internal static class FileUploadCommands
    {
        public static RoutedCommand FileBrowserCommand { get; } = new();

        public static RoutedCommand UploadCommand { get; } = new();
    }
}
