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
    public class TelephonistCompaniesController : ControllerBase
    {
        ITelephonistCompaniesBL _TelephonistCompaniestBL;
        ICompaniesBL _companiesBL;
        public TelephonistCompaniesController(ITelephonistCompaniesBL TelephonistCompaniesBL, ICompaniesBL companiesBL)
        {

            _TelephonistCompaniestBL = TelephonistCompaniesBL;
            _companiesBL = companiesBL;
        }
        [HttpGet]
        public List<TelephonistCompaniesDTO> GetTelephonist(string password, string mail)
        {
            List<TelephonistCompaniesDTO> TelephonistCompanies = _TelephonistCompaniestBL.GetTelephonistCompanies();
            return TelephonistCompanies;
        }
        ////עדכון טלפנית
        //[Route("GetAllTelephonistInCompanyByID/{id}")]
        //[HttpGet]
        //public ActionResult<List<TelephonistInCompanyDetails>> GetTelephonistByID(int id)
        //{
        //    List<TelephonistInCompanyDetails> telephonistInCompanyDetails = _ICompaniesBL.getAlltelephonistInCimpany(id);
        //    if (telephonistInCompanyDetails != null)
        //        return telephonistInCompanyDetails;
        //    else
        //        return NotFound();
        //}
        [Route("DeleteTelephonistInCompanies/{id}")]
        [HttpDelete("{id}")]
        public IActionResult DeleteTelephonistInCompanies(int id)
        {
            int flag = _TelephonistCompaniestBL.GetIdCompaniesByIdTelephonist(id);

            try
            {
                _TelephonistCompaniestBL.DeleteTelephonistCompanies(id);
                return Ok(_companiesBL.getAlltelephonistInCimpany(flag));
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        [Route("GetTelephonistCompaniesById/{id}")]
        [HttpGet]
        public List<TelephonistCompaniesDTO> GetTelephonistCompaniesById(int id)
        {
            List<TelephonistCompaniesDTO> TelephonistCompanies = _TelephonistCompaniestBL.GetTelephonistCompaniesById(id);
            return TelephonistCompanies;
        }
        //החזרת טלפנית לפי מספר טלפנית בחברה
        [Route("GetTelephonistByIdTelephonistCompany/{id}")]
        [HttpGet]
        public ActionResult<TelephonistDTO> GetTelephonistByIdTelephonistCompany(int id)
        {
            try
            {
                TelephonistDTO telephonistDTO = _TelephonistCompaniestBL.GetTelephonistByIdTelephonist(id);
                return telephonistDTO;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        //החזרת טלפנית לפי מספר טלפנית בחברה
        [Route("GetTelephonistinCompanyByIdTelephonistInCopmany/{id}")]
        [HttpGet]
        public ActionResult<TelephonistCompaniesDTO> GetTelephonistinCompanyByIdTelephonistInCopmany(int id)
        {
            try
            {
                TelephonistCompaniesDTO telephonistCompaniesDTO = _TelephonistCompaniestBL.GetTelephonistinCompanyByIdTelephonistInCopmany(id);
                return telephonistCompaniesDTO;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }
    }
}
