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

namespace MiniMoneyBook
{



    // Represents a month of the year
    public class MBView
    {

        // Year, Month
        public string Year { get; set; }

        public string Month { get; set; }

        // Balance of the month
        public decimal Balance { get; set; }
        public MBViewItem[] MBViewItems{ get; set; }
    }

    // Represents a money record item within a month of the year:
    public class MBViewItem
    {
        public string Date { get; set; }
        public string I_E { get; set; }
        public string Category { get; set; }
        public decimal Amount { get; set; }

            
    }

}