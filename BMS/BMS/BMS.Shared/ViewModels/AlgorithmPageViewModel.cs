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
    public class AlgorithmPageViewModel : ViewModel
    {
        public AlgorithmPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
            CalculateRectangleCommand = new DelegateCommand(CalculateRectangle);
        }

        public override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            ClearProperties();

#if WINDOWS_PHONE_APP
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
#endif
        }

        private void CalculateRectangle()
        {
            //Calling the method in cs page (method has written in CS page since UI element has used)
            if (EventCalculateRectangle != null)
                EventCalculateRectangle(this.UserInput);
        }

        public void ClearProperties()
        {
            this.BlankSpaceCount = 0;
            this.ColumnCount = 0;
            this.RowCount = 0;
            this.UserInput = 0;
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
            else
            {
                Application.Current.Exit();
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

        private readonly INavigationService _navigationService;
        public DelegateCommand CalculateRectangleCommand { get; private set; }

        public delegate void DelegateCalculateRectangle(int input);

        public event DelegateCalculateRectangle EventCalculateRectangle;

        private int blankSpaceCount;
        public int BlankSpaceCount
        {
            get { return this.blankSpaceCount; }
            set { SetProperty(ref this.blankSpaceCount, value); }
        }

        private int columnCount;
        public int ColumnCount
        {
            get { return this.columnCount; }
            set { SetProperty(ref this.columnCount, value); }
        }

        private int rowCount;
        public int RowCount
        {
            get { return this.rowCount; }
            set { SetProperty(ref this.rowCount, value); }
        }

        private int userInput;
        public int UserInput
        {
            get { return this.userInput; }
            set { SetProperty(ref this.userInput, value); }
        }
    }
}
