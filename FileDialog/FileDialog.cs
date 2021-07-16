using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;


namespace BitcoinFastSearch.FileDialog
{
    public static class Dialog
    {
        public static async Task<string?> GetFilePatch(Window mainWindow)
        {
            var dialog = new OpenFileDialog();
            //(Window)this.VisualRoot
            var result = await dialog.ShowAsync(mainWindow);

            if (result != null)
                return result[0];
            else
                return default;

        }
        public static async Task<string?> GetFolderPatch(Window mainWindow)
        {
            var dialog = new OpenFolderDialog();
            
            var result = await dialog.ShowAsync(mainWindow);

            if (result != null)
                return result;
            else
                return default;

        }


   
    }
}