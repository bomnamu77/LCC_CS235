using Android.App;
using Android.Widget;
using Android.OS;
using PigGame;

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



            /*
            editTextPlayer1.AfterTextChanged += delegate {
                if (game.Turn == 1)
                    textViewPlayersTurn.Text = editTextPlayer1 + "'s Turn";
                
            };
            editTextPlayer2.AfterTextChanged += delegate {
                if (game.Turn == 2)
                    textViewPlayersTurn.Text = editTextPlayer2 + "'s Turn";

            };*/
            int roll = 0;
            var buttonRollDie = FindViewById<Button>(Resource.Id.buttonRollDie);
            var textViewPoint4ThisTurn = FindViewById<TextView>(Resource.Id.textViewPoint4ThisTurn);
            var imageViewDie = FindViewById<ImageView>(Resource.Id.imageViewDie);
            var textViewPlayer1Score = FindViewById<TextView>(Resource.Id.textViewPlayer1Score);
            var textViewPlayer2Score = FindViewById<TextView>(Resource.Id.textViewPlayer2Score);

            buttonRollDie.Click += delegate
            {
                roll = game.RollDie();
                if (roll == 1)
                    imageViewDie.SetImageResource(Resource.Drawable.Dice1);
                else if (roll == 2)
                    imageViewDie.SetImageResource(Resource.Drawable.Dice2);
                else if (roll == 3)
                    imageViewDie.SetImageResource(Resource.Drawable.Dice3);
                else if (roll == 4)
                    imageViewDie.SetImageResource(Resource.Drawable.Dice4);
                else if (roll == 5)
                    imageViewDie.SetImageResource(Resource.Drawable.Dice5);
                else
                    imageViewDie.SetImageResource(Resource.Drawable.Dice6);
                textViewPoint4ThisTurn.Text = string.Format("{0}", game.TurnPoints);
                if (roll == PigLogic.BAD_NUMBER)
                {
                    game.ChangeTurn();
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
            };

            
            var buttonEndTurn = FindViewById<Button>(Resource.Id.buttonEndTurn);

            buttonEndTurn.Click += delegate
            {
                game.ChangeTurn();

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


            };

            var buttonNewGame = FindViewById<Button>(Resource.Id.buttonNewGame);
            buttonNewGame.Click += delegate
            {
                game.ResetGame();
                textViewPlayer1Score.Text = textViewPlayer2Score.Text = "0";
            };
        }
    }
}

