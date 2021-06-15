using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Event
    {
        public long ReportId { get; set; }
        public Report Report { get; set; } = null!;
        public int Id { get; set; }

        public int PowerType { get; set; }
        public Spell? Spell { get; set; }
        public Spell? ExtraSpell { get; set; }
        public int SubType { get; set; }
        public int Type { get; set; }
        public int School { get; set; }

        public int? Amount { get; set; }
        public int? Flags { get; set; }
        public int? MissAmount { get; set; }
        public int? Blocked { get; set; }
        public int? Overheal { get; set; }
        public int? Overkill { get; set; }
        public int? Absorbed { get; set; }
        public int? Resisted { get; set; }
        public int? MissType { get; set; }
        public int? EnvironmentType { get; set; }
        
        // version ?
        // public int? RemainingPoints { get; set; }
        // public int? EncounterId { get; set; }
        // public int? EncounterName { get; set; }
        // public int? EncounterDifficulty { get; set; }
        // public int? EncounterPlayerCount { get; set; }
        // public int? EncounterSuccess { get; set; }

        [NotMapped]
        public ICollection<Line> Lines { get; set; } = new List<Line>();

    }
}