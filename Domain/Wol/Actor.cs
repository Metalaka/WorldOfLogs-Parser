// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnassignedField.Global
namespace Domain.Wol
{
    public class Actor
    {
        public string name = null!;
        public string clazz = null!;
        public string GUIDString = null!;
        public string type = null!;

        /// <summary>
        /// PK
        /// </summary>
        public int id;
        /// <summary>
        /// <see cref="UnifiedId.uid"/>
        /// </summary>
        // public int Uid
    }
}