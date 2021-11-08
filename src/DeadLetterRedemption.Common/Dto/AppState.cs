namespace DeadLetterRedemption.Common.Dto
{
    public record AppState
    {
        public int DeadLetterCountTotal { get; init; }
        public int InFlightCountTotal { get; init; }
        public int SuccessfulRequeueCountTotal { get; init; }
    }
}
