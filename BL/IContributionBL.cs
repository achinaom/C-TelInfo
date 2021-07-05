using System;
using System.Collections.Generic;
using System.Text;
using DataObject;
using DAL.models;
namespace BL
{
    public interface IContributionBL
    {
        public int count_Contribution_for_month(int id);
        public int count_Contribution_for_day(int id);
        public int sum_Contribution_for_day(int id);
        public int sum_Contribution_for_month(int id);
        public List<ContributionDTO> get_contribution_for_phone(int id_telephonist, int id_phone);

        //החזרת כל התרומות של חברה מסויימת של כל הטלפניות
        public List<ContributionDTO> get_contribution_for_company(int id_company);
        //החזרת כל התרומות של טלפנית מסויימת 
        public List<ContributionDTO> All_Contribution_for_Telephonist(int id);
        public List<ContributionDTO> add_contribute(ContributionDTO contributionDTO);
        public List<ContributionDTO> All_Contribution_for_TelephonistForMonth(int id);
        public List<ContributionDTO> All_Contribution_for_TelephonistForyDay(int id);
        public List<ContributionDTO> All_Contribution_for_TelephonistForYear(int id);

        }
}
