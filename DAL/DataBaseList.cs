using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.models;

namespace DAL
{
    public class DataBaseList
    {
        public static List<Calls> callList;
        public static List<Companies> CompaniesList;
        public static List<Contribution> ContributionList;
        public static List<PhoneNumbers> PhoneNumbersList;
        public static List<Telephonist> TelephonistList;
        public static List<TelephonistInCompanies> TelephonistInCompaniesList;

        public DataBaseList()
        {

            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                callList = context.Calls.ToList();
                CompaniesList = context.Companies.ToList();
                ContributionList = context.Contribution.ToList();
                PhoneNumbersList = context.PhoneNumbers.ToList();
                TelephonistList = context.Telephonist.ToList();
                TelephonistInCompaniesList = context.TelephonistInCompanies.ToList();

            }
        }

    }
}
