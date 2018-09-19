using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Spartan.Models
{
    [DataContract]
    public class ClientEquipmentResult
    {
        [DataMember]
        public int ItemNumber { get; set; }
        [DataMember]
        public int UnitNumber { get; set; }
        [DataMember]
        public string Description { get; set; }

        public ClientEquipmentResult() { }

        public ClientEquipmentResult(int itemNumber, int unitNumber, string description)
        {
            ItemNumber = itemNumber;
            UnitNumber = unitNumber;
            Description = description;
        }

    }
}