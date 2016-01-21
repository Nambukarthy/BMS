using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace BMS.ViewModels
{
    public class TrailerWebviewPageViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;

        
        private string webViewSource;
        public string WebViewSource
        {
            get { return this.webViewSource; }
            set
            {
                SetProperty(ref this.webViewSource, value);
            }            
        }

        public TrailerWebviewPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;            
        }

        public override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            var Source = navigationParameter as string;
            this.WebViewSource = Source;            

#if WINDOWS_PHONE_APP
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
#endif
        }

       

#if WINDOWS_PHONE_APP
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {

            var currentFrame = Window.Current.Content as Frame;
            if (currentFrame == null)
            {
                return;
            }
            else if (currentFrame.CanGoBack)
            {
                currentFrame.GoBack();
                e.Handled = true;
            }
           
        }

        public override void OnNavigatedFrom(Dictionary<string, object> viewModelState, bool suspending)
        {
            base.OnNavigatedFrom(viewModelState, suspending);

            if (!suspending)
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            }
        }
#endif
    }
}
