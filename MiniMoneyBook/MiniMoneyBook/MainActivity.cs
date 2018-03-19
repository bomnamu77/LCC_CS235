using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using SQLite;
using MiniMoneyBook.DAL;
using System;
using System.Linq;
using System.IO;


namespace MiniMoneyBook
{
    
    [Activity(Label = "MiniMoneyBook", MainLauncher = true)]
    public class MainActivity : Activity
    {
        static object locker = new object();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            

            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            /* ------ copy and open the dB file using the SQLite-Net ORM ------ */

            
            SQLiteConnection db = null;
            // Get the path to the database that was deployed in Assets
            string dbPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MoneyBook.db3");

            // It seems you can read a file in Assets, but not write to it
            // so we'll copy our file to a read/write location
            //using (Stream inStream = Assets.Open("MoneyBook.db3"))
            //using (Stream outStream = File.Create(dbPath))
             //   inStream.CopyTo(outStream);

            // Open the database
            //db = new SQLiteConnection(dbPath);
            //dbPath = @"../../../MiniMoneyBook/Assets/MoneyBook.db3";

            //string dbPath = Directory.GetCurrentDirectory ();
            //string dbPath = @"../../Assets/MoneyBook.db3";
            //string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "MoneyBook.db3");
            db = new SQLiteConnection(dbPath);


            InitDb(db);
            TextView todayTextView = FindViewById<TextView>(Resource.Id.todayValueTextView);
            
            todayTextView.Text = System.DateTime.Today.ToShortDateString();

            TextView thisMonthTextView = FindViewById<TextView>(Resource.Id.thisMonthValueTextView);
            TextView incomeTextView = FindViewById<TextView>(Resource.Id.incomeValueTextView);
            TextView expenseTextView = FindViewById<TextView>(Resource.Id.expenseValueTextView);

            DateTime tempDate = DateTime.Today;
            var firstDayOfMonth = new DateTime(tempDate.Year, tempDate.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            lock (locker)
            {
                int temp = db.Table<MoneyBook>().Count();
                //Android.Widget.Toast.MakeText(this, temp.ToString(),
                 //     Android.Widget.ToastLength.Short).Show();
                //var sum = db.Get<MoneyBook>((from s in db.Table<MoneyBook>()
                //                             where (s.Date >= firstDayOfMonth) && (s.Date <= lastDayOfMonth)
                //                             select s).Sum(s => s.Amount));

                var thisMonthBalance = (from s in db.Table<MoneyBook>()
                             where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth)
                             select s).Sum(s => s.Amount);

                var income = (from s in db.Table<MoneyBook>()
                                        where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth && s.I_E=="I")
                                        select s).Sum(s => s.Amount);

                var expense = (from s in db.Table<MoneyBook>()
                                        where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth && s.I_E == "E")
                                        select s).Sum(s => s.Amount);

                //Tide dateTide =
                //  db.Get<Tide>((from s in db.Table<Tide>() select s).Min(s => s.ID));

                if (thisMonthBalance > 0)
                    thisMonthTextView.SetTextColor(Android.Graphics.Color.Green);
                else
                    thisMonthTextView.SetTextColor(Android.Graphics.Color.HotPink);
                thisMonthTextView.Text = "$"+thisMonthBalance.ToString();
                incomeTextView.Text = "$" + income.ToString();
                incomeTextView.SetTextColor (Android.Graphics.Color.Blue);
                expense = 0 - expense;
                expenseTextView.Text = "$" + expense.ToString();
                
                expenseTextView.SetTextColor(Android.Graphics.Color.Red);
                //thisMonthTextView.SetText(Resource.String.this_month);
                //thisMonthTextView.setText
            }

            Button addButton = FindViewById<Button>(Resource.Id.addButton);

            addButton.Click += delegate
            {
                var input = new Intent(this, typeof(InputActivity));
                //second.PutExtra("Location", selectedLocation);
                //second.PutExtra("Date", tideDatePicker.DateTime.ToString());
                StartActivity(input);
            };


            Button logButton = FindViewById<Button>(Resource.Id.logButton);

            logButton.Click += delegate
            {
                var log = new Intent(this, typeof(LogActivity));
                //second.PutExtra("Location", selectedLocation);
                //second.PutExtra("Date", tideDatePicker.DateTime.ToString());
                StartActivity(log);
            };
        }

        private static void InitDb(SQLiteConnection db)
        {

            lock (locker)
            {

                // Create tables if they don't exist
                db.CreateTable<MoneyBook>();
                //db.DeleteAll<MoneyBook>();

                db.CreateTable<Category>();
                //if Category is created newly, add category data
                if (db.Table<Category>().Count() == 0)
                {
                    //db.DeleteAll<Category>();

                    db.Insert(new Category()
                    { I_E = "I", CategoryName = "Salary" });
                    db.Insert(new Category()
                    { I_E = "I", CategoryName = "Bonus" });
                    db.Insert(new Category()
                    { I_E = "I", CategoryName = "Others" });

                    db.Insert(new Category()
                    { I_E = "E", CategoryName = "Food" });
                    db.Insert(new Category()
                    { I_E = "E", CategoryName = "Bills" });
                    db.Insert(new Category()
                    { I_E = "E", CategoryName = "Personals" });

                    db.Insert(new Category()
                    { I_E = "E", CategoryName = "Others" });


                }
            }
        }
    }
}

