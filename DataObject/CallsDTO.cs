using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
   public class CallsDTO
    {
        public Nullable<int> Id { get; set; }
        public int IdPhoneNumber { get; set; }
        public int IdTelephonist { get; set; }
        public Nullable<DateTime> DateCall { get; set; }
        public Nullable<DateTime> TimeCall { get; set; }
        public string? TranscriptCall { get; set; }
        public int? Done { get; set; }



    }
}
