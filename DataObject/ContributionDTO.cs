using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public class ContributionDTO
    {
        public int Id { get; set; }
        public int sum_contribution { get; set; }
        public int idTelephonistCompany { get; set; }
        public int idPhone { get; set; }
        public DateTime? dateC { get; set; }
    }
}
