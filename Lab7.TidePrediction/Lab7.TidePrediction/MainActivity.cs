using Android.App;
using Android.Widget;
using Android.OS;

namespace Lab7.TidePrediction
{
    [Activity(Label = "Lab7.TidePrediction", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

