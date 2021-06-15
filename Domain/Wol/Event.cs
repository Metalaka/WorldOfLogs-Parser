// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnassignedField.Global
namespace Domain.Wol
{
    public class Event
    {
        public int id;
        
        public int powerType;
        public int? spell;
        public int? extraSpell;
        public int subType;
        public int type;
        public int? school;

        public int? amount;
        public int? flags;
        public int? blocked;
        public int? overheal;
        public int? overkill;
        public int? absorbed;
        public int? resisted;
        public int? missType;
        public int? missAmount;
        public int? environmentType;
    }
}