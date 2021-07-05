using System;
using System.Collections.Generic;

namespace DAL.models
{
    public partial class Telephonist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public DateTime? DateBirth { get; set; }
        public int? FamilyStatus { get; set; }
        public string Tz { get; set; }
    }
}
