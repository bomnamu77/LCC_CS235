using Android.App;
using Android.Widget;
using Android.OS;

namespace HelloAndroid
{
    [Activity(Label = "HelloAndroid", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Create the user interface in code

            var layout = new LinearLayout(this);
            layout.Orientation = Orientation.Vertical;
            //Create first Label and Button
            var aLabel = new TextView(this);
            aLabel.SetText(Resource.String.helloLabelText);
            var aButton = new Button(this);
            aButton.SetText(Resource.String.helloButtonText);
            //Create second Label and Button
            var aLabel2 = new TextView(this);
            aLabel2.SetText(Resource.String.helloLabel2Text);
            var aButton2 = new Button(this);
            aButton2.SetText(Resource.String.helloButton2Text);

            // event handler for the first button
            aButton.Click += (sender, e) => {
                aLabel.SetText(Resource.String.helloLabelText2Change);
                aLabel2.SetText(Resource.String.helloLabelText2Change);
            };

            // event handler for the second button
            aButton2.Click += (sender, e) => {
                aLabel.Text = aLabel2.Text = "";
            };

            layout.AddView(aLabel);
            layout.AddView(aButton);
            layout.AddView(aLabel2);
            layout.AddView(aButton2);
            SetContentView(layout);
        }
    }
}

