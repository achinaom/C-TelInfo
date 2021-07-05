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
    public class Text_decodingController
    {
        [Route("Get_Call_By_transciption/{transcript}")]
        [HttpGet]
        public ActionResult<Respons> Get_Call_By_transciption(string transcript)
        {
            try
            {
                Text_Decoding td = new Text_Decoding();
                Respons respons = td.Decode(transcript);
                return respons;
            }
            catch (Exception)
            {
                Respons respons = null;
                return respons;
            }
        }
    }
}
