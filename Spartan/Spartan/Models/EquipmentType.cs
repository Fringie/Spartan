using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spartan.Models
{
    public class EquipmentType
    {
        public Guid Id { get; set; }
        public int ExternalId { get; set; }
        public string Description { get; set; }
    }
}