using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class Calls
    {
        public int Id { get; set; }
        public int IdPhoneNumber { get; set; }
        public int IdTelephonist { get; set; }
        public DateTime  DateCall { get; set; }
        public DateTime TimeCall { get; set; }
        public string? TranscriptCall { get; set; }
        public int ? Done { get; set; }

        public virtual PhoneNumbers IdPhoneNumberNavigation { get; set; }
        public virtual TelephonistInCompanies IdTelephonistNavigation { get; set; }
    }
}
