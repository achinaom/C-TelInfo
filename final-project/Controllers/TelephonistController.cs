using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataObject;
using BL;

namespace final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelephonistController : ControllerBase
    {
        ITelephonistBL _telephonistBL;
        ICompaniesBL _companiesBL;

        public TelephonistController(ITelephonistBL telephonistBL,ICompaniesBL companiesBL)
        {
            _companiesBL = companiesBL;
            _telephonistBL = telephonistBL;
        }

        [Route("{upate_telephonist}")]
        [HttpPut]
        public IActionResult upate_telephonist(TelephonistInCompanyDetails telephonistInCompanyDetails)
        {
            try
            {
                _telephonistBL.update_telephonist(telephonistInCompanyDetails);
                return Ok(true);
            }
            catch (Exception)
            {
                return NotFound(false);
            }

        }
        [HttpGet("{password}/{mail}")]
        public ActionResult<TelephonistCompaniesDTO> GetTelephonist(string password, string mail)
        {
            TelephonistCompaniesDTO telephonist = _telephonistBL.GetTelephonist(mail,password);
            if (telephonist != null)
                return telephonist;
            else
                return NotFound();
        }



        [Route("addtelephonist/{idCompany}/{password}")]
        [HttpPost]
        public ActionResult<List<TelephonistInCompanyDetails>> addtelephonist( int idCompany, String password, TelephonistDTO telephonistDTO)
        {
            try
            {
                _telephonistBL.Add_telephonnist(telephonistDTO,idCompany,password);
                return _companiesBL.getAlltelephonistInCimpany(idCompany); ;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
    }
}
