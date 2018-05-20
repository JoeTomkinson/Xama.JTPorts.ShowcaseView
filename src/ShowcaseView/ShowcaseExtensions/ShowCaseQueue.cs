using ShowcaseView.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShowcaseView.ShowcaseExtensions
{
    class ShowCaseQueue : DismissListener
    {
        #region CLASS LEVEL VARIABLE DECLARATIONS

        private Queue<ShowCaseView> mQueue;
        private DismissListener mCurrentOriginalDismissListener;
        private ShowCaseView mCurrent;
        private TimeSpan? autoRunDelay;
        private bool autoRunClosesQueue = true;
        
        public event EventHandler QueueCompleted;
        public event EventHandler<ShowCaseView> ShowCaseViewCompleted;

        /// <summary>
        /// Auto run is off by default unless a delay is specified.
        /// </summary>
        public TimeSpan? AutoRunDelay
        {
            get { return autoRunDelay; }
            set { autoRunDelay = value; }
        }
        
        /// <summary>
        /// Should the last showcaseview in the queue auto hide if autorundelay is set, defaults to true.
        /// </summary>
        public bool ShouldAutoRunCloseQueue
        {
            get { return autoRunClosesQueue; }
            set { autoRunClosesQueue = value; }
        }

        /// <summary>
        /// Retrieves current showcase view within the queue.
        /// </summary>
        public ShowCaseView Current
        {
            get { return mCurrent; }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShowCaseQueue()
        {
            mQueue = new Queue<ShowCaseView>();
            mCurrentOriginalDismissListener = null;
        }

        #endregion

        /// <summary>
        /// Add item into the showcase
        /// </summary>
        /// <param name="showCaseView"></param>
        /// <returns></returns>
        public ShowCaseQueue Add(ShowCaseView showCaseView)
        {
            mQueue.Enqueue(showCaseView);
            return this;
        }

        /// <summary>
        /// Add range of items into the showcase
        /// </summary>
        /// <param name="showCaseView"></param>
        /// <returns></returns>
        public ShowCaseQueue AddRange(List<ShowCaseView> showCaseViews)
        {
            foreach (var item in showCaseViews)
            {
                mQueue.Enqueue(item);
            }

            return this;
        }

        /// <summary>
        /// Starts displaying all views in order of their insertion into the queue
        /// </summary>
        public async void Show()
        {
            if (mQueue.Count != 0)
            {
                mCurrent = mQueue.Dequeue();
                mCurrentOriginalDismissListener = mCurrent.DismissListener;
                mCurrent.DismissListener = this;
                mCurrent.Show();

                if (autoRunDelay != null)
                {
                    await Task.Delay((TimeSpan)autoRunDelay);

                    if (mQueue.Count > 0)
                    {
                        mCurrent.Hide();
                    }
                    else if (mQueue.Count == 0 && autoRunClosesQueue)
                    {
                        mCurrent.Hide();
                    }
                }
            }
            else if (QueueCompleted != null)
            {
                QueueCompleted.Invoke(this, new EventArgs() { });
            }
        }

        /// <summary>
        /// Cancels the Queue
        /// </summary>
        /// <param name="hideCurrent"></param>
        public void Cancel(bool hideCurrent)
        {
            if (hideCurrent && mCurrent != null)
            {
                mCurrent.Hide();
            }
            if (mQueue.Count != 0)
            {
                mQueue.Clear();
            }
        }

        /// <summary>
        /// Interface implementation of dismiss event
        /// </summary>
        /// <param name="id"></param>
        public void OnDismiss(string id)
        {
            if (mCurrentOriginalDismissListener != null)
            {
                mCurrentOriginalDismissListener.OnDismiss(id);
            }

            ShowCaseViewCompleted?.Invoke(this, mCurrent);

            Show();
        }

        /// <summary>
        /// Interface implementation of skip event
        /// </summary>
        /// <param name="id"></param>
        public void OnSkipped(string id)
        {
            if (mCurrentOriginalDismissListener != null)
            {
                mCurrentOriginalDismissListener.OnSkipped(id);
            }

            ShowCaseViewCompleted?.Invoke(this, mCurrent);

            Show();
        }
    }
}