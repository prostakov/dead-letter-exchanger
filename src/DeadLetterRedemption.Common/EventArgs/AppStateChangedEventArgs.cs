namespace DeadLetterRedemption.Common.EventArgs
{
    public class AppStateChangedEventArgs : System.EventArgs
    {
        public AppState AppState { get; }
        
        public AppStateChangedEventArgs(AppState appState)
        {
            AppState = appState;
        }
    }
}
