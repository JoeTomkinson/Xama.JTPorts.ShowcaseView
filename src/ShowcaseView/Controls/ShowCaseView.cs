using Android;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using System;
using Xama.JTPorts.ShowcaseView.Models;
using Xama.JTPorts.ShowcaseView.Utilities;

namespace Xama.JTPorts.ShowcaseView.Interfaces
{
    public class ShowCaseView : FrameLayout, ViewTreeObserver.IOnGlobalLayoutListener, OnViewInflateListener
    {
        #region CLASS LEVEL VARIABLE DECLARATIONS

        // Tag for container view
        private static string CONTAINER_TAG = "ShowCaseViewTag";

        // SharedPreferences name
        private static string PREF_NAME = "PrefShowCaseView";

        // Building paramaters required
        private Activity mActivity;
        private string mTitle;
        private ISpanned mSpannedTitle;
        private string mId;
        private double mFocusCircleRadiusFactor;
        private View mView;
        private Color mBackgroundColor;
        private Color mFocusBorderColor;
        private int mTitleGravity;
        private int mTitleStyle;
        private int mTitleSize;
        private int mTitleSizeUnit;
        private int mCustomViewRes;
        private int mFocusBorderSize;
        private int mRoundRectRadius;
        private OnViewInflateListener mViewInflateListener;
        private Animation mEnterAnimation, mExitAnimation;
        private AnimationListener mAnimationListener;
        private bool mCloseOnTouch;
        private bool mEnableTouchOnFocusedView;
        private bool mFitSystemWindows;
        private FocusShape mFocusShape;
        private DismissListener mDismissListener;
        private long mDelay;
        private int mAnimationDuration = 400;
        private int mFocusAnimationMaxValue;
        private int mFocusAnimationStep;
        private int mCenterX, mCenterY;
        private ViewGroup mRoot;
        private ISharedPreferences mSharedPreferences;
        private Calculator mCalculator;
        private int mFocusPositionX, mFocusPositionY, mFocusCircleRadius, mFocusRectangleWidth, mFocusRectangleHeight;
        private float[] mLastTouchDownXY = new float[2];
        private bool mFocusAnimationEnabled;

        public DismissListener DismissListener
        {
            get { return mDismissListener; }
            set { mDismissListener = value; }
        }

        public int FocusCenterX
        {
            get { return mCalculator.CircleCenterX; }
        }

        public int FocusCenterY
        {
            get { return mCalculator.CircleCenterY; }
        }

        public float FocusRadius
        {
            get { return FocusShape.Circle.Equals(mFocusShape) ? mCalculator.CircleRadius(0, 1) : 0; }
        }

        public int FocusWidth
        {
            get { return mCalculator.FocusWidth; }
        }

        public int FocusHeight
        {
            get { return mCalculator.FocusHeight; }
        }

        #endregion

        #region CONSTRUCTORS

        protected ShowCaseView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public ShowCaseView(Context context) : base(context)
        {
        }

        public ShowCaseView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public ShowCaseView(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public ShowCaseView(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }


        private ShowCaseView(Context mActivity, string mTitle, ISpanned mSpannedTitle, string mId, double mFocusCircleRadiusFactor, View mView, Color mBackgroundColor, Color mFocusBorderColor, int mTitleGravity, int mTitleStyle, int mTitleSize, int mTitleSizeUnit, int mCustomViewRes, int mFocusBorderSize, int mRoundRectRadius, OnViewInflateListener mViewInflateListener, Animation mEnterAnimation, Animation mExitAnimation, AnimationListener mAnimationListener, bool mCloseOnTouch, bool mEnableTouchOnFocusedView, bool mFitSystemWindows, FocusShape mFocusShape, DismissListener mDismissListener, long mDelay, int mFocusAnimationMaxValue, int mFocusAnimationStep, int mFocusPositionX, int mFocusPositionY, int mFocusCircleRadius, int mFocusRectangleWidth, int mFocusRectangleHeight, bool mFocusAnimationEnabled, int mAnimationDuration = 400, int mCenterX = 0, int mCenterY = 0, ViewGroup mRoot = null, ISharedPreferences mSharedPreferences = null, Calculator mCalculator = null, float[] mLastTouchDownXY = null) : base(mActivity)
        {
            this.mActivity = (mActivity as Activity);
            this.mTitle = mTitle;
            this.mSpannedTitle = mSpannedTitle;
            this.mId = mId;
            this.mFocusCircleRadiusFactor = mFocusCircleRadiusFactor;
            this.mView = mView;
            this.mBackgroundColor = mBackgroundColor;
            this.mFocusBorderColor = mFocusBorderColor;
            this.mTitleGravity = mTitleGravity;
            this.mTitleStyle = mTitleStyle;
            this.mTitleSize = mTitleSize;
            this.mTitleSizeUnit = mTitleSizeUnit;
            this.mCustomViewRes = mCustomViewRes;
            this.mFocusBorderSize = mFocusBorderSize;
            this.mRoundRectRadius = mRoundRectRadius;
            this.mViewInflateListener = mViewInflateListener;
            this.mEnterAnimation = mEnterAnimation;
            this.mExitAnimation = mExitAnimation;
            this.mAnimationListener = mAnimationListener;
            this.mCloseOnTouch = mCloseOnTouch;
            this.mEnableTouchOnFocusedView = mEnableTouchOnFocusedView;
            this.mFitSystemWindows = mFitSystemWindows;
            this.mFocusShape = mFocusShape;
            this.mDismissListener = mDismissListener;
            this.mDelay = mDelay;
            this.mAnimationDuration = mAnimationDuration;
            this.mFocusAnimationMaxValue = mFocusAnimationMaxValue;
            this.mFocusAnimationStep = mFocusAnimationStep;
            this.mCenterX = mCenterX;
            this.mCenterY = mCenterY;
            this.mRoot = mRoot;
            this.mSharedPreferences = mSharedPreferences;
            this.mCalculator = mCalculator;
            this.mFocusPositionX = mFocusPositionX;
            this.mFocusPositionY = mFocusPositionY;
            this.mFocusCircleRadius = mFocusCircleRadius;
            this.mFocusRectangleWidth = mFocusRectangleWidth;
            this.mFocusRectangleHeight = mFocusRectangleHeight;
            this.mLastTouchDownXY = mLastTouchDownXY;
            this.mFocusAnimationEnabled = mFocusAnimationEnabled;

            InitializeParameters();
        }

        #endregion

        /// <summary>
        /// Calculates and set initial parameters
        /// </summary>
        private void InitializeParameters()
        {
            // default if none provided. This is depreciated though, so need a work around for the class library.
            mBackgroundColor = mBackgroundColor != 0 ? mBackgroundColor : Resources.GetColor(Resource.Color.showcase_defaultcolour);

            if (mTitleGravity != -2)
            {
                mTitleGravity = mTitleGravity >= 0 ? mTitleGravity : (int)GravityFlags.Center;
            }

            // default if none provided.
            mTitleStyle = mTitleStyle != 0 ? mTitleStyle : Resource.Style.ShowcaseDefaultTitleStyle;

            DisplayMetrics displayMetrics = new DisplayMetrics();
            mActivity.WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            int deviceWidth = displayMetrics.WidthPixels;
            int deviceHeight = displayMetrics.HeightPixels;
            mCenterX = deviceWidth / 2;
            mCenterY = deviceHeight / 2;
            mSharedPreferences = mActivity.GetSharedPreferences(PREF_NAME, FileCreationMode.Private);
        }

       /// <summary>
        /// Resets all show once flags
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        public static void ResetShowOnce(Context context, string id)
        {
            ISharedPreferences sharedPrefs = context.GetSharedPreferences(PREF_NAME, FileCreationMode.Private);
            sharedPrefs.Edit().Remove(id).Commit();
        }

        /// <summary>
        /// Shows the showcase view
        /// </summary>
        public void Show()
        {
            if (mActivity == null || (mId != null && IsShownBefore()))
            {
                if (mDismissListener != null)
                {
                    mDismissListener.OnSkipped(mId);
                }
                return;
            }

            if (mView != null)
            {
                // if view is not laid out get width/height values in onGlobalLayout
                if (mView.Width == 0 && mView.Height == 0)
                {
                    mView.ViewTreeObserver.AddOnGlobalLayoutListener(this);
                }
                else
                {
                    Focus();
                }
            }
            else
            {
                Focus();
            }
        }

        /// <summary>
        /// Determines visble view anit's circle positioning
        /// </summary>
        private void Focus()
        {
            mCalculator = new Calculator(mActivity, mFocusShape, mView, mFocusCircleRadiusFactor, mFitSystemWindows);
            ViewGroup androidContent = mActivity.FindViewById<ViewGroup>(Android.Resource.Id.Content);
            mRoot = (ViewGroup)androidContent.Parent.Parent;

            mRoot.PostDelayed(() =>
            {
                if (mActivity == null || mActivity.IsFinishing)
                {
                    return;
                }

                ShowCaseView visibleView = (ShowCaseView)mRoot.FindViewWithTag(CONTAINER_TAG);
                Clickable = !mEnableTouchOnFocusedView;

                if (visibleView == null)
                {
                    Tag = CONTAINER_TAG;

                    if (mCloseOnTouch)
                    {
                        SetupTouchListener();
                    }

                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

                    mRoot.AddView(this);

                    ShowCaseImageView imageView = new ShowCaseImageView(mActivity);
                    imageView.SetFocusAnimationParameters(mFocusAnimationMaxValue, mFocusAnimationStep);

                    if (mCalculator.HasFocus)
                    {
                        mCenterX = mCalculator.CircleCenterX;
                        mCenterY = mCalculator.CircleCenterY;
                    }

                    imageView.SetParameters(mBackgroundColor, mCalculator);

                    if (mFocusRectangleWidth > 0 && mFocusRectangleHeight > 0)
                    {
                        mCalculator.SetRectPosition(mFocusPositionX, mFocusPositionY, mFocusRectangleWidth, mFocusRectangleHeight);
                    }

                    if (mFocusCircleRadius > 0)
                    {
                        mCalculator.SetCirclePosition(mFocusPositionX, mFocusPositionY, mFocusCircleRadius);
                    }

                    imageView.SetAnimationEnabled(mFocusAnimationEnabled);
                    imageView.LayoutParameters = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

                    if (mFocusBorderColor != 0 && mFocusBorderSize > 0)
                    {
                        imageView.SetBorderParameters(mFocusBorderColor, mFocusBorderSize);
                    }

                    if (mRoundRectRadius > 0)
                    {
                        imageView.setRoundRectRadius(mRoundRectRadius);
                    }

                    AddView(imageView);

                    if (mCustomViewRes == 0)
                    {
                        InflateTitleView();
                    }
                    else
                    {
                        InflateCustomView(mCustomViewRes, mViewInflateListener);
                    }

                    StartEnterAnimation();
                    WriteShown();
                }
            }, mDelay);
        }

        private void SetupTouchListener()
        {
            if (mEnableTouchOnFocusedView)
            {
                bool isWithin = false;

                Touch += (s, e) =>
                {
                    if (e.Event.ActionMasked == MotionEventActions.Down)
                    {
                        float x = e.Event.GetX();
                        float y = e.Event.GetY();

                        switch (mFocusShape)
                        {
                            case FocusShape.Circle:
                                double distance = Java.Lang.Math.Sqrt(
                                        Java.Lang.Math.Pow((FocusCenterX - x), 2)
                                                + Java.Lang.Math.Pow((FocusCenterY - y), 2));

                                isWithin = Java.Lang.Math.Abs(distance) < FocusRadius;
                                break;
                            case FocusShape.RoundedRectangle:
                                Rect rect = new Rect();
                                int left = FocusCenterX - (FocusWidth / 2);
                                int right = FocusCenterX + (FocusWidth / 2);
                                int top = FocusCenterY - (FocusHeight / 2);
                                int bottom = FocusCenterY + (FocusHeight / 2);
                                rect.Set(left, top, right, bottom);
                                isWithin = rect.Contains((int)x, (int)y);
                                break;
                        }
                    }

                    if (isWithin)
                    {
                        if (mCloseOnTouch)
                        {
                            Hide();
                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        //
                    }

                };
            }
            else
            {
                this.Click += (s, e) =>
                {
                    Hide();
                };
            }
        }

        /// <summary>
        /// Check is the ShowCaseView visible
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        public static bool IsVisible(Activity activity)
        {
            ViewGroup androidContent = activity.FindViewById<ViewGroup>(Android.Resource.Id.Content);
            ViewGroup mRoot = (ViewGroup)androidContent.Parent.Parent;
            ShowCaseView mContainer = (ShowCaseView)mRoot.FindViewWithTag(CONTAINER_TAG);
            return mContainer != null;
        }

        /// <summary>
        /// Hides the current ShowcaseView
        /// </summary>
        /// <param name="activity"></param>
        public static void HideCurrent(Activity activity)
        {
            ViewGroup androidContent = activity.FindViewById<ViewGroup>(Android.Resource.Id.Content);
            ViewGroup mRoot = (ViewGroup)androidContent.Parent.Parent;
            ShowCaseView mContainer = (ShowCaseView)mRoot.FindViewWithTag(CONTAINER_TAG);
            mContainer.Hide();
        }

        /// <summary>
        /// Starts enter animation of ShowCaseView
        /// </summary>
        private void StartEnterAnimation()
        {
            if (mEnterAnimation != null)
            {
                StartAnimation(mEnterAnimation);
            }
            else if (ShowcaseUtils.ShouldShowCircularAnimation())
            {
                DoCircularEnterAnimation();
            }
            else
            {
                Animation fadeInAnimation = AnimationUtils.LoadAnimation(mActivity, Resource.Animation.fade_in);
                fadeInAnimation.FillAfter = true;
                fadeInAnimation.AnimationEnd += (s, e) =>
                {
                    if (mAnimationListener != null)
                    {
                        mAnimationListener.OnEnterAnimationEnd();
                    }
                };

                StartAnimation(fadeInAnimation);
            }
        }

        /// <summary>
        /// Hides the ShowCaseView with animation
        /// </summary>
        public void Hide()
        {
            if (mExitAnimation != null)
            {
                StartAnimation(mExitAnimation);
            }
            else if (ShowcaseUtils.ShouldShowCircularAnimation())
            {
                DoCircularExitAnimation();
            }
            else
            {
                Animation fadeOut = AnimationUtils.LoadAnimation(mActivity, Resource.Animation.fade_out);
                fadeOut.AnimationEnd += (s, e) =>
                {
                    RemoveView();
                    if (mAnimationListener != null)
                    {
                        mAnimationListener.OnExitAnimationEnd();
                    }

                };

                fadeOut.FillAfter = true;
                StartAnimation(fadeOut);
            }
        }

        /// <summary>
        /// Inflates the custom view if one is supplied
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="viewInflateListener"></param>
        private void InflateCustomView(int layout, OnViewInflateListener viewInflateListener)
        {
            View view = mActivity.LayoutInflater.Inflate(layout, this, false);
            this.AddView(view);
            if (viewInflateListener != null)
            {
                viewInflateListener.OnViewInflated(view);
            }
        }

        /// <summary>
        /// Inflates the title view layout
        /// </summary>
        private void InflateTitleView()
        {
            InflateCustomView(Resource.Layout.ShowcaseViewTitleLayout, this);
        }

        /// <summary>
        ///  Circular reveal enter animation
        /// </summary>
        private void DoCircularEnterAnimation()
        {
            ViewTreeObserver.GlobalLayout += CircularGlobalLayout;
        }

        /// <summary>
        /// Changed interface pattern to event, so that we can unsubscribe on first call
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CircularGlobalLayout(object sender, EventArgs e)
        {
            ViewTreeObserver.GlobalLayout -= CircularGlobalLayout;

            int revealRadius = (int)Java.Lang.Math.Hypot(Width, Height);
            int startRadius = 0;

            if (mView != null)
            {
                startRadius = mView.Width / 2;
            }
            else if (mFocusCircleRadius > 0 || mFocusRectangleWidth > 0 || mFocusRectangleHeight > 0)
            {
                mCenterX = mFocusPositionX;
                mCenterY = mFocusPositionY;
            }
            Animator enterAnimator = ViewAnimationUtils.CreateCircularReveal(this, mCenterX, mCenterY, startRadius, revealRadius);

            enterAnimator.SetDuration(mAnimationDuration);

            if (mAnimationListener != null)
            {
                enterAnimator.AnimationEnd += (r, t) =>
                {
                    mAnimationListener.OnEnterAnimationEnd();
                };
            }

            enterAnimator.SetInterpolator(AnimationUtils.LoadInterpolator(mActivity, Android.Resource.Interpolator.AccelerateCubic));
            enterAnimator.Start();
        }

        /// <summary>
        ///  Circular reveal exit animation
        /// </summary>
        private void DoCircularExitAnimation()
        {
            int revealRadius = (int)Java.Lang.Math.Hypot(Width, Height);
            Animator exitAnimator = ViewAnimationUtils.CreateCircularReveal(this, mCenterX, mCenterY, revealRadius, 0f);
            exitAnimator.SetDuration(mAnimationDuration);
            exitAnimator.SetInterpolator(AnimationUtils.LoadInterpolator(mActivity, Android.Resource.Interpolator.DecelerateCubic));

            exitAnimator.AnimationEnd += (s, e) =>
            {
                RemoveView();
                if (mAnimationListener != null)
                {
                    mAnimationListener.OnExitAnimationEnd();
                }
            };

            exitAnimator.Start();
        }

        /// <summary>
        /// Saves the ShowCaseView id to SharedPreferences that is shown once
        /// </summary>
        private void WriteShown()
        {
            ISharedPreferencesEditor editor = mSharedPreferences.Edit();
            editor.PutBoolean(mId, true);
            editor.Apply();
        }

        /// <summary>
        /// Returns if the ShowCaseView with given id is shown before
        /// </summary>
        /// <returns></returns>
        public bool IsShownBefore()
        {
            return mSharedPreferences.GetBoolean(mId, false);
        }

        /// <summary>
        /// Removes the ShowCaseView view from activity root view
        /// </summary>
        public void RemoveView()
        {
            mRoot.RemoveView(this);
            if (mDismissListener != null)
            {
                mDismissListener.OnDismiss(mId);
            }
        }

        /// <summary>
        /// Implemented as part of the interface used by this class
        /// </summary>
        public void OnGlobalLayout()
        {
            mView.ViewTreeObserver.RemoveOnGlobalLayoutListener(this); //This is accurate, this is definately part of this class
            Focus();
        }

        public void OnViewInflated(View view)
        {
            TextView textView = (TextView)view.FindViewById(Resource.Id.scv_title);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                textView.SetTextAppearance(mTitleStyle);
            }
            else
            {
                // This is depreciated, but works on older APIs
                textView.SetTextAppearance(mActivity, mTitleStyle);
            }

            if (mTitleSize != -1)
            {
                textView.SetTextSize((ComplexUnitType)mTitleSizeUnit, mTitleSize);
            }
            
            textView.Gravity = (GravityFlags)mTitleGravity;
            
            if (mFitSystemWindows)
            {
                RelativeLayout.LayoutParams para = (RelativeLayout.LayoutParams)textView.LayoutParameters;

                para.SetMargins(0, ShowcaseUtils.GetStatusBarHeight(Context), 0, 0);
            }
            if (mSpannedTitle != null)
            {
                textView.TextFormatted = mSpannedTitle;
            }
            else
            {
                textView.Text = mTitle;
            }
        }

        /// <summary>
        ///  Builder class for a ShowCaseView
        ///  Adheres to the C# static builder design pattern.
        /// </summary>
        public class Builder
        {
            private Activity mActivity;
            private View mView;
            private string mId;
            private string mTitle;
            private ISpanned mSpannedTitle;
            private double mFocusCircleRadiusFactor = 1;
            private Color mBackgroundColor;
            private Color mFocusBorderColor;
            private int mTitleGravity = -1;
            private int mTitleSize = -1;
            private int mTitleSizeUnit = -1;
            private int mTitleStyle;
            private int mCustomViewRes;
            private int mRoundRectRadius;
            private OnViewInflateListener mViewInflateListener;
            private Animation mEnterAnimation, mExitAnimation;
            private AnimationListener mAnimationListener;
            private bool mCloseOnTouch = true;
            private bool mEnableTouchOnFocusedView;
            private bool mFitSystemWindows;
            private FocusShape mFocusShape = Models.FocusShape.Circle;
            private DismissListener mDismissListener = null;
            private int mFocusBorderSize;
            private int mFocusPositionX, mFocusPositionY, mFocusCircleRadius, mFocusRectangleWidth, mFocusRectangleHeight;
            private bool mFocusAnimationEnabled = true;
            private int mFocusAnimationMaxValue = 20;
            private int mFocusAnimationStep = 1;
            private long mDelay;

            /// <summary>
            /// Required context for the builder class
            /// </summary>
            /// <param name="activity"></param>
            /// <returns></returns>
            public Builder Context(Activity activity)
            {
                mActivity = activity;
                return this;
            }

            /// <summary>
            /// Set ShowCaseView title using a string
            /// </summary>
            /// <param name="title"></param>
            /// <returns></returns>
            public Builder Title(string title)
            {
                mTitle = title;
                mSpannedTitle = null;
                return this;
            }

            /// <summary>
            /// Set ShowCaseView title using ISpanned value
            /// </summary>
            /// <param name="title"></param>
            /// <returns></returns>
            public Builder Title(ISpanned title)
            {
                mSpannedTitle = title;
                mTitle = null;
                return this;
            }

            /// <summary>
            /// Set the title style using a resource Id and supply the desired title gravity.
            /// </summary>
            /// <param name="style"></param>
            /// <param name="titleGravity"></param>
            /// <returns></returns>
            public Builder TitleStyle(int style, int titleGravity)
            {
                mTitleGravity = titleGravity;
                mTitleStyle = style;
                return this;
            }

            /// <summary>
            /// Set Border color for focus shape
            /// </summary>
            /// <param name="focusBorderColor"></param>
            /// <returns></returns>
            public Builder FocusBorderColor(Color focusBorderColor)
            {
                mFocusBorderColor = focusBorderColor;
                return this;
            }

            /// <summary>
            /// Border size for focus shape
            /// </summary>
            /// <param name="focusBorderSize"></param>
            /// <returns></returns>
            public Builder FocusBorderSize(int focusBorderSize)
            {
                mFocusBorderSize = focusBorderSize;
                return this;
            }

            /// <summary>
            /// Title gravity
            /// </summary>
            /// <param name="titleGravity"></param>
            /// <returns></returns>
            public Builder TitleGravity(int titleGravity)
            {
                mTitleGravity = titleGravity;
                return this;
            }

            /// <summary>
            /// The defined text size overrides any defined size in the default or provided style
            /// </summary>
            /// <param name="titleSize"></param>
            /// <param name="unit"></param>
            /// <returns></returns>
            public Builder TitleSize(int titleSize, int unit)
            {
                mTitleSize = titleSize;
                mTitleSizeUnit = unit;
                return this;
            }

            /// <summary>
            /// Set an id as the unique identifier for the ShowCaseView
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public Builder ShowOnce(string id)
            {
                mId = id;
                return this;
            }

            /// <summary>
            /// View to focus on
            /// </summary>
            /// <param name="view"></param>
            /// <returns></returns>
            public Builder FocusOn(View view)
            {
                mView = view;
                return this;
            }

            /// <summary>
            /// Background color of FancyShowCaseView
            /// </summary>
            /// <param name="backgroundColor"></param>
            /// <returns></returns>
            public Builder BackgroundColor(Color backgroundColor)
            {
                mBackgroundColor = backgroundColor;
                return this;
            }

            /// <summary>
            /// Focus circle radius factor (default value = 1)
            /// </summary>
            /// <param name="focusCircleRadiusFactor"></param>
            /// <returns></returns>
            public Builder FocusCircleRadiusFactor(double focusCircleRadiusFactor)
            {
                mFocusCircleRadiusFactor = focusCircleRadiusFactor;
                return this;
            }

            /// <summary>
            /// set the custom view layout resource
            /// </summary>
            /// <param name="layoutResource"></param>
            /// <param name="listener"></param>
            /// <returns></returns>
            public Builder CustomView(int layoutResource, OnViewInflateListener listener)
            {
                mCustomViewRes = layoutResource;
                mViewInflateListener = listener;
                return this;
            }

            /// <summary>
            ///  Set a custom enter animation for the ShowCaseView
            /// </summary>
            /// <param name="enterAnimation"></param>
            /// <returns></returns>
            public Builder SetEnterAnimation(Animation enterAnimation)
            {
                mEnterAnimation = enterAnimation;
                return this;
            }

            /// <summary>
            ///  Listener for enter/exit animations
            /// </summary>
            /// <param name="listener"></param>
            /// <returns></returns>
            public Builder AnimationListener(AnimationListener listener)
            {
                mAnimationListener = listener;
                return this;
            }

            /// <summary>
            /// Set a custom exit animation for the ShowCaseView
            /// </summary>
            /// <param name="exitAnimation"></param>
            /// <returns></returns>
            public Builder ExitAnimation(Animation exitAnimation)
            {
                mExitAnimation = exitAnimation;
                return this;
            }

            /// <summary>
            ///  Set if ShowcaseView should close on touch
            /// </summary>
            /// <param name="closeOnTouch"></param>
            /// <returns></returns>
            public Builder CloseOnTouch(bool closeOnTouch)
            {
                mCloseOnTouch = closeOnTouch;
                return this;
            }

            /// <summary>
            /// Closes on touch of focused view if enabled
            /// </summary>
            /// <param name="enableTouchOnFocusedView"></param>
            /// <returns></returns>
            public Builder EnableTouchOnFocusedView(bool enableTouchOnFocusedView)
            {
                mEnableTouchOnFocusedView = enableTouchOnFocusedView;
                return this;
            }

            /// <summary>
            /// This should be the same as root view's fitSystemWindows value
            /// </summary>
            /// <param name="fitSystemWindows"></param>
            /// <returns></returns>
            public Builder FitSystemWindows(bool fitSystemWindows)
            {
                mFitSystemWindows = fitSystemWindows;
                return this;
            }

            /// <summary>
            /// Select the type of focus shape: Circle or Rounded Rectangle
            /// </summary>
            /// <param name="focusShape"></param>
            /// <returns></returns>
            public Builder FocusShape(FocusShape focusShape)
            {
                mFocusShape = focusShape;
                return this;
            }

            /// <summary>
            /// Focus round rectangle at specific position
            /// </summary>
            /// <param name="positionX"></param>
            /// <param name="positionY"></param>
            /// <param name="positionWidth"></param>
            /// <param name="positionHeight"></param>
            /// <returns></returns>
            public Builder FocusRectAtPosition(int positionX, int positionY, int positionWidth, int positionHeight)
            {
                mFocusPositionX = positionX;
                mFocusPositionY = positionY;
                mFocusRectangleWidth = positionWidth;
                mFocusRectangleHeight = positionHeight;
                return this;
            }

            /// <summary>
            /// Focus circle at specific position
            /// </summary>
            /// <param name="positionX"></param>
            /// <param name="positionY"></param>
            /// <param name="radius"></param>
            /// <returns></returns>
            public Builder FocusCircleAtPosition(int positionX, int positionY, int radius)
            {
                mFocusPositionX = positionX;
                mFocusPositionY = positionY;
                mFocusCircleRadius = radius;
                return this;
            }

            /// <summary>
            /// Set the dismiss listener
            /// </summary>
            /// <param name="dismissListener"></param>
            /// <returns></returns>
            public Builder SetDismissListener(DismissListener dismissListener)
            {
                mDismissListener = dismissListener;
                return this;
            }

            /// <summary>
            /// Set the radius of the rounded rectangle from the focused view
            /// </summary>
            /// <param name="roundRectRadius"></param>
            /// <returns></returns>
            public Builder RoundRectRadius(int roundRectRadius)
            {
                mRoundRectRadius = roundRectRadius;
                return this;
            }

            /// <summary>
            /// Disable Focus Animation
            /// </summary>
            /// <returns></returns>
            public Builder DisableFocusAnimation()
            {
                mFocusAnimationEnabled = false;
                return this;
            }

            public Builder FocusAnimationMaxValue(int focusAnimationMaxValue)
            {
                mFocusAnimationMaxValue = focusAnimationMaxValue;
                return this;
            }

            public Builder FocusAnimationStep(int focusAnimationStep)
            {
                mFocusAnimationStep = focusAnimationStep;
                return this;
            }

            public Builder Delay(int delayInMillis)
            {
                mDelay = delayInMillis;
                return this;
            }

            /// <summary>
            /// Builds the builder
            /// </summary>
            /// <returns></returns>
            public ShowCaseView Build()
            {
                if (mActivity == null)
                {
                    throw new Exception("No Context Supplied", new Exception("The ShowCaseView Builder requires context to be supplied as it's first builder element"));
                }

                return new ShowCaseView(
                    mActivity,
                    mTitle,
                    mSpannedTitle,
                    mId,
                    mFocusCircleRadiusFactor,
                    mView,
                    mBackgroundColor,
                    mFocusBorderColor,
                    mTitleGravity,
                    mTitleStyle,
                    mTitleSize,
                    mTitleSizeUnit,
                    mCustomViewRes,
                    mFocusBorderSize,
                    mRoundRectRadius,
                    mViewInflateListener,
                    mEnterAnimation,
                    mExitAnimation,
                    mAnimationListener,
                    mCloseOnTouch,
                    mEnableTouchOnFocusedView,
                    mFitSystemWindows,
                    mFocusShape,
                    mDismissListener,
                    mDelay,
                    mFocusAnimationMaxValue,
                    mFocusAnimationStep,
                    mFocusPositionX,
                    mFocusPositionY,
                    mFocusCircleRadius,
                    mFocusRectangleWidth,
                    mFocusRectangleHeight,
                    mFocusAnimationEnabled);
            }

            internal object FocusOn(object profileName)
            {
                throw new NotImplementedException();
            }
        }
    }
}

