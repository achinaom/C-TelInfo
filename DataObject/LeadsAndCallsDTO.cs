using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
   public class LeadsAndCallsDTO
    {
        public int leadId { get; set; }
        public string leadPhoneNumber { get; set; }
        public string leadFirstName { get; set; }
        public string leadLastName { get; set; }
        public string leadCity { get; set; }
        public DateTime? leadCreationDate { get; set; }

        public string leadTz { get; set; }
        public string leadAddress { get; set; }
        public string leadMail { get; set; }
        public int? leadStatus { get; set; }
        public int? leadType{ get; set; }
        public string ?leadPhone2 { get; set; }
        public string leadPlaceWorking { get; set; }

        public int IdCall { get; set; }
        public int IdTelephonist_c { get; set; }

        public DateTime? DateCall { get; set; }
        public DateTime? TimeCall { get; set; }
        public int countcall { get; set; }
        public int? Done { get; set; }

    }
}
