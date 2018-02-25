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


        

        public TideItem(string date, string day, string t, string h, string hl)
        {
            
            Date = date;
            Day = day;
            Time = t;
            Height = h;
            Hi_Low = hl;
        }
/*
        public override string ToString()
        {
            return Spanish;
        }*/
    }
}