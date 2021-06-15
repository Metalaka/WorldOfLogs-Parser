// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnassignedField.Global
namespace Domain.Wol
{
    public class Report
    {
        public long e;
        public long s;
        public long endTime;
        public long exportDate;
        public bool hasForbiddenBuffs;
        /// <summary>
        /// report id
        /// </summary>
        public string sid = null!;

        public long startTime;
        public TimeInfo timeInfo = null!;
    }
    public class TimeInfo
    {
        public long e;
        public long s;
    }
}