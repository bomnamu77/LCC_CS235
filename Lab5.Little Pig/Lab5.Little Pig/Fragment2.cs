using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PigGame;

namespace Lab5.Little_Pig
{
    public class Fragment2 : Fragment
    {
        PigLogic game;


        public static Fragment2 NewInstance(string player1name, string player2name)
        {
            var Frag1 = new Fragment2 { Arguments = new Bundle() };
            Frag1.Arguments.PutString("player1name", player1name);
            Frag1.Arguments.PutString("player2name", player2name);
            return Frag1;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here


            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);


            View view = inflater.Inflate(Resource.Layout.Fragment2, container, false);

            int roll = 0;
            if (savedInstanceState == null)
            

            {
                game = new PigLogic();

                game.Player1Name = Arguments.GetString("player1name");
                game.Player2Name = Arguments.GetString("player2name");

                var editTextPlayer1 = view.FindViewById<EditText>(Resource.Id.editTextPlayer1);
                var editTextPlayer2 = view.FindViewById<TextView>(Resource.Id.editTextPlayer2);

                var textViewPlayersTurn = view.FindViewById<TextView>(Resource.Id.textViewPlayersTurn);

                textViewPlayersTurn.Text = game.Player1Name;
             } 
            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}