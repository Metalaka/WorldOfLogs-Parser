using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        
        [NotMapped]
        public IEnumerable<Spell> Spells { get; set; } = null!;
    }
}