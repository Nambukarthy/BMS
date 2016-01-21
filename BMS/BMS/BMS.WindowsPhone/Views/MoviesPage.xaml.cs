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
using BMS.ViewModels;

namespace BMS.Views
{
    public sealed partial class MoviesPage : VisualStateAwarePage
    {
        double PreviousVerticalOffset = 0;

        public MoviesPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode != NavigationMode.Back)
                PreviousVerticalOffset = 0;
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            ScrollViewer viewer = GetScrollViewer(this.lstEvents);
            viewer.ViewChanged += MainPage_ViewChanged;
        }

        private void MainPage_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            MoviesPageViewModel vm = DataContext as MoviesPageViewModel;
            ScrollViewer view = (ScrollViewer)sender;
            double progress = view.VerticalOffset / view.ScrollableHeight;

            if ((view.VerticalOffset - PreviousVerticalOffset) > (vm.PageHeight / 2) && !vm.Endoflist)
            {
                PreviousVerticalOffset = view.VerticalOffset;
                vm.AddImageToList();
            }
            if (progress > 0.7 && !vm.Endoflist)
            {
                vm.Endoflist = true;
                vm.AddImageToList();
            }
        }

        public static ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer) return depObj as ScrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = GetScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }



    }
}
