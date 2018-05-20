using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;

namespace ShowcaseView.Utilities
{
    /// <summary>
    /// Utility class for determining various android specific calculations and defaults
    /// </summary>
    public class ShowcaseUtils
    {
        /// <summary>
        /// Circular reveal animation condition
        /// </summary>
        /// <returns></returns>
        public static bool ShouldShowCircularAnimation()
        {
            return Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop;
        }

        /// <summary>
        /// Calculates focus point values
        /// </summary>
        /// <param name="view"></param>
        /// <param name="circleRadiusFactor"></param>
        /// <param name="adjustHeight"></param>
        /// <returns></returns>
        public static int[] CalculateFocusPointValues(View view, double circleRadiusFactor, int adjustHeight)
        {
            int[] point = new int[3];
            if (view != null)
            {
                int[] viewPoint = new int[2];
                view.GetLocationInWindow(viewPoint);

                point[0] = viewPoint[0] + view.Width / 2;
                point[1] = viewPoint[1] + view.Height / 2 - adjustHeight;
                int radius = (int)((int)(Java.Lang.Math.Hypot(view.Width, view.Height) / 2) * circleRadiusFactor);
                point[2] = radius;
                return point;
            }
            return null;
        }

        /// <summary>
        /// Draws focus circle
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="point"></param>
        /// <param name="radius"></param>
        public static void DrawFocusCircle(Bitmap bitmap, int[] point, int radius)
        {
            Paint p = new Paint();
            p.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Clear));
            Canvas c = new Canvas(bitmap);
            c.DrawCircle(point[0], point[1], radius, p);
        }

        /// <summary>
        /// Returns statusBar height
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static int GetStatusBarHeight(Context context)
        {
            int result = 0;
            int resourceId = context.Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                result = context.Resources.GetDimensionPixelSize(resourceId);
            }
            return result;
        }
    }
}