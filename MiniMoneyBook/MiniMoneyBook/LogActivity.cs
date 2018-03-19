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
            // Get the path to the database that was deployed in Assets
            string dbPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MoneyBook.db3");

            db = new SQLiteConnection(dbPath);

            //var selectDatePicker = FindViewById<DatePicker>(Resource.Id.selectDatePicker);



            var monthList = db.Table<MoneyBook>().GroupBy(s =>s.Date.Year+s.Date.Month).Select(s => s.First()).ToList();
             // HACK: gets around "Default constructor not found for type System.String" error
             // create string Array and bind it to ListView
             int count = monthList.Count;
            //var monthBalance = db.Table<MoneyBook>().GroupBy(s => s.Date.Year + s.Date.Month).ToList();
             //string[] monthListArray = new string[count];
             foreach(var monthData in monthList)
             //for (int i = 0; i < count; i++)
             {
                var firstDayOfMonth = new DateTime(monthData.Date.Year, monthData.Date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                var thisMonthBalance = (from s in db.Table<MoneyBook>()
                                        where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth)
                                        select s).Sum(s => s.Amount);

                var itemsInMonth = (from s in db.Table<MoneyBook>()
                                    where (s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth)
                                    select s).ToList();
                int itemsCount = itemsInMonth.Count;
                var mbViewItems = new MBViewItem[itemsCount];
                int j=0;
                foreach(var item in itemsInMonth)
                {
                    
                    mbViewItems[j++] = new MBViewItem { Date = item.Date.ToShortDateString(), I_E = item.I_E, Category = item.Category, Amount = item.Amount };
                }
                viewItems.Add(new MBView()
                {

                    Year = monthData.Date.Year.ToString(),
                    Month = monthData.Date.Month.ToString(),

                    Balance = thisMonthBalance,
                    MBViewItems = mbViewItems


                    
                                           
                });

            }


            // Wire in the adapter for this expandable list activity:
            var adapter = new MBViewAdaptor(this, viewItems);
            SetListAdapter(adapter);

            //MoneyBook dateMoneyBook =
            //    db.Get<MoneyBook>((from s in db.Table<MoneyBook>() select s).Min(s => s.ID));
            //DateTime dateTime = dateMoneyBook.Date;
            //selectDatePicker.DateTime = dateTime;

            /*selectDatePicker.DateChanged += delegate
            {
                DateTime selectedDate = selectDatePicker.DateTime;
                DateTime nextDate = selectedDate.AddDays(1);
                var moneyBooks = (from s in db.Table<MoneyBook>()
                                  where (s.Date >= selectedDate && s.Date < nextDate)

                                  select s).ToList();
                // HACK: gets around "Default constructor not found for type System.String" error
                // create string Array and bind it to ListView
                int count = moneyBooks.Count;
                string[] moneyBookInfoArray = new string[count];
                for (int i = 0; i < count; i++)
                {
                    moneyBookInfoArray[i] =
                        moneyBooks[i].Date + "\t\t" + moneyBooks[i].I_E + "\t\t" + moneyBooks[i].Category + "\t\t" + moneyBooks[i].Amount;
                }


                ListAdapter = new ArrayAdapter<string>(this,
                     Android.Resource.Layout.SimpleListItem1,
                     moneyBookInfoArray);
            };*/

        }
    }
}