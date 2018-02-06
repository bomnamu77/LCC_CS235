﻿using Android.App;
using Android.Widget;
using Android.OS;
using PigGame;
using System.IO;
using System.Xml.Serialization;

namespace Lab4.Little_Pig
{
    

    [Activity(Label = "Lab4.Little_Pig", MainLauncher = true)]
    public class MainActivity : Activity
    {

        PigLogic game;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            int roll = 0;
            var editTextPlayer1 = FindViewById<EditText>(Resource.Id.editTextPlayer1);
            var editTextPlayer2 = FindViewById<TextView>(Resource.Id.editTextPlayer2);
            var textViewPlayersTurn = FindViewById<TextView>(Resource.Id.textViewPlayersTurn);
            if (savedInstanceState == null)     //if null, initialization
            {
                // Create the quote collection and load quotes
                game = new PigLogic();
                game.Player1Name = editTextPlayer1.Text;
                game.Player2Name = editTextPlayer2.Text;
                textViewPlayersTurn.Text = game.Player1Name + "'s Turn";
            }
            else
            {

                // Deserialized the saved object state
                string xmlGame = savedInstanceState.GetString("PigGame");
                XmlSerializer x = new XmlSerializer(typeof(PigLogic));
                game = (PigLogic)x.Deserialize(new StringReader(xmlGame));

            }



            /*
            editTextPlayer1.AfterTextChanged += delegate {
                if (game.Turn == 1)
                    textViewPlayersTurn.Text = editTextPlayer1 + "'s Turn";
                
            };
            editTextPlayer2.AfterTextChanged += delegate {
                if (game.Turn == 2)
                    textViewPlayersTurn.Text = editTextPlayer2 + "'s Turn";

            };*/
            
            var buttonRollDie = FindViewById<Button>(Resource.Id.buttonRollDie);
            var textViewPoint4ThisTurn = FindViewById<TextView>(Resource.Id.textViewPoint4ThisTurn);
            var imageViewDie = FindViewById<ImageView>(Resource.Id.imageViewDie);
            var textViewPlayer1Score = FindViewById<TextView>(Resource.Id.textViewPlayer1Score);
            var textViewPlayer2Score = FindViewById<TextView>(Resource.Id.textViewPlayer2Score);
            displayScores(textViewPlayer1Score, textViewPlayer2Score, textViewPlayersTurn);
            displayTurnScore(roll, textViewPoint4ThisTurn, imageViewDie);
            

            buttonRollDie.Click += delegate
            {
                
                roll = game.RollDie();
                displayTurnScore(roll, textViewPoint4ThisTurn, imageViewDie);
                if (roll == PigLogic.BAD_NUMBER)
                {
                    game.ChangeTurn();
                    displayScores(textViewPlayer1Score, textViewPlayer2Score, textViewPlayersTurn);
                    


                }
            };

            
            var buttonEndTurn = FindViewById<Button>(Resource.Id.buttonEndTurn);

            buttonEndTurn.Click += delegate
            {
                game.ChangeTurn();

                displayScores(textViewPlayer1Score, textViewPlayer2Score, textViewPlayersTurn);
                displayTurnScore(roll, textViewPoint4ThisTurn, imageViewDie);

                // Check whether points are same or over the point's limit
                // only after both player's playing
                if (game.Turn == 1)
                {
                    

                    if (game.CheckForWinner() == "Tie")
                    {
                        textViewPlayersTurn.Text = "It's tie!";
                        game.ResetGame();
                        textViewPlayer1Score.Text = textViewPlayer2Score.Text = "0";
                    }
                    else if (game.CheckForWinner() == game.Player1Name)
                    {
                        textViewPlayersTurn.Text = game.Player1Name + " wins!";
                        game.ResetGame();
                        textViewPlayer1Score.Text = textViewPlayer2Score.Text = "0";
                    }
                    else if (game.CheckForWinner() == game.Player2Name)
                    {
                        textViewPlayersTurn.Text = game.Player2Name + " wins!";
                        game.ResetGame();
                        textViewPlayer1Score.Text = textViewPlayer2Score.Text = "0";

                    }
                }


            };

            var buttonNewGame = FindViewById<Button>(Resource.Id.buttonNewGame);
            buttonNewGame.Click += delegate
            {
                game.ResetGame();
                textViewPlayer1Score.Text = textViewPlayer2Score.Text = "0";
            };


        }

        
        protected override void OnSaveInstanceState(Bundle outState)
        {


            // Use this to convert a stream to a string
            StringWriter writer = new StringWriter();

            // Serialize the public state of quoteCollecion to XML
            XmlSerializer gameSerializer = new XmlSerializer(typeof(PigLogic));
            gameSerializer.Serialize(writer, game);

            // Save the serialized state3
            string xmlGame = writer.ToString();
            outState.PutString("PigGame", xmlGame);

            base.OnSaveInstanceState(outState);
        }

        void displayScores(TextView textViewPlayer1Score, TextView textViewPlayer2Score, TextView textViewPlayersTurn)
        {
            if (game.Turn == 2)
            {

                textViewPlayer1Score.Text = string.Format("{0}", game.Player1Score);

                textViewPlayersTurn.Text = game.Player2Name + "'s Turn";
            }
            else
            {

                textViewPlayer2Score.Text = string.Format("{0}", game.Player2Score);

                textViewPlayersTurn.Text = game.Player1Name + "'s Turn";
            }

        }

        void displayTurnScore(int roll, TextView textViewPoint4ThisTurn, ImageView imageViewDie)
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
    }
}

