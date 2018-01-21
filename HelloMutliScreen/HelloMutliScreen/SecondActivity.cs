using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;


namespace HelloMutliScreen
{
    [Activity(Label = "SecondActivity")]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Second);
            var label = FindViewById<TextView>(Resource.Id.screen2Label);
            label.Text = Intent.GetStringExtra("FirstData") ?? "Data not available";
        }
    }
}