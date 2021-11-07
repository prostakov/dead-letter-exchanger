namespace DeadLetterRedemption.Common
{
    /// <summary>
    /// Stores shared names used in both client and hub
    /// </summary>
    public static class MessageTypes
    {
        /// <summary>
        /// Event name when a AppState changed
        /// </summary>
        public const string AppStateChange = "AppStateChange";
        
        /// <summary>
        /// Event name when a message is received
        /// </summary>
        public const string Receive = "ReceiveMessage";

        /// <summary>
        /// Name of the Hub method to register a new user
        /// </summary>
        public const string Register = "Register";

        /// <summary>
        /// Name of the Hub method to send a message
        /// </summary>
        public const string Send = "SendMessage";

    }
}
