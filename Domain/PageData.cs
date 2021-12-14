namespace Domain
{
    using Domain.Wol;

    public record PageData
    {
        public int Page { get; set; }
        public Guild? Guild { get; set; }
        public Report? Report { get; set; }
        public CombatLog CombatLog { get; set; }
        public SimpleQuery SimpleQuery { get; set; }
    }
}
