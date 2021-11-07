namespace DeadLetterRedemption.Common.EventArgs
{
    /// <summary>
    /// Message received argument class
    /// </summary>
    public class MessageReceivedEventArgs : System.EventArgs
    {
        public MessageReceivedEventArgs(string username, string message)
        {
            Username = username;
            Message = message;
        }

        /// <summary>
        /// Name of the message/event
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Message data items
        /// </summary>
        public string Message { get; set; }
    }
}
