namespace Domain.Entities
{
    public class Line
    {
        public long ReportId { get; set; }
        public Report Report { get; set; } = null!;
        public int Id { get; set; }
        
        public long Timestamp { get; set; }
        
        public FlaggedActor? SourceFlaggedActor { get; set; }
        public int? SourceFlaggedActorId { get; set; }
        public FlaggedActor? TargetFlaggedActor { get; set; }
        public int? TargetFlaggedActorId { get; set; }
        
        public Event Event { get; set; } = null!;
        public int EventId { get; set; }
    }
}