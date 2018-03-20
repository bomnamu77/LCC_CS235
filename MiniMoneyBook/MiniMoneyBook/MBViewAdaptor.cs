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

    class MBViewAdaptor : BaseExpandableListAdapter
    {

        // Context, usually set to the activity:
        private readonly Context _context;

        // List of mbView objects (Year-Month):
        private readonly List<MBView> _mbView;

        public MBViewAdaptor(Context context, List<MBView> mbView)
        {
            _context = context;
            _mbView = mbView;
        }


        public override bool HasStableIds
        {
            // Indexes are used for IDs:
            get { return true; }
        }

        //---------------------------------------------------------------------------------------
        // Group methods:

        public override long GetGroupId(int groupPosition)
        {
            // The index of the group is used as its ID:
            return groupPosition;
        }

        public override int GroupCount
        {
            // Return the number of mbView objects:
            get { return _mbView.Count; }
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            // Recycle a previous view if provided:
            var view = convertView;

            // If no recycled view, inflate a new view as a simple expandable list item 1:
            if (view == null)
            {
                var inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                view = inflater.Inflate(Android.Resource.Layout.SimpleExpandableListItem1, null);
            }

            // Grab the mbView object (Year-Month) at the group position:
            var mbView = _mbView[groupPosition];

            // Get the built-in first text view and insert the group name (Month-Year - Balance):
            TextView textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            textView.Text = mbView.Month+ "/" + mbView.Year + "\t\t\t" + mbView.Balance.ToString();
            
            return view;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return null;
        }

        //---------------------------------------------------------------------------------------
        // Child methods:

        public override long GetChildId(int groupPosition, int childPosition)
        {
            // The index of the child is used as its ID:
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            // Return the number of children (mbView item objects) in the group MBViewItems object):
            var mbView = _mbView[groupPosition];
            return mbView.MBViewItems.Length;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            // Recycle a previous view if provided:
            var view = convertView;

            // If no recycled view, inflate a new view as a simple expandable list item 2:
            if (view == null)
            {
                var inflater = _context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                view = inflater.Inflate(Android.Resource.Layout.SimpleExpandableListItem2, null);
            }

            // Grab the mbView object (Year-Month) at the group position:
            var mbView = _mbView[groupPosition];

            // Extract the MBViewitems object (records of each month) at the child position:
            var mbViewItem = mbView.MBViewItems[childPosition];

            // Get the built-in first text view and insert the child name :
            TextView textView = view.FindViewById<TextView>(Android.Resource.Id.Text1);
            if (mbViewItem.I_E == "I")      //If it's "income", blue col
                textView.SetTextColor(Android.Graphics.Color.Blue);
            else //if it's "expense", red color
                textView.SetTextColor(Android.Graphics.Color.Red);
            textView.Text = mbViewItem.Date+" " + mbViewItem.I_E + " " + mbViewItem.Category + " " + mbViewItem.Amount;

            return view;
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            return null;
        }

        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }

    }

    class MBViewAdaptorViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}