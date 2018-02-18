using Android.App;
using Android.Widget;
using Android.OS;
using PigGame;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;

namespace Lab5.Little_Pig
{
    [Activity(Label = "Lab5.Little_Pig", MainLauncher = true )]
    

    

    public class FrontActivity : Activity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            // Only use landscape orientation for large (tablet) screens
            // This is becuase my one layout for large sceens looks better in landscape
            if ((Application.ApplicationContext.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask) == ScreenLayout.SizeLarge)
            {
                RequestedOrientation = ScreenOrientation.Landscape;
            }

            if ((Application.ApplicationContext.Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask) == ScreenLayout.SizeSmall)
            {
                RequestedOrientation = ScreenOrientation.Portrait;
            }

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.FrontActivity);

            // Only the single pane layout (portrait) has a translate button
            /*
            if (savedInstanceState == null)     //if null, initialization
            {
                // Create the quote collection and load quotes
                game = new PigLogic();
                //game.Player1Name = editTextPlayer1.Text;
                //game.Player2Name = editTextPlayer2.Text;
                //textViewPlayersTurn.Text = game.Player1Name + "'s Turn";
            }*/
            var buttonNewGame = FindViewById<Button>(Resource.Id.buttonNewGame);

            bool isDualPane = false;
            if (buttonNewGame != null)
                isDualPane = true;

            var editTextPlayer1 = FindViewById<EditText>(Resource.Id.editTextPlayer1);
            var editTextPlayer2 = FindViewById<TextView>(Resource.Id.editTextPlayer2);

            var buttonStartGame = FindViewById<Button>(Resource.Id.buttonStartGame);


            buttonStartGame.Click += delegate
            {
                
                if (!isDualPane)
                {
                    // if it's not dual pane, call backactivity using intent with player names 
                    var intent = new Intent();
                    intent.SetClass(this, typeof(BackActivity));
                    intent.PutExtra("player1name", editTextPlayer1.Text);
                    intent.PutExtra("player2name", editTextPlayer2.Text);

                    StartActivity(intent);
                }
                else
                {
                    // if it's dual pane, set player names in the Fragment2
                    var textPlayer1Actual = FindViewById<TextView>(Resource.Id.textPlayer1Actual);
                    var textPlayer2Actual = FindViewById<TextView>(Resource.Id.textPlayer2Actual);
                    var textPlayerTurn = FindViewById<TextView>(Resource.Id.textViewPlayersTurn);

                    textPlayer1Actual.Text = editTextPlayer1.Text;
                    textPlayer2Actual.Text = editTextPlayer2.Text;
                    // this shall be starting of the game, turn must be player1
                    textPlayerTurn.Text = editTextPlayer1.Text + "'s Turn";

                    // Find Fragment2 and execute SetPlayersName method and ResetGame
                    var frag2 = FragmentManager.FindFragmentById(Resource.Id.Fragment2) as Fragment2;
                    frag2.SetPlayersName(editTextPlayer1.Text, editTextPlayer2.Text);
                    frag2.ResetGame();
                    
                }
               


            };
            
        }
    }
}

