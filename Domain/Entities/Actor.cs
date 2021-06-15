using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Actor
    {
        public long ReportId { get; set; }
        public Report Report { get; set; } = null!;
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;

        public string Clazz { get; set; } = null!;
        public long Guid { get; set; } // unchecked((ulong)-1076357078391455527)
        public string Type { get; set; } = null!;
        
        
        [NotMapped]
        public ICollection<FlaggedActor> FlaggedActors { get; set; } = new List<FlaggedActor>();

    }
}