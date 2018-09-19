using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Spartan.Models;

namespace Spartan.Controllers
{
    public class EquipmentController : ApiController
    {
        private Equipment _equipment;
        private ClientEquipmentResult[] _allRecords;

        public EquipmentController()
        {
            DeserializeModelData();
        }

        #region IHttpActionResults

        /// <summary>
        /// Gets all the equipment items stored in the json
        /// http://localhost:40033/api/equipment/getall
        /// </summary>
        /// <returns>Query results array</returns>
        public IHttpActionResult GetAll()
        {
            return Ok(_allRecords);
        }

        /// <summary>
        /// Gets all the equipment records that match the specified Unit Number
        /// </summary>
        /// <param name="id">This is passed in from the client side, the user should enter this value</param>
        /// <returns>Query results array</returns>
        public IHttpActionResult GetAllByUnitNumber(int id)
        {
            ClientEquipmentResult[] results = _allRecords.Where(i => i.UnitNumber == id).ToArray();
            return Ok(results);
        }

        /// <summary>
        /// Gets all the equipment records that match the specified Item Number
        /// </summary>
        /// <param name="id">This is passed in from the client side, the user should enter this value</param>
        /// <returns>Query results array</returns>
        public IHttpActionResult GetAllByItemNumber(int id)
        {
            ClientEquipmentResult[] results = _allRecords.Where(i => i.ItemNumber == id).ToArray();
            return Ok(results);
        }

        #endregion

        #region HelperMethods

        /// <summary>
        /// Gets the data from the JSON and deserializes it into a usable Model/Object
        /// </summary>
        private void DeserializeModelData()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + System.Web.Configuration.WebConfigurationManager.AppSettings["EquipmentJsonPath"];
            string json = System.IO.File.ReadAllText(path);
            _equipment = JsonConvert.DeserializeObject<Equipment>(json);
            _allRecords = JoinClientEquipmentResults(_equipment.SerialisedEquipment);
        }

        /// <summary>
        /// Get the relevant EquipmentType info
        /// </summary>
        /// <param name="equipmentTypeId">Relevant Id as taken from the SerialisedEquipment object</param>
        /// <returns>Data from the EquipmentType json</returns>
        private EquipmentType GetEquipmentTypeRecord(Guid equipmentTypeId)
        {
            return _equipment.EquipmentType.FirstOrDefault(e => e.Id == equipmentTypeId);
        }

        /// <summary>
        /// Joins the EquipmentType data to SerialisedEquipment query results
        /// </summary>
        /// <param name="serialisedEquipmentResults">This should be the result of a query ran on the SerialisedEquipment json</param>
        /// <returns>ClientEquipmentResult array which is ready to be sent to the client</returns>
        private ClientEquipmentResult[] JoinClientEquipmentResults(List<SerialisedEquipment> serialisedEquipmentResults)
        {
            // Create results object which will be sent back to the client
            ClientEquipmentResult[] results = new ClientEquipmentResult[_equipment.SerialisedEquipment.Count];
            for (var index = 0; index < serialisedEquipmentResults.Count; index++)
            {
                // Get data from both of the relevant json objects
                SerialisedEquipment equipmentRecord = serialisedEquipmentResults[index];
                EquipmentType equipmentTypeInfo = GetEquipmentTypeRecord(equipmentRecord.EquipmentTypeId);

                // Join the relevant data from both json objects into one single object to be passed to the client side
                results[index] = new ClientEquipmentResult(equipmentTypeInfo.ExternalId, equipmentRecord.ExternalId, equipmentTypeInfo.Description);
            }
            return results;
        }

        #endregion
    }
}
