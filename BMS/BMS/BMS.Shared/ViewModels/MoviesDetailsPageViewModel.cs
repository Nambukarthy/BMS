using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using BMS.Models;

namespace BMS.ViewModels
{
    public class MoviesDetailsPageViewModel : ViewModel
    {       
        public MoviesDetailsPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
        }

        public override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            var selectedItem = navigationParameter as Event;

            if (selectedItem != null)
            {
                this.Actors = selectedItem.Actors;
                this.Director = selectedItem.Director;
                this.EventTitle = selectedItem.EventTitle;
                this.Genre = selectedItem.Genre;
                this.ImageUrl = selectedItem.ImageUrl;
                this.Language = selectedItem.Language;                
                this.Length = selectedItem.Length;
                this.Ratings = selectedItem.Rate;
                this.ReleaseDt = selectedItem.ReleaseDt;
                this.StrMessage = selectedItem.strMessage; 
            }
        }

        private readonly INavigationService _navigationService;

        private string actors;
        public string Actors
        {
            get { return actors; }
            set { SetProperty(ref actors, value); }
        }

        private string director;
        public string Director
        {
            get { return director; }
            set { SetProperty(ref director, value); }
        }

        private string eventTitle;
        public string EventTitle
        {
            get { return eventTitle; }
            set { SetProperty(ref eventTitle, value); }
        }

        private string genre;
        public string Genre
        {
            get { return genre; }
            set { SetProperty(ref genre, value); }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set { SetProperty(ref imageUrl, value); }
        }

        private string language;
        public string Language
        {
            get { return language; }
            set { SetProperty(ref language, value); }
        }

        private string length;
        public string Length
        {
            get { return length; }
            set { SetProperty(ref length, value); }
        }

        private double ratings;
        public double Ratings
        {
            get { return ratings; }
            set { SetProperty(ref ratings, value); }
        }

        private string releaseDt;
        public string ReleaseDt
        {
            get { return releaseDt; }
            set { SetProperty(ref releaseDt, value); }
        }

        private string strMessage;
        public string StrMessage
        {
            get { return strMessage; }
            set { SetProperty(ref strMessage, value); }
        }
    }
}
