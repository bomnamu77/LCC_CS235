using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace _99Bugs
{
    [Activity(Label = "99Bugs", MainLauncher = true)]
    public class MainActivity : Activity
    {
        public const string TAKE_DOWN = "TakeDown";
        public const string BUGS_REMAINING = "Bugs";
        const int RESULT_REQUEST = 0;  // sub-activity result request code

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Send numers of bug taken down and Show the SecondActivity 
            // first button (Take one down)
            Button button1 = FindViewById<Button>(Resource.Id.btnTake1down);
            
            button1.Click += delegate {
                var second = new Intent(this, typeof(SecondActivity));
                // Note: Intent is both a class and a property name, be sure you have a using statement

                second.PutExtra(TAKE_DOWN, 1);
                StartActivityForResult(second, 0);
            };

            // second button (Take two down)
            Button button2 = FindViewById<Button>(Resource.Id.btnTake2down);

            button2.Click += delegate {
                var second = new Intent(this, typeof(SecondActivity));
                // Note: Intent is both a class and a property name, be sure you have a using statement

                second.PutExtra(TAKE_DOWN, 2);
                StartActivityForResult(second, 0);
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == RESULT_REQUEST)
            {
                if (resultCode == Result.Ok)
                {
                    // Get data(number of remaining bugs) from SecondActivity and Display it 
                    int bugs = data.GetIntExtra(BUGS_REMAINING, 0);
                    
                    var bugsTextView = FindViewById<TextView>(Resource.Id.textBugs);
                    bugsTextView.Text = string.Format("{0} little bugs in the code", bugs);
                }
            }
        }
    }
}

