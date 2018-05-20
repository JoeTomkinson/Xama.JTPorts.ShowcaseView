using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;

namespace xamarinShowcaseSample
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity
	{
        private Button ButtonNoFocus, ButtonCircle, ButtonRoundedRect, ButtonLargerCircle, ButtonLongerText, ButtonWithColour, ButtonCustomAnimation, ButtonCustomView, ButtonActivity;


        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);

			Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            // Shwocase Interactibles
            
            ButtonNoFocus = FindViewById<Button>(Resource.Id.buttonNoFocus);
            ButtonCircle = FindViewById<Button>(Resource.Id.buttonCircle);
            ButtonRoundedRect = FindViewById<Button>(Resource.Id.buttonRoundedRect);
            ButtonLargerCircle = FindViewById<Button>(Resource.Id.buttonLargerCircle);
            ButtonLongerText = FindViewById<Button>(Resource.Id.buttonLongerText);
            ButtonWithColour = FindViewById<Button>(Resource.Id.buttonWithColour);
            ButtonCustomAnimation = FindViewById<Button>(Resource.Id.buttonCustomAnimation);
            ButtonCustomView = FindViewById<Button>(Resource.Id.buttonCustomView);
            ButtonActivity = FindViewById<Button>(Resource.Id.buttonActivity);

            // Showcase set-up


        }

		public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Feel free to message me through StackOverflow, user:4486115", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
	}
}
