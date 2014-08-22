
namespace WinRTUtils.Messages
{
    /// <summary>
    /// Message for initiating a page navigation
    /// </summary>
    public class NavigateMessage
    {
        /// <summary>
        /// Name of the target page
        /// </summary>
        public string TargetPage { get; set; }

        /// <summary>
        /// Navigation parameter
        /// </summary>
        public object Parameter { get; set; }
    }
}
