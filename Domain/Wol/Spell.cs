using System.Collections.Generic;
// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnassignedField.Global

namespace Domain.Wol
{
    public class Spell
    {
        public IEnumerable<string> schools = null!;
        public int school;

        /// <summary>
        /// spell id
        /// </summary>
        public int id;
        /// <summary>
        /// Name in the locale of the uploader
        /// </summary>
        public string name = null!;
    }
}