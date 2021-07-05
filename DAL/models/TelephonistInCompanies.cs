using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class TelephonistInCompanies
    {
        public TelephonistInCompanies()
        {
            Calls = new HashSet<Calls>();
            Contribution = new HashSet<Contribution>();
        }

        public int Id { get; set; }
        public int? IdTelephonist { get; set; }
        public string Password { get; set; }
        public int? IdCompany { get; set; }

        public virtual Companies IdCompanyNavigation { get; set; }
        public virtual ICollection<Calls> Calls { get; set; }
        public virtual ICollection<Contribution> Contribution { get; set; }
    }
}
