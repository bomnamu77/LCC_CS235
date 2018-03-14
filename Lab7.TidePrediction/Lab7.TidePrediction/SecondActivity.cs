using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
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

            // get location and date information from main activity
            string location = Intent.GetStringExtra("Location");
            string date = Intent.GetStringExtra("Date");


            //convert date data to appropriate format
            DateTime dt_Date = DateTime.Parse(date);
            string str_Month = dt_Date.Month.ToString();
            string str_Day = dt_Date.Month.ToString();
            if (str_Month.Length == 1)  // make Month data two digits
                str_Month = "0" + str_Month;
            if (str_Day.Length == 1)    // make Month data two digits
                str_Day = "0" + str_Day;
            string str_Date = dt_Date.Year + "/" + str_Month + "/" + str_Day;
            

            
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
                            && (s.Date == str_Date)
						 select s).ToList();
            // HACK: gets around "Default constructor not found for type System.String" error
            // create string Array and bind it to ListView
            int count = tides.Count;
            string[] tideInfoArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                tideInfoArray[i] =
                    tides[i].Date + "\t\t" + tides[i].Day + "\t\t" + tides[i].Time + "\n" +
                    tides[i].Height+ " ft \t\t" + tides[i].H_L;
            }

            
            ListAdapter = new ArrayAdapter<string>(this,
                 Android.Resource.Layout.SimpleListItem1,
                 tideInfoArray);
        }
    }
}