using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Lab6.TidePrediction
{
    public class TideItem
    {

        public string Date { get; set; }
        public string Day { get; set; }
        public string Time { get; set; }
        public string Height { get; set; }
        public string Hi_Low { get; set; }
        public string Month { get; set; }



        public TideItem(string date, string day, string t, string h, string hl)
        {

            Date = date;
            Day = day;
            Time = t;
            Height = h;
            Hi_Low = hl;
            
            string temp = date.Substring(5, 2);
            // Get month data and convert to text month
            switch (temp)
            {
            case "01":
                Month = "Jan";
                break;
            case "02":
                Month = "Feb";
                break;
            case "03":
                Month = "Mar";
                break;
            case "04":
                Month = "Apr";
                break;
            case "05":
                Month = "May";
                break;
            case "06":
                Month = "Jun";
                break;
            case "07":
                Month = "Jul";
                break;
            case "08":
                Month = "Aug";
                break;
            case "09":
                Month = "Sep";
                break;
            case "10":
                Month = "Oct";
                break;
            case "11":
                Month = "Nov";
                break;
            case "12":
                Month = "Dec";
                break;

            }
    
        }

        public override string ToString()
        {
            return Date;
        }
    }
}