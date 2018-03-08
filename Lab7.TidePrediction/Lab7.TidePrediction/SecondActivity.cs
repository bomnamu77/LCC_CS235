using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using Lab7.TidePrediction.DAL;

namespace Lab7.TidePrediction
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            string location = Intent.GetStringExtra("Location");
            string date = Intent.GetStringExtra("Date");
            DateTime dt_Date = DateTime.ParseExact(date, "yy/MM/dd", System.Globalization.CultureInfo.InvariantCulture);

            //ListView tidesListView = FindViewById<ListView>(Resource.Id.tidesListView);

            SQLiteConnection db = null;

            var tides = (from s in db.Table<Tide>()
                            where (s.Location == location)
                                && (s.Date == dt_Date)
                                
                            select s).ToList();
            // HACK: gets around "Default constructor not found for type System.String" error
            int count = tides.Count;
            string[] tideInfoArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                tideInfoArray[i] =
                    tides[i].Date.ToShortDateString() + "\t\t" + tides[i].Day + "\t\t" + tides[i].Time + "\t\t" + 
                        tides[i].Height+ "\t\t" + tides[i].H_L;
            }

            
            ListAdapter = new ArrayAdapter<string>(this,
                 Android.Resource.Layout.SimpleListItem1,
                 tideInfoArray);
        }
    }
}