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
   
    [Activity(Label = "InputActivity")]
    public class InputActivity : Activity
    {
        static object locker = new object();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.InputActivity);

           
            SQLiteConnection db = null;
            // Get the path to the database 
            string dbPath = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MoneyBook.db3");
            
            db = new SQLiteConnection(dbPath);

            string ieResult = checkI_E();

            var categorySpinner = FindViewById<Spinner>(Resource.Id.categorySpinner);

            refreshSpinner(db, ieResult, categorySpinner);
            var radioGroup= FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            radioGroup.CheckedChange += delegate
                {
                    ieResult = checkI_E();
                    refreshSpinner(db, ieResult, categorySpinner);
                };
           




            // Event handler for selected spinner item
            string selectedCategory = "";
            categorySpinner.ItemSelected += delegate (object sender, AdapterView.ItemSelectedEventArgs e) {
                Spinner spinner = (Spinner)sender;
                selectedCategory = (string)spinner.GetItemAtPosition(e.Position);
            };

            var inputDatePicker = FindViewById<DatePicker>(Resource.Id.inputDatePicker);
            EditText amountEditText = FindViewById<EditText>(Resource.Id.amountEditText);
            EditText memoEditText = FindViewById<EditText>(Resource.Id.memoEditText);
            Button submitButton = FindViewById<Button>(Resource.Id.submitButton);
            
            submitButton.Click += delegate
            {
                decimal amount = System.Convert.ToDecimal(amountEditText.Text);

                if (ieResult == "E")
                    amount = 0 - amount;
                lock (locker)
                {
                    int result = db.Insert(new MoneyBook()
                    {
                        I_E = ieResult,
                        Date = inputDatePicker.DateTime,
                        Amount = amount,
                        Category = selectedCategory,
                        Memo = memoEditText.Text
                    });


                    
                }

                
                var main = new Intent(this, typeof(MainActivity));

                StartActivity(main);
            };
        }

        private string checkI_E()
        {

            var incomeRadioButton = FindViewById<RadioButton>(Resource.Id.incomeRadioButton);
            var expenseRadioButton = FindViewById<RadioButton>(Resource.Id.expenseRadioButton);
            

            if (incomeRadioButton.Checked == true)
                return "I";
            else
                return "E";
        }
        private  void refreshSpinner(SQLiteConnection db, string ieResult, Spinner spinner)
        {

            

            /* ------ Spinner initialization ------ */

            // Initialize the adapter for the spinner with stock symbols

            var categoryList = (from s in db.Table<Category>()
                                where (s.I_E == ieResult)
                                select s).ToList();
            // var categoryList = categoryList1.Select(s => s.CategoryName).ToList();
            int count = categoryList.Count;
            string[] categoryArray = new string[count];
            for (int i = 0; i < count; i++)
            {
                categoryArray[i] = categoryList[i].CategoryName;
            }
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, categoryArray);

            
            spinner.Adapter = adapter;
        }
    }
}