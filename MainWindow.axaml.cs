using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BitcoinFastSearch.FileDialog;

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
    }
}