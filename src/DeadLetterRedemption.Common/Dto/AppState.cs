namespace DeadLetterRedemption.Common.Dto
{
    public class AppState
    {
        public int DeadLetterCountTotal { get; set; }
        public int InFlightCountTotal { get; set; }
        public int SuccessfulRequeueCountTotal { get; set; }
    }
}
