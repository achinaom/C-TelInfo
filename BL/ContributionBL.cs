using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DataObject;
using AutoMapper;
using System.Linq;
using DAL.models;

namespace BL
{
    public class ContributionBL : IContributionBL
    {

        IContributionDAL _ContributionDAL;
        ICallsBL _callsBL;
        ITelephonistCompaniesDAL _telephonistCompaniesDAL;

        IMapper iMapper;
        public ContributionBL(IContributionDAL ContributionDAL,ICallsBL callsBL,ITelephonistCompaniesDAL telephonistCompaniesDAL)
        {
            _telephonistCompaniesDAL = telephonistCompaniesDAL;
            _callsBL = callsBL;
            _ContributionDAL = ContributionDAL;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            iMapper = config.CreateMapper();
        }
        //כמות התרומות לחודש זה
        public int count_Contribution_for_month(int id)
        {
            List<Contribution> ContributionList = _ContributionDAL.GetContribution();
            int count = ContributionList.Where(x => x.IdTelephonistCompany == id && x.DateC.Value.Month == DateTime.Today.Month).Count();
            return count;
        }
        //כמות השיחות ליום
        public int count_Contribution_for_day(int id)
        {
            List<Contribution> ContributionList = _ContributionDAL.GetContribution();
            int count = ContributionList.Where(x => x.IdTelephonistCompany == id && x.DateC.Value.Date == DateTime.Today.Date).Count();
            return count;
        }
        //סך התרומות ליום
        public int sum_Contribution_for_day(int id)
        {
            List<Contribution> ContributionList = _ContributionDAL.GetContribution();
            int count = (int)ContributionList.Where(x => x.IdTelephonistCompany == id && x.DateC.Value.Date == DateTime.Today.Date).Select(x => x.SumContribution).Sum();
            return count;
        }
        //סך התרומות לחודש
        public int sum_Contribution_for_month(int id)
        {
            List<Contribution> ContributionList = _ContributionDAL.GetContribution();
            int count = (int)ContributionList.Where(x => x.IdTelephonistCompany == id && x.DateC.Value.Month == DateTime.Today.Month).Select(x => x.SumContribution).Sum();
            return count;
        }
        //החזרת כל התרומות של טלפנית מסויימת לפי איש קשר מסויים
        public List<ContributionDTO> get_contribution_for_phone(int id_telephonist, int id_phone)
        {
            List<Contribution> ContributionList = _ContributionDAL.GetContribution();
            List<Contribution> contributionss = ContributionList.Where(x => x.IdTelephonistCompany == id_telephonist && x.IdPhone == id_phone).ToList();
            List<ContributionDTO> contributionDTOs = iMapper.Map<List<Contribution>, List<ContributionDTO>>(contributionss);
            return contributionDTOs;
        }
        //החזרת כל התרומות של חברה מסויימת של כל הטלפניות
        public List<ContributionDTO> get_contribution_for_company(int id_company)
        {
            List<Contribution> ContributionList = _ContributionDAL.GetContribution();
            List<Contribution> ContributionList1 = new List<Contribution> ();
            List<TelephonistInCompanies> telephonistInCompanies = new List<TelephonistInCompanies>();
            telephonistInCompanies = _telephonistCompaniesDAL.GetTelephonistInCompanies();
            Boolean degel;
            foreach (var item in ContributionList)
            {
                foreach (var item1 in telephonistInCompanies)
                {
                    if(item1.Id == item.IdTelephonistCompany && item1.IdCompany == id_company)
                        ContributionList1.Add(item);

                }
                //if (degel = telephonistInCompanies.Any(x => x.IdTelephonist == item.IdTelephonistCompany && x.IdCompany == id_company))
                //    ContributionList1.Add(item);
            }
            List<ContributionDTO> ContributionDTOList = iMapper.Map<List<Contribution>, List<ContributionDTO>>(ContributionList1);
            return ContributionDTOList;
        }
        //החזרת כל התרומות של טלפנית מסויימת 
        public List<ContributionDTO> All_Contribution_for_Telephonist(int id)
        {
            List<Contribution> ContributionList = _ContributionDAL.GetContribution().Where(x => x.IdTelephonistCompany == id).ToList();
            List<ContributionDTO> ContributionDTOList = iMapper.Map<List<Contribution>, List<ContributionDTO>>(ContributionList);
            return ContributionDTOList;
        }
        public List<ContributionDTO> All_Contribution_for_TelephonistForYear(int id)
        {
            DateTime date = DateTime.UtcNow;
            date.AddHours(3);
            List<Contribution> ContributionList = _ContributionDAL.GetContribution().Where(x => x.IdTelephonistCompany == id && x.DateC.Value.Year == date.Year).ToList();
            List<ContributionDTO> ContributionDTOList = iMapper.Map<List<Contribution>, List<ContributionDTO>>(ContributionList);
            return ContributionDTOList;
        }
        public List<ContributionDTO> All_Contribution_for_TelephonistForyDay(int id)
        {                                                                                                   
            DateTime date = DateTime.UtcNow;
            date.AddHours(3);
            List<Contribution> ContributionList = _ContributionDAL.GetContribution().Where(x => x.IdTelephonistCompany == id&&x.DateC.Value.Date==date.Date).ToList();
            List<ContributionDTO> ContributionDTOList = iMapper.Map<List<Contribution>, List<ContributionDTO>>(ContributionList);
            return ContributionDTOList;
        }
        public List<ContributionDTO> All_Contribution_for_TelephonistForMonth(int id)
        {
            DateTime date = DateTime.UtcNow;
            date.AddHours(3);
            List<Contribution> ContributionList = _ContributionDAL.GetContribution().Where(x => x.IdTelephonistCompany == id&& x.DateC.Value.Month == date.Month && x.DateC.Value.Year == date.Year).ToList();
            List<ContributionDTO> ContributionDTOList = iMapper.Map<List<Contribution>, List<ContributionDTO>>(ContributionList);
            return ContributionDTOList;
        }
        //הוספת תרומה
        public List<ContributionDTO> add_contribute(ContributionDTO contributionDTO)
        {
            Contribution contribution = iMapper.Map<ContributionDTO, Contribution>(contributionDTO);
            contribution.SumContribution = contributionDTO.sum_contribution;

            _ContributionDAL.AddContribution(contribution);
            List<Contribution> contributions = _ContributionDAL.GetContribution();
            List<ContributionDTO> contributionDTOs = iMapper.Map<List<Contribution>, List<ContributionDTO>>(contributions);
            List<ContributionDTO> contributionDTO1 = contributionDTOs.Where(x => x.idTelephonistCompany == contributionDTO.idTelephonistCompany && x.idPhone == contribution.IdPhone).ToList();
            CallsDTO calls = new CallsDTO();
            if (contribution.SumContribution > 3000)
            {
                calls.DateCall = DateTime.UtcNow.AddYears(1);
                calls.TimeCall = DateTime.UtcNow;
            }
          else  if (contribution.SumContribution > 1000)
            {
                calls.DateCall = DateTime.UtcNow.AddMonths(6);
                calls.TimeCall = DateTime.UtcNow;
            }
            else
            {
                calls.DateCall = DateTime.UtcNow.AddHours(3).AddMonths(3);
                calls.TimeCall = DateTime.UtcNow;
            }
            calls.TranscriptCall = null;
            calls.IdTelephonist =(int) contribution.IdTelephonistCompany;
            calls.IdPhoneNumber =(int) contribution.IdPhone;
            calls.Done = 0;
            List<CallsDTO> callsDTOs = _callsBL.Add_call_for_telephonnist(calls);


            return contributionDTO1;
        }
    }
}
