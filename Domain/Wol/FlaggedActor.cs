// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnassignedField.Global
namespace Domain.Wol
{
    /// <summary>
    /// actor state, raid icon for example
    /// </summary>
    public class FlaggedActor
    {
        /// <summary>
        /// <see cref="Actor.id"/>
        /// </summary>
        public int aid;
        public int flags;

        /// <summary>
        /// PK
        /// </summary>
        public int id;
        /// <summary>
        /// <see cref="UnifiedId.id"/>, PK over two properties ?
        /// </summary>
        // public int Uaid
        /// <summary>
        /// <see cref="UnifiedId.uid"/>
        /// </summary>
        // public int Uid
    }
}