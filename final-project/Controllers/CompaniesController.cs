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
    public class CompaniesController : ControllerBase
    {
        ICompaniesBL _ICompaniesBL;

        public CompaniesController(ICompaniesBL companiesBL)
        {
            _ICompaniesBL = companiesBL;
        }
        [Route("GetTelephonistByID/{id}")]
        [HttpGet]
        public ActionResult<List<TelephonistInCompanyDetails>> GetTelephonistByID(int id)
        {
            List<TelephonistInCompanyDetails> telephonistInCompanyDetails = _ICompaniesBL.getAlltelephonistInCimpany(id);
            if (telephonistInCompanyDetails != null)
                return telephonistInCompanyDetails;
            else
                return NotFound();
        }
        [Route("getAllPhoneForCompany/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getAllPhoneForCompany(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_NumbersDTOs = _ICompaniesBL.getAllPhoneForCompany(id);
                return phone_NumbersDTOs;
            }
            catch
            {
                return NotFound();
            }
        }
        [Route("getAllCompanies")]
        [HttpGet]
        public ActionResult<List<CompaniesDTO>> getAllCompanies()

        {
            try
            {
                List<CompaniesDTO> companies = _ICompaniesBL.GetAllCompany();
                return companies;
            }
            catch
            {
                return NotFound();
            }
        }
        [Route("addCompanies")]
        [HttpPost]
        public ActionResult<List<CompaniesDTO>> addCompanies(CompaniesDTO company)

        {
            try
            {
                _ICompaniesBL.AddCompany(company);
                return getAllCompanies();
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("UpdateCompanies")]
        [HttpPut]
        public ActionResult<List<CompaniesDTO>> UpdateCompanies(CompaniesDTO company)
        {
            try
            {
                _ICompaniesBL.UpdateCompany(company);
                return getAllCompanies();
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
