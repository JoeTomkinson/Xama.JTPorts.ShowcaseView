using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using ShowcaseView;
using ShowcaseView.Interfaces;
using Android.Graphics;
using Android.Views.Animations;

namespace xamarinShowcaseSample
{
	[Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
	public class MainActivity : AppCompatActivity, OnViewInflateListener
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
            SetupShowcases();
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

        private void SetupShowcases()
        {
            ButtonNoFocus.Click += (s, e) => {
                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .CloseOnTouch(true)
                .BackgroundColor(Color.DarkRed)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .Title("No Focus")
                .FocusCircleRadiusFactor(1.5)
                .Build();

                showcase.Show();
            };

            ButtonCircle.Click += (s, e) => {
                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .FocusOn(ButtonCircle)
                .CloseOnTouch(true)
                .BackgroundColor(Color.DarkBlue)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .Title("Circle Focus")
                .FocusCircleRadiusFactor(1.5)
                .Build();

                showcase.Show();
            };

            ButtonRoundedRect.Click += (s, e) => {
                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .FocusOn(ButtonRoundedRect)
                .FocusShape(ShowcaseView.Models.FocusShape.RoundedRectangle)
                .CloseOnTouch(true)
                .BackgroundColor(Color.DarkGreen)
                .FocusBorderColor(Color.White)
                .Title("Rounded Rectangle")
                .Build();

                showcase.Show();
            };

            ButtonLargerCircle.Click += (s, e) => {
                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .FocusOn(ButtonLargerCircle)
                .CloseOnTouch(true)
                .BackgroundColor(Color.DarkMagenta)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .Title("Larger Circle")
                .TitleGravity((int)GravityFlags.Top)
                .FocusCircleRadiusFactor(2.5)
                .Build();

                showcase.Show();
            };

            ButtonLongerText.Click += (s, e) => {
                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .FocusOn(ButtonLongerText)
                .CloseOnTouch(true)
                .BackgroundColor(Color.DarkOrchid)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .Title("Longer Text")
                .TitleGravity((int)GravityFlags.Top)
                .FocusCircleRadiusFactor(1.5)
                .Build();

                showcase.Show();
            };

            ButtonWithColour.Click += (s, e) => {
                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .FocusOn(ButtonWithColour)
                .CloseOnTouch(true)
                .BackgroundColor(Color.DarkTurquoise)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .Title("Color Focus")
                .TitleGravity((int)GravityFlags.Top)
                .FocusCircleRadiusFactor(1.5)
                .Build();

                showcase.Show();
            };

            ButtonCustomAnimation.Click += (s, e) => {

                var enterAnim = AnimationUtils.LoadAnimation(this, Resource.Animation.abc_slide_in_bottom);
                var exitAnim = AnimationUtils.LoadAnimation(this, Resource.Animation.abc_slide_in_top);

                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .FocusOn(ButtonCustomAnimation)
                .CloseOnTouch(true)
                .SetEnterAnimation(enterAnim)
                .BackgroundColor(Color.DarkViolet)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .TitleGravity((int)GravityFlags.Top)
                .Title("Custom Animation")
                .FocusCircleRadiusFactor(1.5)
                .Build();

                showcase.Show();
            };

            ButtonCustomView.Click += (s, e) => {
                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .FocusOn(ButtonCustomView)
                .CloseOnTouch(true)
                .CustomView(Resource.Layout.CustomShowcaseView, this)
                .BackgroundColor(Color.DarkBlue)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .TitleGravity((int)GravityFlags.Top)
                .FocusCircleRadiusFactor(1.5)
                .Build();

                showcase.Show();
            };

            ButtonActivity.Click += (s, e) => {
                ShowCaseView showcase = new ShowCaseView.Builder()
                .Context(this)
                .FocusOn(ButtonActivity)
                .CloseOnTouch(true)
                .BackgroundColor(Color.DarkBlue)
                .FocusBorderColor(Color.White)
                .FocusBorderSize(15)
                .Title("Show Activity")
                .TitleGravity((int)GravityFlags.Top)
                .FocusCircleRadiusFactor(1.5)
                .Build();

                showcase.Show();
            };
        }

        public void OnViewInflated(View view)
        {
            //
        }
    }
}
