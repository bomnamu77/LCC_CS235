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
using System.IO;

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
            DateTime dt_Date = DateTime.Parse(date);
			DateTime nextDate = dt_Date.AddDays(1);

			//ListView tidesListView = FindViewById<ListView>(Resource.Id.tidesListView);
			string dbPath = "";
			SQLiteConnection db = null;
			// Get the path to the database that was deployed in Assets
			dbPath = Path.Combine(
				System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Tides.db3");

			// It seems you can read a file in Assets, but not write to it
			// so we'll copy our file to a read/write location
			using (Stream inStream = Assets.Open("Tides.db3"))
			using (Stream outStream = File.Create(dbPath))
				inStream.CopyTo(outStream);

			// Open the database
			db = new SQLiteConnection(dbPath);
			var tides = (from s in db.Table<Tide>()
                            where (s.Location == location)
							&& (s.DateTimes >= dt_Date) && (s.DateTimes <= nextDate)
						 select s).ToList();
            // HACK: gets around "Default constructor not found for type System.String" error
            int count = tides.Count;
            string[] tideInfoArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                tideInfoArray[i] =
                    tides[i].DateTimes.Month + "/" + tides[i].DateTimes.Day + "\t\t" + tides[i].Day + "\t\t" + tides[i].DateTimes.TimeOfDay + "\n" + 
                        tides[i].Height+ "\t\t" + tides[i].H_L;
            }

            
            ListAdapter = new ArrayAdapter<string>(this,
                 Android.Resource.Layout.SimpleListItem1,
                 tideInfoArray);
        }
    }
}