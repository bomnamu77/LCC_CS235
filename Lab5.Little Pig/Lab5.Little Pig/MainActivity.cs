﻿using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab5.Little_Pig
{
    [Activity(Label = "Lab5.Little_Pig", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape )]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

