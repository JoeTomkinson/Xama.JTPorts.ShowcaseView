using Android.App;
using Android.OS;
using Android.Util;
using Android.Views;
using Xama.JTPorts.ShowcaseView.Models;

namespace Xama.JTPorts.ShowcaseView.Utilities
{ 
    public class Calculator
    {
        #region Class Variables

        private int mBitmapWidth, mBitmapHeight;
        private FocusShape mFocusShape;
        private int mFocusWidth, mFocusHeight, mCircleCenterX, mCircleCenterY, mCircleRadius;
        private bool mHasFocus;
        private Activity mActivity;
        private FocusShape mFocusShape1;
        private View mView;
        private double mFocusCircleRadiusFactor;
        private bool mFitSystemWindows;

        /**
* @return Shape of focus
*/
        public FocusShape FocusShape
        {
            get { return mFocusShape; }
        }

        /**
         * @return Focus width
         */
        public int FocusWidth
        {
            get { return mFocusWidth; }
        }

        /**
         * @return Focus height
         */
        public int FocusHeight
        {
            get { return mFocusHeight; }
        }

        /**
         * @return X coordinate of focus circle
         */
        public int CircleCenterX
        {
            get { return mCircleCenterX; }
        }

        /**
         * @return Y coordinate of focus circle
         */
        public int CircleCenterY
        {
            get { return mCircleCenterY; }
        }

        /**
         * @return Radius of focus circle
         */
        public int ViewRadius
        {
            get { return mCircleRadius; }
        }

        /**
         * @return True if there is a view to focus
         */
        public bool HasFocus
        {
            get { return mHasFocus; }
        }

        /**
         * @return Width of background bitmap
         */
        public int BitmapWidth
        {
            get { return mBitmapWidth; }
        }

        /**
         * @return Height of background bitmap
         */
        public int BitmapHeight
        {
            get { return mBitmapHeight; }
        }

        #endregion
        
        public void SetmCircleRadius(int mCircleRadius)
        {
            this.mCircleRadius = mCircleRadius;
        }

        public Calculator(Activity activity, FocusShape focusShape, View view, double radiusFactor, bool fitSystemWindows)
        {
            DisplayMetrics displayMetrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            int deviceWidth = displayMetrics.WidthPixels;
            int deviceHeight = displayMetrics.HeightPixels;
            mBitmapWidth = deviceWidth;
            mBitmapHeight = deviceHeight - (fitSystemWindows ? 0 : ShowcaseUtils.GetStatusBarHeight(activity));
            if (view != null)
            {
                int adjustHeight = (fitSystemWindows && Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop ? 0 : ShowcaseUtils.GetStatusBarHeight(activity));
                int[] viewPoint = new int[2];
                view.GetLocationInWindow(viewPoint);
                mFocusWidth = view.Width;
                mFocusHeight = view.Height;
                mFocusShape = focusShape;
                mCircleCenterX = viewPoint[0] + mFocusWidth / 2;
                mCircleCenterY = viewPoint[1] + mFocusHeight / 2 - adjustHeight;
                mCircleRadius = (int)((int)(Java.Lang.Math.Hypot(view.Width, view.Height) / 2) * radiusFactor);
                mHasFocus = true;
            }
            else
            {
                mHasFocus = false;
            }
        }

        public void SetRectPosition(int positionX, int positionY, int rectWidth, int rectHeight)
        {
            mCircleCenterX = positionX;
            mCircleCenterY = positionY;
            mFocusWidth = rectWidth;
            mFocusHeight = rectHeight;
            mFocusShape = FocusShape.RoundedRectangle;
            mHasFocus = true;
        }

        public void SetCirclePosition(int positionX, int positionY, int radius)
        {
            mCircleCenterX = positionX;
            mCircleRadius = radius;
            mCircleCenterY = positionY;
            mFocusShape = FocusShape.Circle;
            mHasFocus = true;
        }

        /// <summary>
        /// Return Radius of animating circle, given the paramaters
        /// </summary>
        /// <param name="animCounter"></param>
        /// <param name="animMoveFactor"></param>
        /// <returns></returns>
        public float CircleRadius(int animCounter, double animMoveFactor)
        {
            return (float)(mCircleRadius + animCounter * animMoveFactor);
        }

        /// <summary>
        /// Return Bottom position of round rect
        /// </summary>
        /// <param name="animCounter"></param>
        /// <param name="animMoveFactor"></param>
        /// <returns></returns>
        public float RoundRectLeft(int animCounter, double animMoveFactor)
        {
            return (float)(mCircleCenterX - mFocusWidth / 2 - animCounter * animMoveFactor);
        }

        /// <summary>
        /// Return Top position of focus round rect
        /// </summary>
        /// <param name="animCounter"></param>
        /// <param name="animMoveFactor"></param>
        /// <returns></returns>
        public float RoundRectTop(int animCounter, double animMoveFactor)
        {
            return (float)(mCircleCenterY - mFocusHeight / 2 - animCounter * animMoveFactor);
        }

        /// <summary>
        /// Return Bottom position of round rect
        /// </summary>
        /// <param name="animCounter"></param>
        /// <param name="animMoveFactor"></param>
        /// <returns></returns>
        public float RoundRectRight(int animCounter, double animMoveFactor)
        {
            return (float)(mCircleCenterX + mFocusWidth / 2 + animCounter * animMoveFactor);
        }

        /// <summary>
        /// Return Bottom position of round rect
        /// </summary>
        /// <param name="animCounter"></param>
        /// <param name="animMoveFactor"></param>
        /// <returns></returns>
        public float RoundRectBottom(int animCounter, double animMoveFactor)
        {
            return (float)(mCircleCenterY + mFocusHeight / 2 + animCounter * animMoveFactor);
        }

        /// <summary>
        /// Return Radius of focus round rect
        /// </summary>
        /// <param name="animCounter"></param>
        /// <param name="animMoveFactor"></param>
        /// <returns></returns>
        public float RoundRectLeftCircleRadius(int animCounter, double animMoveFactor)
        {
            return (float)(mFocusHeight / 2 + animCounter * animMoveFactor);
        }
    }
}