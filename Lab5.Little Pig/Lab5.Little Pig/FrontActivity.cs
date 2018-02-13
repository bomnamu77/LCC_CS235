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
                //game.Player1Name = editTextPlayer1.Text;
                //game.Player2Name = editTextPlayer2.Text;

                if (!isDualPane)
                {

                    var intent = new Intent();
                    intent.SetClass(this, typeof(BackActivity));
                    intent.PutExtra("player1name", editTextPlayer1.Text);
                    intent.PutExtra("player2name", editTextPlayer2.Text);

                    StartActivity(intent);
                }


            };
            
        }
    }
}

