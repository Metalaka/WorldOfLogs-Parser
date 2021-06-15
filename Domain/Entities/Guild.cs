using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Guild
    {
        public long Id { get; set; }
        
        public string Timezone { get; set; } = null!;
        public string Realm { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Faction { get; set; } = null!;
        
        [NotMapped]
        public ICollection<Report> Reports { get; set; } = new List<Report>();

    }
}