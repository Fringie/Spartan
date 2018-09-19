using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spartan.Models
{
    public class Equipment
    {
        public List<SerialisedEquipment> SerialisedEquipment { get; set; }
        public IList<EquipmentType> EquipmentType { get; set; }
    }
}