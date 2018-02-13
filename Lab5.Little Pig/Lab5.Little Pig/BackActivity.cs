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

namespace Lab5.Little_Pig
{
    [Activity(Label = "BackActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class BackActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            //SetContentView(Resource.Layout.BackActivity);
            var player1name = Intent.Extras.GetString("player1name");
            var player2name = Intent.Extras.GetString("player2name");


            var frag2 = Fragment2.NewInstance(player1name, player2name); // Details
            var fragmentTransaction = FragmentManager.BeginTransaction();
            fragmentTransaction.Add(Android.Resource.Id.Content, frag2);
            fragmentTransaction.Commit();


        }
    }
}