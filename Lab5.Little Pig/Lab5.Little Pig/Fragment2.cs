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
            var textViewPlayer1 = view.FindViewById<TextView>(Resource.Id.textPlayer1Actual);
            var textViewPlayer2 = view.FindViewById<TextView>(Resource.Id.textPlayer2Actual);
            var textViewPlayersTurn = view.FindViewById<TextView>(Resource.Id.textViewPlayersTurn);

            if (savedInstanceState == null)
             {
                game = new PigLogic();
                SetPlayersName("player1", "player2");
            }
            //SetPlayersName();
            /*if (Arguments != null)
            {
                game.Player1Name = Arguments.GetString("player1name");
                game.Player2Name = Arguments.GetString("player2name");
            }*/


            textViewPlayersTurn.Text = game.Player1Name + "'s Turn";
            textViewPlayer1.Text = game.Player1Name;
            textViewPlayer2.Text = game.Player2Name;

            var buttonRollDie = view.FindViewById<Button>(Resource.Id.buttonRollDie);
            var textViewPoint4ThisTurn = view.FindViewById<TextView>(Resource.Id.textViewPoint4ThisTurn);
            var imageViewDie = view.FindViewById<ImageView>(Resource.Id.imageViewDie);
            var textViewPlayer1Score = view.FindViewById<TextView>(Resource.Id.textViewPlayer1Score);
            var textViewPlayer2Score = view.FindViewById<TextView>(Resource.Id.textViewPlayer2Score);
            DisplayScores(textViewPlayer1Score, textViewPlayer2Score, textViewPlayersTurn);
            DisplayTurnScore(roll, textViewPoint4ThisTurn, imageViewDie);


            buttonRollDie.Click += delegate
            {

                roll = game.RollDie();
                DisplayTurnScore(roll, textViewPoint4ThisTurn, imageViewDie);
                //when the roll is a bad number, it changes turn with giving 0 score to current player.
                if (roll == PigLogic.BAD_NUMBER)
                {
                    game.ChangeTurn();
                    DisplayScores(textViewPlayer1Score, textViewPlayer2Score, textViewPlayersTurn);
                    Android.Widget.Toast.MakeText(Activity, game.GetCurrentPlayer() + " rolled 0! Turn changed", Android.Widget.ToastLength.Short).Show();
                    
                    // Check whether points are same or over the point's limit
                    // only after both player's playing same amount of rolling
                    if (game.Turn == 1)
                    {
                        if (game.CheckForWinner() != "")
                        {

                            CheckWinner();
                            ResetGame();
                        }

                    }

                }
            };


            var buttonEndTurn = view.FindViewById<Button>(Resource.Id.buttonEndTurn);

            buttonEndTurn.Click += delegate
            {
                game.ChangeTurn();

                DisplayScores(textViewPlayer1Score, textViewPlayer2Score, textViewPlayersTurn);
                DisplayTurnScore(roll, textViewPoint4ThisTurn, imageViewDie);


                // Check whether points are same or over the point's limit
                // only after both player's playing same amount of rolling
                if (game.Turn == 1)
                {
                    if (game.CheckForWinner() != "")
                    {
                        CheckWinner();
                        ResetGame();
                    }

                }


            };

            var buttonNewGame = view.FindViewById<Button>(Resource.Id.buttonNewGame);
            buttonNewGame.Click += delegate
            {
                ResetGame();
            };
            return view;
            //return base.OnCreateView(inflater, container, savedInstanceState);
        }

        // Function DisplayScores
        // display updated scores and who's turn
        void DisplayScores(TextView textViewPlayer1Score, TextView textViewPlayer2Score, TextView textViewPlayersTurn)
        {
            /*
            TextView textViewPlayer1Score = Activity.FindViewById<TextView>(Resource.Id.textViewPlayer1Score);
            TextView textViewPlayer2Score = Activity.FindViewById<TextView>(Resource.Id.textViewPlayer2Score);
            TextView textViewPlayersTurn = Activity.FindViewById<TextView>(Resource.Id.textViewPlayersTurn);


            /*textViewPlayer1Score.Text = string.Format("{0}", game.Player1Score);
            textViewPlayer2Score.Text = string.Format("{0}", game.Player2Score);

            textViewPlayersTurn.Text = game.GetCurrentPlayer() + "'s Turn";
            */
            if (game.Turn == 2)
            {

                textViewPlayer1Score.Text = string.Format("{0}", game.Player1Score);

                textViewPlayersTurn.Text = game.GetCurrentPlayer() + "'s Turn";
            }
            else
            {

                textViewPlayer2Score.Text = string.Format("{0}", game.Player2Score);

                textViewPlayersTurn.Text = game.Player1Name + "'s Turn";
            }

        }


        // Function DisplayTurnScore
        // display dice image and turn points
        void DisplayTurnScore(int roll, TextView textViewPoint4ThisTurn, ImageView imageViewDie)
        {
            switch (roll)
            {
                case 2:
                    imageViewDie.SetImageResource(Resource.Drawable.Dice2);
                    break;
                case 3:
                    imageViewDie.SetImageResource(Resource.Drawable.Dice3);
                    break;
                case 4:
                    imageViewDie.SetImageResource(Resource.Drawable.Dice4);
                    break;
                case 5:
                    imageViewDie.SetImageResource(Resource.Drawable.Dice5);
                    break;
                case 6:

                    imageViewDie.SetImageResource(Resource.Drawable.Dice6);
                    break;
                default:
                    imageViewDie.SetImageResource(Resource.Drawable.Dice1);
                    break;
            };
            textViewPoint4ThisTurn.Text = string.Format("{0}", game.TurnPoints);

        }

        // function CheckWineer
        // check for winner and display the result
        void CheckWinner()
        {
            TextView textViewPlayersTurn = Activity.FindViewById<TextView>(Resource.Id.textViewPlayersTurn);

            if (game.CheckForWinner() == "Tie")
            {
                textViewPlayersTurn.Text = "It's tie!";
            }
            else if (game.CheckForWinner() == game.Player1Name)
            {
                textViewPlayersTurn.Text = game.Player1Name + " wins!";
            }
            else if (game.CheckForWinner() == game.Player2Name)
            {
                textViewPlayersTurn.Text = game.Player2Name + " wins!";

            }
        }

        // function ResetGame
        // reset the game and score display
        public void ResetGame()
        {

            game.ResetGame();

            TextView textViewPlayer1Score = Activity.FindViewById<TextView>(Resource.Id.textViewPlayer1Score);
            TextView textViewPlayer2Score = Activity.FindViewById<TextView>(Resource.Id.textViewPlayer2Score);
            
            textViewPlayer1Score.Text = textViewPlayer2Score.Text = "0";

        }

        // function SetPlayersName
        // set players name 
        public void SetPlayersName(string player1Name, string player2Name)
        {
            game.Player1Name = player1Name;
            game.Player2Name = player2Name;


        }
    }
}
