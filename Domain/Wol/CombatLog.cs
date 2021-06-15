using System.Collections.Generic;
// ReSharper disable InconsistentNaming
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnassignedField.Global

namespace Domain.Wol
{
    public class CombatLog
    {
        public IEnumerable<Actor> actors = null!;
        /// <summary>
        /// log lines
        ///
        /// [0] line id
        /// [1] line timestamp
        /// [2] sourceActor <see cref="FlaggedActor.id"/> or -1
        /// [3] targetActor <see cref="FlaggedActor.id"/> or -1
        /// [4] event <see cref="Event.id"/>
        /// </summary>
        public IEnumerable<int[]> entries = null!;
        public IEnumerable<Event> events = null!;
        public IEnumerable<FlaggedActor> flaggedActors = null!;
        public IEnumerable<Spell> spells = null!;
        // public IEnumerable<UnifiedId> uids = null!;
    }
}