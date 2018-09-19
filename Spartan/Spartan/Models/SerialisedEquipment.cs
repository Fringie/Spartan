using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spartan.Models
{
    public class SerialisedEquipment
    {
        public Guid Id { get; set; }
        public int ExternalId { get; set; }
        public Guid EquipmentTypeId { get; set; }
        public int MeterReading { get; set; }
    }
}