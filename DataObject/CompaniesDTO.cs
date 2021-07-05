using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public class CompaniesDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string ManagerName { get; set; }
        public string ManagerTz { get; set; }
        public string ManagerPassword { get; set; }
        public string mail { get; set; }
    }
}
