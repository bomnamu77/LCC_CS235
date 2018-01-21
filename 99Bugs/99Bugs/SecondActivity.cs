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

namespace _99Bugs
{
    [Activity(Label = "The code", LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class SecondActivity : Activity
    {
        static int nBugs = 99;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Second);
            // Create your application here

            // Substract bugs taken down (sent from the main) and Display the number of bugs remaining
            int answer = Intent.Extras.GetInt(MainActivity.TAKE_DOWN);
            nBugs -= answer;
            var bugsTextView = FindViewById<TextView>(Resource.Id.textBugsRemaining);
            bugsTextView.Text = string.Format("Bugs remaining: {0} ", nBugs);

            // Send the number of bugs remaining
            Button patchButton = FindViewById<Button>(Resource.Id.btnPatchIt);
            patchButton.Click += delegate {
                var toFront = new Intent(this, typeof(MainActivity));
                // Note: both buttons use the same key in the intent because they
                // are just setting two different values for the same data item.
                toFront.PutExtra(MainActivity.BUGS_REMAINING, nBugs);
                SetResult(Result.Ok, toFront);
                Finish();
            };

            
        }
    }
}