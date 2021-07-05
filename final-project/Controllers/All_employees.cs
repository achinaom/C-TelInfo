using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL;
using BL;
using DataObject;
namespace final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class All_employees : ControllerBase
    {
        ITelephonistBL _telephonistBL;
        ICompaniesBL _CompaniesBL;

        public All_employees(ITelephonistBL telephonistBL,ICompaniesBL companiesBL)
        {
            _CompaniesBL = companiesBL;
            _telephonistBL = telephonistBL;
        }
        [Route("log_in/{password}/{mail}")]
        [HttpGet]
        public int log_in(string password, string mail)
        {
            TelephonistCompaniesDTO telephonist = _telephonistBL.GetTelephonist(mail, password);
            CompaniesDTO Companiey = _CompaniesBL.GetCompany(mail, password);
            if (telephonist.Id != 0)
                return 1;
            else if(Companiey.Id!=0)
                return 2;
            return 0;
        }

        public int changePass( string mail,string tz)
        {
           List<TelephonistCompaniesDTO>  telephonist = _telephonistBL.GetTelephonistByMailAndTz(mail, tz);
            CompaniesDTO Companiey = _CompaniesBL.GetCompanyByMailAndTz(mail, tz);
            if (telephonist!=null)
                return 1;
            else if (Companiey.Id != 0)
                return 2;
            return 0;
        }

        [Route("log_in_telephonist/{password}/{mail}")]
        [HttpGet]
        public ActionResult<TelephonistCompaniesDTO> log_in_telephonist(string password, string mail)
        {
            TelephonistCompaniesDTO telephonist = _telephonistBL.GetTelephonist(mail, password);
            if (telephonist != null)
                return telephonist;
            else
                return NotFound();
        }

        [Route("log_in_companey/{password}/{mail}")]
        [HttpGet]
        public ActionResult<CompaniesDTO> log_in_companey(string password, string mail)
        {
            CompaniesDTO Companiey = _CompaniesBL.GetCompany(mail, password);
            if (Companiey != null)
                return Companiey;
            else
                return NotFound();
        }


      
    }
}
