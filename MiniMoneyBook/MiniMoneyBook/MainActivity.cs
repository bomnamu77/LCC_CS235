using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using SQLite;
using MiniMoneyBook.DAL;
using System;
using System.Linq;
using System.IO;
using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Series;

namespace MiniMoneyBook
{
    
    [Activity(Label = "MiniMoneyBook", MainLauncher = true, Icon ="@drawable/Icon")]
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
                
                var thisMonthBalance = (from s in db.Table<MoneyBook>()
                             where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth)
                             select s).Sum(s => s.Amount);

                var income = (from s in db.Table<MoneyBook>()
                                        where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth && s.I_E=="I")
                                        select s).Sum(s => s.Amount);

                var expense = (from s in db.Table<MoneyBook>()
                                        where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth && s.I_E == "E")
                                        select s).Sum(s => s.Amount);

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
                
            }

            //PieChart 
            PlotView view = FindViewById<PlotView>(Resource.Id.plot_view);
            view.Model = CreatePlotModel(db, firstDayOfMonth, lastDayOfMonth);
               
            //when click add button
            Button addButton = FindViewById<Button>(Resource.Id.addButton);

            addButton.Click += delegate
            {
                var input = new Intent(this, typeof(InputActivity));
                StartActivity(input);
            };

            //When click log button
            Button logButton = FindViewById<Button>(Resource.Id.logButton);

            logButton.Click += delegate
            {
                var log = new Intent(this, typeof(LogActivity));
                StartActivity(log);
            };
        }
        //Initialize database with category 
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
                    { I_E = "E", CategoryName = "Transportaion" });
                    db.Insert(new Category()
                    { I_E = "E", CategoryName = "Personals" });

                    db.Insert(new Category()
                    { I_E = "E", CategoryName = "Others" });


                }
            }
        }

        //create Pie chart for expense usage
        private PlotModel CreatePlotModel(SQLiteConnection db, DateTime firstDayOfMonth, DateTime lastDayOfMonth )
        {
            PlotModel plotModel = new PlotModel { Title = "Expense Usage Ratio", TitleColor=OxyColors.White };

            PieSeries seriesP1 = new PieSeries { StrokeThickness = 1.0, InsideLabelPosition = 0.8, AngleSpan = 360, StartAngle = 0, TextColor=OxyColors.White};

            lock (locker)
            {
                //get category that are used for this month
                var categoryList = (from s in db.Table<MoneyBook>()
                                    where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth && s.I_E == "E")
                                    select s).GroupBy(s => s.Category).Select(s => s.First());
                //for each category, get sum of a category
                foreach (var cat in categoryList)
                {
                    var sumCategory = (from s in db.Table<MoneyBook>()
                                       where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth && s.I_E == "E" && s.Category == cat.Category)
                                       select s).Sum(s => s.Amount);
                    seriesP1.Slices.Add(new PieSlice(cat.Category, Convert.ToDouble(sumCategory)) { IsExploded = true });
                }
            }
            plotModel.Series.Add(seriesP1);


            return plotModel;
        }
    }
}

