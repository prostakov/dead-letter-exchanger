namespace DeadLetterRedemption.Common
{
    public record AppState
    {
        public int DeadLetterCountTotal { get; init; }
        public int InFlightCountTotal { get; init; }
        public int SuccessfulRequeueCountTotal { get; init; }
    }
}
