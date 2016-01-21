using BMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BMS.DesignTime
{
    public class MoviesDetailsPageDesignTimeViewModel
    {
        public MoviesDetailsPageDesignTimeViewModel()
        {
            this.Director = "Nick Moore";
            this.EventTitle = "Wild Child (U/A)";
            this.Genre = "Comedy, Romantic";
            this.ImageUrl = "ms-appx:///Assets/PlaceHolder.png";                                
            this.Language = "English";
            this.Length = "1 hr 40 Mins";
        }

        private string director;
        public string Director
        {
            get { return director; }
            set { director = value; }
        }

        private string eventTitle;
        public string EventTitle
        {
            get{ return eventTitle;}          
            set { eventTitle = value; }
        }

        private string genre;
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }       
            set { imageUrl = value; }
        }

        private string language;
        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        private string length;
        public string Length
        {
            get { return length; }
            set { length = value; }
        }
    }
}
