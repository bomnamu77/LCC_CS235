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

            int itemCount = dataList.Count;

            tideItem = new TideItem[itemCount];
            
            int i = 0;
            //JavaDictionary<string, object> prediction = null;
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
            
            ListAdapter = new ArrayAdapter<TideItem>(this,
                 Android.Resource.Layout.TwoLineListItem,
                 tideItem);
            /*
            // constructor takes: reference to this Activity, List of Dictionary objects, row layout, 
            ListAdapter = new VocabAdapter(this, dataList,
                Android.Resource.Layout.SimpleListItem1,
                new string[] { XmlVocabFileParser.SPANISH, XmlVocabFileParser.ENGLISH },
                new int[] { Android.Resource.Id.Text1 }
            );*/
            
            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);


        }

        // Row click event handler in ListActivity
        protected override void OnListItemClick(ListView l,
             View v, int position, long id)
        {
            string word = tideItem[position].Height;
            Android.Widget.Toast.MakeText(this, word,
                Android.Widget.ToastLength.Short).Show();
        }
    }
}

