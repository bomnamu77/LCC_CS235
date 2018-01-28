using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab3
{
    [Activity(Label = "Lab3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        QuoteBank quoteCollection;
        TextView quotationTextView;
        int nRight = 0;
        int nWrong = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            quoteCollection = new QuoteBank();
            quoteCollection.LoadQuotes();

            quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);
            var whoEditText = FindViewById<EditText>(Resource.Id.whoEditText);
            var answerTextView = FindViewById<TextView>(Resource.Id.answerTextView);
            var scoreTextView = FindViewById<TextView>(Resource.Id.scoreTextView);

            if (savedInstanceState == null)
            {
                 // Create the quote collection and display the current quote
                quoteCollection.GetNextQuote();
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;

            }
            else
            {
                // Get Quote data and set currentQuote 
                Quote q = new Quote();
                q.Quotation = savedInstanceState.GetString("Quotation");
                q.Person = savedInstanceState.GetString("Person");

                quoteCollection.CurrentQuote = q;
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;

                //Get scores data and set scores
                nRight = savedInstanceState.GetInt("Score_right");
                nWrong = savedInstanceState.GetInt("Score_wrong");
                scoreTextView.Text = string.Format("Score: Right({0}), Wrong ({1})", nRight, nWrong);

            }
            // Display another quote when the "Next Quote" button is tapped
            var nextButton = FindViewById<Button>(Resource.Id.nextButton);
            nextButton.Click += delegate {
                quoteCollection.GetNextQuote();
                quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
            };

            // Enter button
            // Check the user's input with the answer and count scores
             var enterButton = FindViewById<Button>(Resource.Id.enterButton);
            enterButton.Click += delegate {

                if (whoEditText.Text != quoteCollection.CurrentQuote.Person)    // The answer is incorrect
                {
                    string person = quoteCollection.CurrentQuote.Person;
                    answerTextView.Text = "Wrong! The answer is " + person;
                    nWrong++;
                    scoreTextView.Text = string.Format("Score: Right({0}), Wrong ({1})", nRight, nWrong);
                    quoteCollection.GetNextQuote();
                    quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;

                }
                else
                {
                    answerTextView.Text = "Correct Answer!";                    // The answer is correct
                    nRight++;
                    scoreTextView.Text = string.Format("Score: Right({0}), Wrong ({1})", nRight, nWrong);
                    quoteCollection.GetNextQuote();
                    quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;

                }

            };

            // Reset button
            // Set Scores as 0
            var resetButton = FindViewById<Button>(Resource.Id.resetButton);
            resetButton.Click += delegate
            {
                nRight = nWrong = 0;
                scoreTextView.Text = string.Format("Score: Right({0}), Wrong ({1})", nRight, nWrong);
            };
        }


        protected override void OnSaveInstanceState(Bundle outState)
        {

            // Store current Quote information
            outState.PutString("Quotation", quoteCollection.CurrentQuote.Quotation);
            outState.PutString("Person", quoteCollection.CurrentQuote.Person);

            // Store current score information
            outState.PutInt("Score_right", nRight);
            outState.PutInt("Score_wrong", nWrong);


            base.OnSaveInstanceState(outState);
        }
    }
}

