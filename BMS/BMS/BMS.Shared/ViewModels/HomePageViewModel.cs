using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;

namespace BMS.ViewModels
{
    public class HomePageViewModel : ViewModel
    {       
        public HomePageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;

            TaskOneCommand = new DelegateCommand(TaskOneTapped);
            TaskTwoCommand = new DelegateCommand(TaskTwoTapped);
        }
        public override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

#if WINDOWS_PHONE_APP
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
#endif
        }
        private async void TaskOneTapped()
        {
            if (CheckNetworkConnection())
                _navigationService.Navigate("Movies", null);
            else
            {
                MessageDialog msgbox = new MessageDialog("Please check your internet connection.", "No Network !");
                await msgbox.ShowAsync();
            }
        }
        private void TaskTwoTapped()
        {
            _navigationService.Navigate("Algorithm", null);
        }
        public static bool CheckNetworkConnection()
        {
            bool IsConnected = NetworkInterface.GetIsNetworkAvailable();
            return IsConnected;                   
        }


#if WINDOWS_PHONE_APP
        private async void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
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
            else
            {
                MessageDialog msgbox = new MessageDialog("Are you sure? want to close this app.", "BMS: Windows Task");
                msgbox.Commands.Add(new UICommand("OK", OkCommand));
                msgbox.Commands.Add(new UICommand("Cancel"));
                await msgbox.ShowAsync();                  
            }
        }

        private void OkCommand(IUICommand command)
        {
            Application.Current.Exit();
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
        private readonly INavigationService _navigationService;
        public DelegateCommand TaskOneCommand { get; private set; }
        public DelegateCommand TaskTwoCommand { get; private set; }
    }
}
