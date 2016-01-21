using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Controls;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;
using System.Globalization;
using BMS.Models;
using BMS.Constants;

namespace BMS.ViewModels
{
    public class MoviesPageViewModel : ViewModel
    {        
        public MoviesPageViewModel(INavigationService navigationService)
        {
            this._navigationService = navigationService;
            this.IsMainGrid = true;
            MoviesItemsCommand = new DelegateCommand<ItemClickEventArgs>(MoviesClickedMethod);
            MovieTrailerTappedCommand = new DelegateCommand(MovieTrailerTappedMethod);
        }

        public async override void OnNavigatedTo(object navigationParameter, Windows.UI.Xaml.Navigation.NavigationMode navigationMode, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

#if WINDOWS_PHONE_APP
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            if (navigationMode != NavigationMode.Back)
            {
                ClearProperties();
                await GetResponse();
            }

#endif

#if WINDOWS_APP
            await GetResponse();
#endif
        }

        public async Task GetResponse()
        {
            try
            {
                IsProgressRing = true;

                EventList = new ObservableCollection<Event>();
                StoredEventList = new ObservableCollection<Event>();
                ApiData = new RootEvent();
                HttpClient client = new HttpClient();

                string Result = await client.GetStringAsync(new System.Uri(ConstantUrl.EventUrl.Value));
                ApiData = JsonConvert.DeserializeObject<RootEvent>(Result);

                if (ApiData == null || ApiData.BookMyShow == null || ApiData.BookMyShow.Event == null) return;

                StoredEventList = apiData.BookMyShow.Event;

#if WINDOWS_PHONE_APP
                int count = 0;

                foreach (var item in apiData.BookMyShow.Event)
                {
                    if (count < 3)
                        EventList.Add(new Event()
                        {
                            ImageUrl = item.ImageUrl,
                            Rate = item.MovieRating,
                            EventTitle = item.EventTitle,
                            Language = item.Language,
                            Certified = item.Certified,
                            MovieName = item.MovieName,
                            Actors = item.Actors,
                            Director = item.Director,
                            Genre = item.Genre,
                            Length = item.Length,
                            strMessage = item.strMessage,
                            ReleaseDt = item.ReleaseDt,
                            TrailerURL = item.TrailerURL
                        });
                    else
                        EventList.Add(new Event()
                        {
                            ImageUrl = ConstantImage.DefaultImage.Value,
                            Rate = item.MovieRating,
                            EventTitle = item.EventTitle,
                            Language = item.Language,
                            Certified = item.Certified,
                            MovieName = item.MovieName,
                            Actors = item.Actors,
                            Director = item.Director,
                            Genre = item.Genre,
                            Length = item.Length,
                            strMessage = item.strMessage,
                            ReleaseDt = item.ReleaseDt,
                            TrailerURL = item.TrailerURL
                        });

                    count++;
                }
#endif

#if WINDOWS_APP
                foreach (var item in apiData.BookMyShow.Event)
                {
                    EventList.Add(new Event()
                    {
                        ImageUrl = item.ImageUrl,
                        Rate = item.MovieRating,
                        EventTitle = item.EventTitle,
                        Language = item.Language,
                        Certified = item.Certified,
                        MovieName = item.MovieName,
                        Actors = item.Actors,
                        Director = item.Director,
                        Genre = item.Genre,
                        Length = item.Length,
                        strMessage = item.strMessage,
                        ReleaseDt = item.ReleaseDt,
                        TrailerURL = item.TrailerURL
                    });
                }
#endif

                this.IsProgressRing = false;
            }
            catch (Exception ex)
            {
                this.IsProgressRing = false;
            }
        }

        public void AddImageToList()
        {
            int count = 0;

            for (int i = this.AddedImageCount; i < StoredEventList.Count; i++)
            {
                if (EventList[i].ImageUrl == ConstantImage.DefaultImage.Value)
                {
                    if (!Endoflist)
                        if (count < 2)
                            EventList[i].ImageUrl = StoredEventList[i].ImageUrl;
                        else
                        {
                            this.AddedImageCount = i;
                            return;
                        }
                    else
                    {
                        EventList[i].ImageUrl = StoredEventList[i].ImageUrl;
                        this.AddedImageCount = i;
                    }

                    count++;
                }
            }
        }

        private void ClearProperties()
        {
            this.AddedImageCount = 0;
            this.EventList = null;
            this.StoredEventList = null;
            this.Endoflist = false;
            this.ApiData = null;
        }

        private void MoviesClickedMethod(ItemClickEventArgs args)
        {
#if WINDOWS_PHONE_APP
            this.IsMainGrid = false;
#endif
            var selectedItem = args.ClickedItem as Event;

            if (selectedItem != null)
            {
                this.Actors = selectedItem.Actors;
                this.Director = selectedItem.Director;
                this.EventTitle = selectedItem.EventTitle;
                this.Genre = selectedItem.Genre;
                this.ImageUrl = selectedItem.ImageUrl;
                this.Length = selectedItem.Length;
                this.MovieRating = selectedItem.Rate;
                this.ReleaseDt = !string.IsNullOrEmpty(selectedItem.ReleaseDt) && selectedItem.ReleaseDt.Length > 10 ?
                           selectedItem.ReleaseDt.Substring(0, selectedItem.ReleaseDt.Length - 9) :
                           selectedItem.ReleaseDt;
                this.StrMessage = selectedItem.strMessage;                              
                this.TrailerURL = selectedItem.TrailerURL;                             
            }
#if WINDOWS_APP
            _navigationService.Navigate("MoviesDetails", selectedItem);
#endif

        }

        private void MovieTrailerTappedMethod()
        {
            _navigationService.Navigate("TrailerWebview", this.TrailerURL);
        }

#if WINDOWS_PHONE_APP
        private void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {

            if (!this.IsMainGrid)
            {
                this.IsMainGrid = true;
                return;
            }

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
        public DelegateCommand<ItemClickEventArgs> MoviesItemsCommand { get; private set; }
        public DelegateCommand MovieTrailerTappedCommand { get; private set; }

        private string actors;
        public string Actors
        {
            get { return actors; }
            set { SetProperty(ref actors, value); }
        }

        private int addedImageCount = 0;
        public int AddedImageCount
        {
            get { return addedImageCount; }
            set { addedImageCount = value; }
        }

        private RootEvent apiData;
        public RootEvent ApiData
        {
            get { return apiData; }
            set { apiData = value; }
        }

        private string director;
        public string Director
        {
            get { return director; }
            set { SetProperty(ref director, value); }
        }

        private bool endoflist = false;
        public bool Endoflist
        {
            get { return endoflist; }
            set { endoflist = value; }
        }

        private ObservableCollection<Event> eventList;
        public ObservableCollection<Event> EventList
        {
            get { return eventList; }
            set { SetProperty(ref eventList, value); }
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
            get{return imageUrl;}          
            set { SetProperty(ref imageUrl, value); }
        }

        private bool isMainGrid = true;
        public bool IsMainGrid
        {
            get { return isMainGrid; }
            set { SetProperty(ref isMainGrid, value); }
        }

        private bool isProgressRing = false;
        public bool IsProgressRing
        {
            get { return isProgressRing; }
            set { SetProperty(ref isProgressRing, value); }
        }

        private string length;
        public string Length
        {
            get { return length; }
            set { SetProperty(ref length, value); }
        }

        private double movieRating;
        public double MovieRating
        {
            get{return movieRating;}           
            set { SetProperty(ref movieRating, value); }
        }

        private double pageHeight = Window.Current.Bounds.Height;
        public double PageHeight
        {
            get { return pageHeight; }
            set { pageHeight = value; }
        }

        private double pageWidth = Window.Current.Bounds.Width;
        public double PageWidth
        {
            get { return pageWidth; }
            set { pageWidth = value; }
        }

        private string releaseDt;
        public string ReleaseDt
        {
            get { return releaseDt; }
            set { SetProperty(ref releaseDt, value); }
        }

        private ObservableCollection<Event> storedEventList;
        public ObservableCollection<Event> StoredEventList
        {
            get { return storedEventList; }
            set { SetProperty(ref storedEventList, value); }
        }

        private string strMessage;
        public string StrMessage
        {
            get { return strMessage; }
            set { SetProperty(ref strMessage, value); }
        }

        private string trailerUrl;
        public string TrailerURL
        {
            get { return trailerUrl; }
            set { SetProperty(ref trailerUrl, value); }
        }
    }  
}
