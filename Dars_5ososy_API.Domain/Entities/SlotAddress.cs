using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Dars_5ososy_API.Domain.Entities
{
    public class SlotAddress
    {
        public long Id { get; set; }
        public long AvailabilitySlotId { get; set; }
        [ForeignKey(nameof(AvailabilitySlotId))]
        public AvailabilitySlot AvailabilitySlot { get; set; }

        public string Address { get; set; }

        public long AreaId { get; set; }
        [ForeignKey(nameof(AreaId))]
        public Area Area { get; set; }
    }
}
