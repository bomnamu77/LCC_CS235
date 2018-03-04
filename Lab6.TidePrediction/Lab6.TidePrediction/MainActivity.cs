using Android.App;
using Android.Widget;
using Android.Views;
using Android.OS;
using System.Collections.Generic;
using Android.Runtime;

namespace Lab6.TidePrediction
{
    [Activity(Label = "Lab6.TidePrediction", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
        TideItem[] tideItem;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Read and prase the vocabulary file and provide the adapter with the data
            var reader = new XmlTideFileParser(Assets.Open(@"TideData.xml"));
            var dataList = reader.TideList;

            //get size of list
            int itemCount = dataList.Count;

            //create arrays
            tideItem = new TideItem[itemCount];
            
            int i = 0;
            
            //convert list of dictionary data to array
            while ( i < itemCount )
            {
                object date, day, time, height, hi_low;
                //prediction = new JavaDictionary<string, object>();
                //dataList.IndexOf(prediction, i);
                IDictionary<string, object> prediction = dataList[i];
                prediction.TryGetValue(XmlTideFileParser.DATE, out date);
                prediction.TryGetValue(XmlTideFileParser.DAY, out day);
                prediction.TryGetValue(XmlTideFileParser.TIME, out time);
                prediction.TryGetValue(XmlTideFileParser.HEIGHT, out height);
                prediction.TryGetValue(XmlTideFileParser.HI_LOW, out hi_low);

                string sdate = date.ToString();
                string sday = day.ToString();
                string stime = time.ToString();
                string sheight = height.ToString();
                string shi_low = hi_low.ToString();
                 tideItem[i] = new TideItem(sdate, sday, stime, sheight, shi_low);
                i++;

            }

            //create list view with ArrayAdapter with SectionIndex
            ListAdapter = new TideAdapter(this, Android.Resource.Layout.TwoLineListItem, tideItem);
            // This is all you need to do to enable fast scrolling
            ListView.FastScrollEnabled = true;

            


        }

        // Row click event handler in ListActivity
        protected override void OnListItemClick(ListView l,
             View v, int position, long id)
        {
            //shows tide height using toast message 
            string word = tideItem[position].Height;
            Android.Widget.Toast.MakeText(this, word,
                Android.Widget.ToastLength.Short).Show();
        }
    }
}

