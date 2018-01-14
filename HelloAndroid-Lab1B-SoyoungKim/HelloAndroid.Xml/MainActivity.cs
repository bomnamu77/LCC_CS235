using Android.App;
using Android.Widget;
using Android.OS;

namespace HelloAndroid.Xml
{
    [Activity(Label = "HelloAndroid.Xml", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            var aButton = FindViewById<Button>(Resource.Id.aButton);
            var aLabel = FindViewById<TextView>(Resource.Id.helloLabel);
            aButton.Click += (sender, e) => {
                aLabel.Text = "Hello from the button";
            };
        }
    }
}

