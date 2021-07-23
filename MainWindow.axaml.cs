using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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

            string? path = await Dialog.GetFilePatch(rootWindow);
            
            if (path!=null)
                this.FindControl<TextBox>("TextBoxPathAddresses").Text = path;

           
            
        }

        private async void ButtonPathResult_OnClick(object? sender, RoutedEventArgs e)
        {
            var rootWindow = (Window) this.VisualRoot;
            string? path = await Dialog.GetFolderPatch(rootWindow);
            
            if (path != null)
                this.FindControl<TextBox>("TextBoxPathResult").Text = path;
        }


         async void ShowProgress(int progress,int max)
        {
            await Task.Delay(1000);
           this.FindControl<Label>("MessageLabel").Content=_i+" / "+max+" / key "+(_i-progress)+"S";
        }


   

        private bool _buttonPosition=false;
        private int _i = 0;

        async void Scan()
        {
            
            int _error = 0;
            _i = 0;
           
            if (_buttonPosition == true)
                _buttonPosition = false;
            else
                _buttonPosition = true;

     
           
            string filePatch = this.FindControl<TextBox>("TextBoxPathAddresses").Text;

            List<Address.Data> addressWithBalances = new List<Address.Data>(); 
            
            
            
            List<Address.Data> addressDataList = new List<Address.Data>(); 
         
            
            
            await Task.Run(() =>
            {
            
                addressDataList = Address.Read(filePatch);
                
            });

            for (; _i<=addressDataList.Count & _buttonPosition==true; _i++)
            {

              var delay= this.FindControl<Slider>("DelaySlider").Value;

                    ShowProgress(_i,addressDataList.Count);

                   
                    new Thread(() =>
                    {
                        try
                        {
                            int i = _i;
                            var res=BalanceAll.Get(addressDataList[i].Address);

                            if (res.AllBalance!="0")
                            {
                                addressWithBalances.Add(addressDataList[i]);
                            }
                        }
                        catch (Exception exception)
                        {
                            
                            _error++;
                   
                        }
                    }).Start();

                
                  

                   

                    
                    
                    
                    if (_i > 3 & _i<addressDataList.Count-3)
                        this.FindControl<TextBox>("LastAddresses").Text = addressDataList[_i].Address + "\n" +
                                                                                addressDataList[_i+1].Address + "\n" +
                                                                                addressDataList[_i+2].Address;
                        
                    
                
                    try
                    {
                        this.FindControl<ProgressBar>("ProgressBar").Value=(((double)_i/addressDataList.Count)*100); 
                        this.FindControl<Label>("ErrorLabel").Content="Error:"+_error;
                    
                    }
                    catch (Exception exception)
                    {
                 
                    }
                
                    await Task.Delay(Convert.ToInt32(delay));

            }

         
            Address.Write(this.FindControl<TextBox>("TextBoxPathResult").Text ,addressWithBalances);
            
            _buttonPosition = false;
         

            
        }

        bool FieldsEmpty()
        {
            if (this.FindControl<TextBox>("TextBoxPathResult").Text == default |
                this.FindControl<TextBox>("TextBoxPathAddresses").Text == default)
                return true;
            else
                return false;
        }


        private async void ButtonScan_OnClick(object? sender, RoutedEventArgs e)
        {
            if (FieldsEmpty())
            { 
                this.FindControl<Label>("MessageLabel").Content = "You have not completed all the fields";
            }
            else
            { 
                Scan();
            }
           
        }
    }
}