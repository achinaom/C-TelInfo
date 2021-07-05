﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public class TelephonistDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Mail { get; set; }
        public DateTime? DateBirth { get; set; }
        public int? FamilyStatus { get; set; }

    }
}
