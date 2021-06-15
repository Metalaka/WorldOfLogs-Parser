using System.Collections.Generic;
// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnassignedField.Global

namespace Domain.Wol
{
    public class SimpleQuery
    {
        public IEnumerable<int> lines = null!;
        public int page;
    }
}