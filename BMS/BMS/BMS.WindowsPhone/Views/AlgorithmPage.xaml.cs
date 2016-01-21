using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.StoreApps;
using Windows.UI.Xaml.Shapes;
using BMS.ViewModels;

namespace BMS.Views
{
    public sealed partial class AlgorithmPage : VisualStateAwarePage
    {
        public AlgorithmPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ClearGrid();

            //Getting datacontext of viewmodel and registering the event
            var vm = (AlgorithmPageViewModel)DataContext;
            vm.EventCalculateRectangle += new AlgorithmPageViewModel.DelegateCalculateRectangle(CalculateRectangle);
        }

        private void CalculateRectangle(int input)
        {
            ClearGrid();

            int SqrtNumber = 0;
            int rowCount = 0;
            int columnCount = 0;

            SqrtNumber = Convert.ToInt32(Math.Sqrt(input));

            if (Math.Sqrt(input) > SqrtNumber)
            {
                SqrtNumber = SqrtNumber + 1;
                rowCount = SqrtNumber - 1;
            }
            else
                rowCount = SqrtNumber;


            columnCount = SqrtNumber;

            for (int column = 0; column < columnCount; column++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
                myGrid.ColumnDefinitions[column].Width = GridLength.Auto;
            }

            for (int row = 0; row < rowCount; row++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
                myGrid.RowDefinitions[row].Height = GridLength.Auto;
            }

            Rectangle rtn;
            int i = 0;

            for (int row = 0; row < myGrid.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < myGrid.ColumnDefinitions.Count; column++)
                {
                    i++;
                    rtn = new Rectangle();
                    rtn.Width = 50;
                    rtn.Height = 50;
                    Thickness margin = rtn.Margin;
                    margin.Left = 1;
                    margin.Right = 1;
                    margin.Top = 1;
                    margin.Bottom = 1;
                    rtn.Margin = margin;
                    if ((i) > input)
                        rtn.Fill = Application.Current.Resources["LightPink"] as SolidColorBrush;
                    else
                        rtn.Fill = Application.Current.Resources["LightBlue"] as SolidColorBrush;

                    rtn.SetValue(Grid.ColumnProperty, column);
                    rtn.SetValue(Grid.RowProperty, row);
                    myGrid.Children.Add(rtn);
                }
            }

            var vm = (AlgorithmPageViewModel)DataContext;

            vm.RowCount = rowCount;
            vm.ColumnCount = columnCount;
            vm.BlankSpaceCount = i - input;
        }

        public void ClearGrid()
        {
            myGrid.Children.Clear();
            myGrid.ColumnDefinitions.Clear();
            myGrid.RowDefinitions.Clear();
        }

        //Restrict entering "." from user input
        private void TextBox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Contains('.'))
            {
                txt.Text = txt.Text.Replace(".", "");
                txt.SelectionStart = txt.Text.Length;
            }

            if (string.IsNullOrEmpty(txt.Text))
            {
                var vm = (AlgorithmPageViewModel)DataContext;
                vm.BlankSpaceCount = 0;
                vm.ColumnCount = 0;
                vm.RowCount = 0;
                ClearGrid();
            }
        }


    }
}
