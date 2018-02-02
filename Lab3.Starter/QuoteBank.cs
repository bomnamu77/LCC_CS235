using System;
using System.Collections.Generic;

namespace Lab3
{
	public class Quote 
	{
		public string Quotation { get; set; }
		public string Person { get; set; }
	}

	public class QuoteBank
	{
		private Random randNumGen = new Random();
		private List<Quote> quotes = new List<Quote>();
        public int currentQuote = 0;

		public List<Quote> Quotes { get { return quotes; } }
		public Quote CurrentQuote { get; set; }


        public int right = 0, wrong = 0;
        
        /*
		public Quote GetRandomQuote() 
		{
			if (quotes.Count > 0) 
			{
				CurrentQuote = quotes [randNumGen.Next (0, quotes.Count)];
			}
				
			return CurrentQuote;
		}
        */

        public Quote GetNextQuote()
        {
            if (quotes.Count > 0)
            {
                if (++currentQuote >= quotes.Count)
                    currentQuote = 0;

                CurrentQuote = quotes[currentQuote];
                
            }
            return CurrentQuote;
        }
        

        public bool CheckAnswer(string answer)
        {
            if (answer == CurrentQuote.Person)
            {
                right++;

                return true;
            }
            else
            {
                wrong++;
                return false;
            }
            

        }



        public void ResetScores()
        {
            right = 0;
            wrong = 0;
        }

        public void LoadQuotes()
		{
			quotes.Add(new Quote { Quotation = "If men were angels, no government would be necessary.", Person = "James Madison"});
			quotes.Add(new Quote { Quotation = "You can't trust water, even a straight stick turns crooked in it.", Person = "W. C. Fields"});
			quotes.Add(new Quote { Quotation = "The best and most beautiful things in the world cannot be seen or even touched - they must be felt with the heart.", Person = "Helen Keller"});
			quotes.Add(new Quote { Quotation = "If you get, give. If you learn, teach.", Person = "Maya Angelou"});
			CurrentQuote = quotes [0];
		}

	}
}

