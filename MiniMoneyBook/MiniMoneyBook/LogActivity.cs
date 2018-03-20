using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MiniMoneyBook.DAL;
using SQLite;

namespace MiniMoneyBook
{
    [Activity(Label = "LogActivity")]
    public class LogActivity : ExpandableListActivity
    {
        List<MBView> viewItems = new List<MBView>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SQLiteConnection db = null;
            // Get the path to the database 
            string dbPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MoneyBook.db3");

            db = new SQLiteConnection(dbPath);

            //get months that have moneybook records
             var monthList = db.Table<MoneyBook>().GroupBy(s =>s.Date.Month).Select(s => s.First()).ToList();
            
                        
             foreach(var monthData in monthList)
             {
                var firstDayOfMonth = new DateTime(monthData.Date.Year, monthData.Date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                // query to get sum of each month's records 
                var thisMonthBalance = (from s in db.Table<MoneyBook>()
                                        where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth)
                                        select s).Sum(s => s.Amount);
                // query to get records of each month
                var itemsInMonth = (from s in db.Table<MoneyBook>()
                                    where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth)
                                    select s).ToList();
                int itemsCount = itemsInMonth.Count;
                //create an Array to add items in the second level
                var mbViewItems = new MBViewItem[itemsCount];
                int j=0;

                //add data for second level list to the array 
                foreach(var item in itemsInMonth)
                {
                    
                    mbViewItems[j++] = new MBViewItem { Date = item.Date.ToShortDateString(), I_E = item.I_E, Category = item.Category, Amount = item.Amount };
                }


                viewItems.Add(new MBView()
                {
                    //first level data
                    Year = monthData.Date.Year.ToString(),
                    Month = monthData.Date.Month.ToString(),
                    Balance = thisMonthBalance,
                    //Array for second level items
                    MBViewItems = mbViewItems

                                           
                });

            }


            // Wire in the adapter for this expandable list activity:
            var adapter = new MBViewAdaptor(this, viewItems);
            SetListAdapter(adapter);



        }
    }
}