using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DataObject;
namespace project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class uplodeController : ControllerBase
    {
        IPhone_numbersBL _phone_NumbersBL;
        Mapper iMapper;
        //העלאת אקסל

        public uplodeController( IPhone_numbersBL Phone_numberBL)
        {
            _phone_NumbersBL = Phone_numberBL;
        }



        [Route("UploadExcel/{id}")]
        [HttpPost, DisableRequestSizeLimit]
        public ActionResult<List<LeadsAndCallsDTO>> UploadExcel(int id)
        {


            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot", "lead");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return(_phone_NumbersBL.addXL(id, fullPath));
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
            //TeachersBL tb = new TeachersBL();
        }
    }
}

