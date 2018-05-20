using System;

namespace ShowcaseView.Interfaces
{
    /// <summary>
    /// Listener for dismissing the show case view
    /// </summary>
    public interface DismissListener
    {
        /// <summary>
        /// Called when a ShowcaseView is dismissed
        /// </summary>
        /// <param name="id"></param>
        void OnDismiss(String id);

        /// <summary>
        /// Called when a ShowCaseView is skipped because of it's show once id
        /// </summary>
        /// <param name="id"></param>
        void OnSkipped(String id);
    }
}