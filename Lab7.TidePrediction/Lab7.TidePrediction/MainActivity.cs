using System;
using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using Lab7.TidePrediction.DAL;
using System.Linq;
using Android.Content;

namespace Lab7.TidePrediction
{
    [Activity(Label = "Lab7.TidePrediction", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            /* ------ copy and open the dB file using the SQLite-Net ORM ------ */

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

            /* ------ Spinner initialization ------ */

            // Initialize the adapter for the spinner with stock symbols
            var distinctTides = db.Table<Tide>().GroupBy(s => s.Location).Select(s => s.First());
            var tideLocation = distinctTides.Select(s => s.Location).ToList();
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, tideLocation);

            var locationSpinner = FindViewById<Spinner>(Resource.Id.locationSpinner);
            locationSpinner.Adapter = adapter;

            // Event handler for selected spinner item
            string selectedLocation = "";
            locationSpinner.ItemSelected += delegate (object sender, AdapterView.ItemSelectedEventArgs e) {
                Spinner spinner = (Spinner)sender;
                selectedLocation = (string)spinner.GetItemAtPosition(e.Position);
            };

            /* ------- DatePicker initialization ------- */

            var tideDatePicker = FindViewById<DatePicker>(Resource.Id.tideDatePicker);

            Tide dateTide =
                db.Get<Tide>((from s in db.Table<Tide>() select s).Min(s => s.ID));
            DateTime dateTime = dateTide.DateTimes;
            tideDatePicker.DateTime = dateTime;

            /* ------- Query for selected stock prices -------- */

            Button listViewButton = FindViewById<Button>(Resource.Id.listViewButton);
            
            listViewButton.Click += delegate
            {
                var second = new Intent(this, typeof(SecondActivity));
                second.PutExtra("Location", selectedLocation);
                second.PutExtra("Date", tideDatePicker.DateTime.ToString());
                StartActivity(second);
            };
        }
    }
}

