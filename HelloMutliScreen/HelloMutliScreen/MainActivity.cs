using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace HelloMutliScreen
{
    [Activity(Label = "HelloMutliScreen", MainLauncher = true)]
    public class FirstActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var showSecond = FindViewById<Button>(Resource.Id.showSecond);
            showSecond.Click += (sender, e) => {
                var second = new Intent(this, typeof(SecondActivity));
                second.PutExtra("FirstData", "Data from FirstActivity");
                StartActivity(second);
            };
        }
    }
}

