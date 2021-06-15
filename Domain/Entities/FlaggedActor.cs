using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class FlaggedActor
    {
        public long ReportId { get; set; }
        public Report Report { get; set; } = null!;
        public int Id { get; set; }
        
        public Actor Actor { get; set; } = null!;
        public int ActorId { get; set; }
        public int? Flags { get; set; }
        
        [NotMapped]
        public ICollection<Line> LinesSource { get; set; } = new List<Line>();
        [NotMapped]
        public ICollection<Line> LinesTarget { get; set; } = new List<Line>();

    }
}