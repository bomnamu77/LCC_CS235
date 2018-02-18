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

            SetContentView(Resource.Layout.BackActivity);
            //Get player names from front activity
            var player1Name = Intent.Extras.GetString("player1name");
            var player2Name = Intent.Extras.GetString("player2name");

            var textPlayer1Actual = FindViewById<TextView>(Resource.Id.textPlayer1Actual);
            var textPlayer2Actual = FindViewById<TextView>(Resource.Id.textPlayer2Actual);
            var textPlayerTurn = FindViewById<TextView>(Resource.Id.textViewPlayersTurn);

            //Set 
            textPlayer1Actual.Text = player1Name;
            textPlayer2Actual.Text = player2Name;
            // this shall be starting of the game, turn must be player1
            textPlayerTurn.Text = player1Name + "'s Turn";

            // Find Fragment2 and execute SetPlayersName method and ResetGame
            var frag2 = FragmentManager.FindFragmentById(Resource.Id.Fragment2) as Fragment2;
            frag2.SetPlayersName(player1Name, player2Name);
            frag2.ResetGame();
            

        }
    }
}