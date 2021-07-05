using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class PhoneNumbers
    {
        public PhoneNumbers()
        {
            Calls = new HashSet<Calls>();
            Contribution = new HashSet<Contribution>();
        }

        public int Id { get; set; }
        public string Phone { get; set; }
        public string Tz { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string Mail { get; set; }
        public int? Status { get; set; }
        public int? IdCompanies { get; set; }
        public int? Type { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public DateTime? CreationDate { get; set; }
        public string Phone2 { get; set; }
        public string PlaceWorking { get; set; }
        public string Mikud { get; set; }

        public virtual Companies IdCompaniesNavigation { get; set; }
        public virtual ICollection<Calls> Calls { get; set; }
        public virtual ICollection<Contribution> Contribution { get; set; }
    }
}
