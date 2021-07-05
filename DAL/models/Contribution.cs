using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class Contribution
    {
        public int Id { get; set; }
        public int? SumContribution { get; set; }
        public int? IdTelephonistCompany { get; set; }
        public int? IdPhone { get; set; }
        public DateTime? DateC { get; set; }

        public virtual PhoneNumbers IdPhoneNavigation { get; set; }
        public virtual TelephonistInCompanies IdTelephonistCompanyNavigation { get; set; }
    }
}
