using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BMS.Models
{
    public class Event : BindableBase
    {
        public string Actors { get; set; }

        private string certified;
        public string Certified
        {
            get
            {
                if (!string.IsNullOrEmpty(EventTitle) && this.EventTitle.ToLower().Contains("(u)"))
                    certified = "(u)";
                else if (!string.IsNullOrEmpty(EventTitle) && this.EventTitle.ToLower().Contains("(u/a)"))
                    certified = "(u/a)";
                else if (!string.IsNullOrEmpty(EventTitle) && this.EventTitle.ToLower().Contains("(a)"))
                    certified = "(a)";

                return certified;
            }
            set { SetProperty(ref certified, value); }
        }
        public string Director { get; set; }
        public string EventCode { get; set; }

        private string eventTitle;
        public string EventTitle
        {
            get { return eventTitle; }
            set { SetProperty(ref eventTitle, value); }
        }
        public string Event_strLandingPageURL { get; set; }
        public string Genre { get; set; }

        public string ImageCode { get; set; }

        //Setting PlaceHolder image (static image) since imageurl is not in feed.
        private string imageUrl = "ms-appx:///Assets/PlaceHolder.png";

        public string ImageUrl
        {
            get
            {
                return imageUrl;
            }
            set { SetProperty(ref imageUrl, value); }
        }
        public string Language { get; set; }
        public string Length { get; set; }

        private string movieName;
        public string MovieName
        {
            get
            {
                if (!string.IsNullOrEmpty(EventTitle) && this.EventTitle.ToLower().Contains("(u)"))
                    movieName = (EventTitle.ToLower().Replace("(u)", string.Empty)).ToUpper();
                else if (!string.IsNullOrEmpty(EventTitle) && this.EventTitle.ToLower().Contains("(u/a)"))
                    movieName = (EventTitle.ToLower().Replace("(u/a)", string.Empty)).ToUpper();
                else if (!string.IsNullOrEmpty(EventTitle) && this.EventTitle.ToLower().Contains("(a)"))
                    movieName = (EventTitle.ToLower().Replace("(a)", string.Empty)).ToUpper();
                else
                    movieName = string.IsNullOrEmpty(EventTitle) ? EventTitle : EventTitle.ToUpper();

                return movieName;
            }
            set { SetProperty(ref movieName, value); }
        }

        private double movieRating;
        public double MovieRating
        {
            get
            {
                double.TryParse(Ratings, out movieRating);

                if (movieRating > 0)
                    movieRating = movieRating / 2;

                return movieRating;
            }
            set { SetProperty(ref movieRating, value); }
        }
        private double rate;
        public double Rate
        {
            get { return rate; }
            set { SetProperty(ref rate, value); }
        }
        public string Ratings { get; set; }       
        public string ReleaseDt { get; set; }       
        public string Seq { get; set; }
        public string Show_Code { get; set; }
        public string strMessage { get; set; }

        private string trailerUrl;
        public string TrailerURL
        {
            get { return trailerUrl; }
            set { trailerUrl = value; }
        }
    }

    public class BookMyShow
    {
        public ObservableCollection<Event> Event { get; set; }
    }

    public class RootEvent
    {
        public BookMyShow BookMyShow { get; set; }
    }
}
