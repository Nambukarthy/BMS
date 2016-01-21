using System;
using System.Collections.Generic;
using System.Text;

namespace BMS.Constants
{
    public sealed class ConstantUrl
    {
        public string Value { get; set; }
        public ConstantUrl(string value)
        {
            Value = value;
        }
        public static ConstantUrl EventUrl
        {
            get { return new ConstantUrl("http://data.in.bookmyshow.com/getData.aspx?cmd=EVENTLIST&f=json&cc=&lt=&lg=&et=MT&dt=&sr=MWEST&t=a54a7b3aba576256614a"); }
        }
    }

    public sealed class ConstantImage
    {
        public string Value { get; set; }
        public ConstantImage(string value)
        {
            Value = value;
        }
        public static ConstantImage DefaultImage
        {
            get { return new ConstantImage("ms-appx:///Assets/Default.png"); }
        }

        public static ConstantImage PlaceHolderImage
        {
            get { return new ConstantImage("ms-appx:///Assets/PlaceHolder.png"); }
        }
    }

}
