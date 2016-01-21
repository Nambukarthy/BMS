using BMS.Models;
using BMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BMS.DesignTime
{
    public class MoviesPageDesignTimeViewModel
    {
        public MoviesPageDesignTimeViewModel()
        {
#if WINDOWS_PHONE_APP
            this.IsMainGrid = true;
            EventList = new List<Event>();
            for (int i = 0; i < 3; i++)
            {
                EventList.Add(new Event()
                {
                    ImageUrl = "ms-appx:///Assets/Default.png",
                    Rate = 3.5,
                    Language = "English",
                    Certified = "(u/a)",
                    MovieName = "WildChild"
                });
            }
#endif

#if WINDOWS_APP
            EventList = new List<Event>();
            for (int i = 0; i < 20; i++)
            {
                EventList.Add(new Event()
                {
                    ImageUrl = "ms-appx:///Assets/PlaceHolder.png",                  
                    EventTitle = "WildChild (U/A)"
                });
            }
#endif
        }

        private List<Event> eventList;
        public List<Event> EventList
        {
            get { return eventList; }
            set { eventList = value; }
        }

        private bool isMainGrid;
        public bool IsMainGrid
        {
            get { return isMainGrid; }
            set { isMainGrid = value; }
        }
    }
}
