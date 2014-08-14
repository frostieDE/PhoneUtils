using System;

namespace PhoneUtils.Messages
{
    /// <summary>
    /// Base class for messages with callbacks
    /// for success and cancellation
    /// </summary>
    public abstract class CallbackMessageBase
    {
        /// <summary>
        /// Action to be called after success or confirmation
        /// </summary>
        public Action SuccessAction { get; set; }

        /// <summary>
        /// Action to be called after cancellation
        /// </summary>
        public Action CancelAction { get; set; }

        /// <summary>
        /// Calls SuccessAction if provided
        /// </summary>
        public void ExecuteSuccess()
        {
            if (SuccessAction != null)
            {
                SuccessAction();
            }
        }

        /// <summary>
        /// Calls CancelAction if provided
        /// </summary>
        public void ExecuteCancel()
        {
            if (CancelAction != null)
            {
                CancelAction();
            }
        }
    }
}
