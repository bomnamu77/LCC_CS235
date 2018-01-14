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
            var aButton2 = FindViewById<Button>(Resource.Id.aButton2);
            var aLabel2 = FindViewById<TextView>(Resource.Id.helloLabel2);


            // event handler for the first button
            aButton.Click += (sender, e) => {
                aLabel.SetText(Resource.String.helloLabelText2Change);
                aLabel2.SetText(Resource.String.helloLabelText2Change);
            };

            // event handler for the second button
            aButton2.Click += (sender, e) => {
                aLabel.Text = aLabel2.Text = "";
            };
        }
    }
}

