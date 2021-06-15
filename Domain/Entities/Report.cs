using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Report
    {
        /// <summary>
        /// technical id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// report id
        /// </summary>
        public string Sid { get; set; } = null!;
        public long E { get; set; }
        public long S { get; set; }
        public long EndTime { get; set; }
        public long ExportDate { get; set; }
        public bool HasForbiddenBuffs { get; set; }
        public long StartTime { get; set; }
        
        public long? TimeInfoE { get; set; }
        public long? TimeInfoS { get; set; }
        
        public Guild? Guild { get; set; }

        // actors
        // flagged actors
        // entries
        // events
        // spells
        [NotMapped]
        public ICollection<Line> Lines { get; set; } = new List<Line>();
        
        [NotMapped]
        public IDictionary<int, Actor> Actors { get; } = new Dictionary<int, Actor>();
        [NotMapped]
        public IDictionary<int, FlaggedActor> FlaggedActors { get; } = new Dictionary<int, FlaggedActor>();
        [NotMapped]
        public IDictionary<int, Event> Events { get; } = new Dictionary<int, Event>();

        // customs

        // public string? Comment { get; set; }

    }
}