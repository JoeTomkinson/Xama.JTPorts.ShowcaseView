using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using System;
using ShowcaseView.Utilities;
using ShowcaseView.Models;

namespace ShowcaseView.Interfaces
{
    class ShowCaseImageView : AppCompatImageView
    {
        #region CLASS LEVEL VARIABLES

        private static int DEFAULT_ANIM_COUNTER = 20;
        private Bitmap mBitmap;
        private Paint mBackgroundPaint, mErasePaint, mCircleBorderPaint;
        private Color mBackgroundColor = Color.Transparent;
        private Color mFocusBorderColor = Color.Transparent;
        private int mFocusBorderSize;
        private int mRoundRectRadius = 20;
        private Calculator mCalculator;
        private int mAnimCounter;
        private int mStep = 1;
        private double mAnimMoveFactor = 1;
        private bool mAnimationEnabled = true;
        private Path mPath;
        private RectF rectF;
        private int mFocusAnimationMaxValue;
        private int mFocusAnimationStep;

        #endregion

        #region CONSTRUCTORS

        public ShowCaseImageView(Context context) : base(context)
        {
            Init();
        }

        public ShowCaseImageView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init();
        }

        public ShowCaseImageView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init();
        }

        protected ShowCaseImageView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Init();
        }

        #endregion


        /// <summary>
        /// Initializations for background and paints
        /// </summary>
        private void Init()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
            {
                SetLayerType(LayerType.Hardware, null);
            }

            SetWillNotDraw(false);
            SetBackgroundColor(Color.Transparent);
            mBackgroundPaint = new Paint();
            mBackgroundPaint.AntiAlias = true;
            mBackgroundPaint.Color = mBackgroundColor;
            mBackgroundPaint.Alpha = 0xFF;

            mErasePaint = new Paint();
            mErasePaint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Clear));
            mErasePaint.Alpha = 0xFF;
            mErasePaint.AntiAlias = true;

            mPath = new Path();
            mCircleBorderPaint = new Paint();
            mCircleBorderPaint.AntiAlias = true;
            mCircleBorderPaint.Color = mFocusBorderColor;
            mCircleBorderPaint.StrokeWidth = mFocusBorderSize;
            mCircleBorderPaint.SetStyle(Paint.Style.Stroke);

            rectF = new RectF();
        }

        /// <summary>
        /// Setting parameters for background an animation
        /// </summary>
        /// <param name="backgroundColor"></param>
        /// <param name="calculator"></param>
        public void SetParameters(Color backgroundColor, Calculator calculator)
        {
            mBackgroundColor = backgroundColor;
            mAnimMoveFactor = 1;
            mCalculator = calculator;
        }

        /// <summary>
        /// Setting parameters for focus border
        /// </summary>
        /// <param name="focusBorderColor"></param>
        /// <param name="focusBorderSize"></param>
        public void SetBorderParameters(Color focusBorderColor, int focusBorderSize)
        {
            mFocusBorderSize = focusBorderSize;
            mCircleBorderPaint.Color = focusBorderColor;
            mCircleBorderPaint.StrokeWidth = focusBorderSize;
        }

        /// <summary>
        /// Setting round rectangle radius
        /// </summary>
        /// <param name="roundRectRadius"></param>
        public void setRoundRectRadius(int roundRectRadius)
        {
            mRoundRectRadius = roundRectRadius;
        }

        /// <summary>
        /// Enable/disable animation
        /// </summary>
        /// <param name="animationEnabled"></param>
        public void SetAnimationEnabled(bool animationEnabled)
        {
            mAnimationEnabled = animationEnabled;
            mAnimCounter = mAnimationEnabled ? DEFAULT_ANIM_COUNTER : 0;
        }

        /// <summary>
        /// Draws background and moving focus area
        /// </summary>
        /// <param name="canvas"></param>
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            if (mBitmap == null)
            {
                mBitmap = Bitmap.CreateBitmap(Width, Height, Bitmap.Config.Argb8888);
                mBitmap.EraseColor(mBackgroundColor);

            }
            canvas.DrawBitmap(mBitmap, 0, 0, mBackgroundPaint);

            if (mCalculator.HasFocus)
            {
                if (mCalculator.FocusShape.Equals(FocusShape.Circle))
                {
                    DrawCircle(canvas);
                }
                else
                {
                    DrawRoundedRectangle(canvas);
                }
                if (mAnimationEnabled)
                {
                    if (mAnimCounter == mFocusAnimationMaxValue)
                    {
                        mStep = -1 * mFocusAnimationStep;
                    }
                    else if (mAnimCounter == 0)
                    {
                        mStep = mFocusAnimationStep;
                    }
                    mAnimCounter = mAnimCounter + mStep;
                    PostInvalidate();
                }
            }
        }

        ///  Draws focus circle
        ///  @param canvas canvas to draw
        private void DrawCircle(Canvas canvas)
        {
            canvas.DrawCircle(mCalculator.CircleCenterX, mCalculator.CircleCenterY, mCalculator.CircleRadius(mAnimCounter, mAnimMoveFactor), mErasePaint);

            if (mFocusBorderSize > 0)
            {
                mPath.Reset();
                mPath.MoveTo(mCalculator.CircleCenterX, mCalculator.CircleCenterY);
                mPath.AddCircle(mCalculator.CircleCenterX, mCalculator.CircleCenterY, mCalculator.CircleRadius(mAnimCounter, mAnimMoveFactor), Path.Direction.Cw);
                canvas.DrawPath(mPath, mCircleBorderPaint);
            }
        }

        /// <summary>
        /// Draws focus rounded rectangle
        /// </summary>
        /// <param name="canvas"></param>
        private void DrawRoundedRectangle(Canvas canvas)
        {
            float left = mCalculator.RoundRectLeft(mAnimCounter, mAnimMoveFactor);
            float top = mCalculator.RoundRectTop(mAnimCounter, mAnimMoveFactor);
            float right = mCalculator.RoundRectRight(mAnimCounter, mAnimMoveFactor);
            float bottom = mCalculator.RoundRectBottom(mAnimCounter, mAnimMoveFactor);

            rectF.Set(left, top, right, bottom);
            canvas.DrawRoundRect(rectF, mRoundRectRadius, mRoundRectRadius, mErasePaint);

            if (mFocusBorderSize > 0)
            {
                mPath.Reset();
                mPath.MoveTo(mCalculator.CircleCenterX, mCalculator.CircleCenterY);
                mPath.AddRoundRect(rectF, mRoundRectRadius, mRoundRectRadius, Path.Direction.Cw);
                canvas.DrawPath(mPath, mCircleBorderPaint);
            }
        }

        public void SetFocusAnimationParameters(int maxValue, int step)
        {
            mFocusAnimationMaxValue = maxValue;
            mFocusAnimationStep = step;
        }

    }
}