using System.Collections.Generic;

namespace Domain.Entities
{
    public class Spell
    {
        /// <summary>
        /// spell id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name in the locale of the uploader
        /// </summary>
        public string Name { get; set; } = null!;
        public IEnumerable<School> Schools { get; set; } = null!;
        // [Flags]
        // public int SchoolsMask { get; set; }
    }
}