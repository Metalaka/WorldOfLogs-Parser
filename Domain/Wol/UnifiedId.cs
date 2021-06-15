// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnassignedField.Global
namespace Domain.Wol
{
    /// <summary>
    /// Unified actor entry
    /// </summary>
    public class UnifiedId
    {
        public string name = null!;
        public string clazz = null!;
        public string GUIDString = null!;
        public string type = null!;

        /// <summary>
        /// <see cref="Actor.id"/> of one actor, the first ?
        /// </summary>
        public int id;

        /// <summary>
        /// PK
        /// </summary>
        public int uid;
    }
}