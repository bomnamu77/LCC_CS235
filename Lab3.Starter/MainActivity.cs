using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Xml.Serialization;

namespace Lab3
{
    [Activity(Label = "Lab3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        QuoteBank quoteCollection;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            if (savedInstanceState == null)     //if null, initialization
            {
                // Create the quote collection and load quotes
                quoteCollection = new QuoteBank();
                quoteCollection.LoadQuotes();
                
            }
            else       //not null, getting infromation from Bundle
            {
  
                // Deserialized the saved object state
                string xmlQuoteCollection = savedInstanceState.GetString("Quotes");
                XmlSerializer x = new XmlSerializer(typeof(QuoteBank));
                quoteCollection = (QuoteBank)x.Deserialize(new StringReader(xmlQuoteCollection));

            }

            TextView quotationTextView = FindViewById<TextView>(Resource.Id.quoteTextView);
 
            quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
            DisplayScores();

            var whoEditText = FindViewById<EditText>(Resource.Id.whoEditText);
            var answerTextView = FindViewById<TextView>(Resource.Id.answerTextView);

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

                if (!quoteCollection.CheckAnswer(whoEditText.Text))       // The answer is incorrect
                {
                    string person = quoteCollection.CurrentQuote.Person;
                    answerTextView.Text = "Wrong! The answer is " + person;

                    DisplayScores();
                    quoteCollection.GetNextQuote();
                    quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
                    whoEditText.Text = "";
                }
                else
                {
                    answerTextView.Text = "Correct Answer!";                    // The answer is correct

                    DisplayScores();
                    quoteCollection.GetNextQuote();
                    quotationTextView.Text = quoteCollection.CurrentQuote.Quotation;
                    whoEditText.Text = "";
                }

            };

            // Reset button
            // Set Scores as 0
            var resetButton = FindViewById<Button>(Resource.Id.resetButton);
            resetButton.Click += delegate
            {
                quoteCollection.ResetScores();
                DisplayScores();
                
            };
        }


        protected override void OnSaveInstanceState(Bundle outState)
        {


            // Use this to convert a stream to a string
            StringWriter writer = new StringWriter();

            // Serialize the public state of quoteCollecion to XML
            XmlSerializer quoteBankSerializer = new XmlSerializer(typeof(QuoteBank));
            quoteBankSerializer.Serialize(writer, quoteCollection);

            // Save the serialized state3
            string xmlQuoteCollection = writer.ToString();
            outState.PutString("Quotes", xmlQuoteCollection);

            base.OnSaveInstanceState(outState);
        }

        //Display scores 
        private void DisplayScores()
        {
            var scoreTextView = FindViewById<TextView>(Resource.Id.scoreTextView);
            int nRight = quoteCollection.right;
            int nWrong = quoteCollection.wrong;
            scoreTextView.Text = string.Format("Score: Right({0}), Wrong ({1})", nRight, nWrong);
            


        }
    }
}

