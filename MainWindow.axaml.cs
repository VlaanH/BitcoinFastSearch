using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BitcoinFastSearch.FileDialog;
using BitcoinFastSearch.Balance;
using BitcoinFastSearch.AddressData;

namespace BitcoinFastSearch
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private async void ButtonPathAddresses_OnClick(object? sender, RoutedEventArgs e)
        {
            var rootWindow = (Window) this.VisualRoot;

            this.FindControl<TextBox>("TextBoxPathAddresses").Text = await Dialog.GetFilePatch(rootWindow);
            
        }

        private async void ButtonPathResult_OnClick(object? sender, RoutedEventArgs e)
        {
            var rootWindow = (Window) this.VisualRoot;

            this.FindControl<TextBox>("TextBoxPathResult").Text = await Dialog.GetFolderPatch(rootWindow);
        }

        private void ButtonScan_OnClick(object? sender, RoutedEventArgs e)
        {
            string filePatch = this.FindControl<TextBox>("TextBoxPathAddresses").Text;

            var addressDataList = Address.Read(filePatch);
           
        }
    }
}