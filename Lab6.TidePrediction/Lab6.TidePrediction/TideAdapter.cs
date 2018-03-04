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

namespace Lab6.TidePrediction
{
    class TideAdapter : ArrayAdapter<TideItem>, ISectionIndexer
    {

        Activity context;

        TideItem[] items;
        Int32 resource;


        public TideAdapter(Activity c, Int32 r,  TideItem[] i) 
            : base(c, r, i)
        {
            context = c;
            resource = r;
            items = i;
            // Sort list by Part Of Speech (pos) field
            //dataList.Sort((x, y) => String.Compare((string)x[XmlTideFileParser.POS], (string)y[XmlVocabFileParser.POS]));
            BuildSectionIndex();
        }

        // ---- Implementation of ISectionIndexer  -------

        String[] sections;
        Java.Lang.Object[] sectionsObjects;
        Dictionary<string, int> alphaIndex;

        public int GetPositionForSection(int section)
        {
            return alphaIndex[sections[section]];
        }

        public int GetSectionForPosition(int position)
        {
            return 1;
        }

        public Java.Lang.Object[] GetSections()
        {
            return sectionsObjects;
        }

        private void BuildSectionIndex()
        {
            alphaIndex = new Dictionary<string, int>();      // Dictionaray will contain section names
            for (var i = 0; i < Count; i++)
            {
                // Use the pos field as a key
                var key = items[i].Month;
                if (!alphaIndex.ContainsKey(key))
                {
                    alphaIndex.Add(key, i);
                }
            }

            // Get the count of sections
            sections = new string[alphaIndex.Keys.Count];
            // Copy section names into the sections array
            alphaIndex.Keys.CopyTo(sections, 0);

            // Copy section names into a Java object array
            sectionsObjects = new Java.Lang.Object[sections.Length];
            for (var i = 0; i < sections.Length; i++)
            {
                sectionsObjects[i] = new Java.Lang.String(sections[i]);
            }
        }

       

       public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(
                    resource,
                    null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text
                = items[position].Day + " " + items[position].Date;
            string temp;

            if (items[position].Hi_Low == "H")
                temp = "High";
            else
                temp = "Low";
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text
                = items[position].Time + " - " + temp;
            return view;


            
        }
        

    }

}