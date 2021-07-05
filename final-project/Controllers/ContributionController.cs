using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL;
using DataObject;
namespace final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionController : ControllerBase
    {

        IContributionBL _ContributionBL;

        public ContributionController(IContributionBL ContributionBL)
        {
            _ContributionBL = ContributionBL;
        }

        [Route("count_Contribution_for_day/{id}")]

        [HttpGet]
        public ActionResult<int> count_Contribution_for_day(int id)
        {

            int count = _ContributionBL.count_Contribution_for_day(id);
            if (count != null)
                return count;
            else
                return NotFound();
        }

        [Route("count_Contribution_for_month/{id}")]

        [HttpGet]
        public ActionResult<int> count_Contribution_for_month(int id)
        {

            int count = _ContributionBL.count_Contribution_for_month(id);
            if (count != null)
                return count;
            else
                return NotFound();
        }


        [Route("sum_Contribution_for_month/{id}")]
        [HttpGet]
        public ActionResult<int> sum_Contribution_for_month(int id)
        {

            int count = _ContributionBL.sum_Contribution_for_month(id);
            if (count != null)
                return count;
            else
                return NotFound();
        }


        [Route("sum_Contribution_for_day/{id}")]
        [HttpGet]
        public ActionResult<int> sum_Contribution_for_day(int id)
        {

            int count = _ContributionBL.sum_Contribution_for_day(id);
            if (count != null)
                return count;
            else
                return NotFound();
        }



             [Route("get_contribution_for_phone/{id_phone}/{id_telephonist}")]
        [HttpGet]
        public ActionResult<List<ContributionDTO>> get_contribution_for_phone(int id_phone,int id_telephonist)
        {

            try
            {
                List<ContributionDTO> contributionDTOs = _ContributionBL.get_contribution_for_phone(id_telephonist,id_phone);
                return contributionDTOs;
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        //החזרת כל התרומות של חברה מסוימת
        //מקבל מספר חברה
        [Route("get_contribution_for_company/{id_company}")]
        [HttpGet]
        public ActionResult<List<ContributionDTO>> get_contribution_for_company(int id_company)
        {

            try
            {
                List<ContributionDTO> contributionDTOs = _ContributionBL.get_contribution_for_company(id_company);
                return contributionDTOs;
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //החזרת כל התרומות של טלפנית מסוימת
        //מקבל מספר טלפנית בחברה
        [Route("All_Contribution_for_Telephonist/{id_TelephonistInCompany}")]
        [HttpGet]
        public ActionResult<List<ContributionDTO>> All_Contribution_for_Telephonist(int id_TelephonistInCompany)
        {

            try
            {
                List<ContributionDTO> contributionDTOs = _ContributionBL.All_Contribution_for_Telephonist(id_TelephonistInCompany);
                return contributionDTOs;
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [Route("All_Contribution_for_TelephonistForMonth/{id_TelephonistInCompany}")]
        [HttpGet]
        public ActionResult<List<ContributionDTO>> All_Contribution_for_TelephonistForMonth(int id_TelephonistInCompany)
        {

            try
            {
                List<ContributionDTO> contributionDTOs = _ContributionBL.All_Contribution_for_TelephonistForMonth(id_TelephonistInCompany);
                return contributionDTOs;
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [Route("All_Contribution_for_TelephonistForYear/{id_TelephonistInCompany}")]
        [HttpGet]

        public ActionResult<List<ContributionDTO>> All_Contribution_for_TelephonistForYear(int id_TelephonistInCompany)
        {

            try
            {
                List<ContributionDTO> contributionDTOs = _ContributionBL.All_Contribution_for_TelephonistForYear(id_TelephonistInCompany);
                return contributionDTOs;
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [Route("All_Contribution_for_TelephonistForyDay/{id_TelephonistInCompany}")]
        [HttpGet]
        public ActionResult<List<ContributionDTO>> All_Contribution_for_TelephonistForyDay(int id_TelephonistInCompany)
        {

            try
            {
                List<ContributionDTO> contributionDTOs = _ContributionBL.All_Contribution_for_TelephonistForyDay(id_TelephonistInCompany);
                return contributionDTOs;
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        //[Route("add_telephonist/{contributionDTO}")]
        [HttpPost]
        public ActionResult<List<ContributionDTO>> new_contribute([FromBody] ContributionDTO new_contribute)
        {
            try
            {
                return _ContributionBL.add_contribute(new_contribute);
            }
            catch (Exception e)
            {

                return NotFound();
            }

        }


      
    }
   
}
