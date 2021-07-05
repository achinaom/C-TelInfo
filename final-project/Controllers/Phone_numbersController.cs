using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataObject;
using BL;
using System.IO;
using System.Text;
using DAL;
using AutoMapper;

namespace final_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Phone_numbersController : ControllerBase
    {
        IPhone_numbersBL _Phone_numbersBL;
        IMapper iMapper;
        IPhone_numbersDAL _NumbersDAL;
        ITelephonistCompaniesBL _telephonistCompaniesBL;
        ICallsBL _callsBL;
        public Phone_numbersController(ICallsBL callsBL, IPhone_numbersBL Phone_numberBL, IPhone_numbersDAL phone_NumbersDAL, ITelephonistCompaniesBL telephonistCompaniesBL)
        {
            _telephonistCompaniesBL = telephonistCompaniesBL;
            _Phone_numbersBL = Phone_numberBL;
            _NumbersDAL = phone_NumbersDAL;
            _callsBL = callsBL;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            iMapper = config.CreateMapper();
        }


        //[HttpGet("{id}")]
        //public ActionResult<List<Phone_numbersDTO>> get_Phone_numbers_for_today(int id)
        //{
        //    try
        //    {
        //        List<Phone_numbersDTO> Phone_number = _Phone_numbersBL.GetAll(id);
        //        return Phone_number;
        //    }
        //    catch (Exception)
        //    {
        //        return NotFound();
        //    }
        //}


        [Route("get_phone/{id}")]
        [HttpGet]
        public Phone_numbersDTO get_phone(int id)
        {

            Phone_numbersDTO phone_Numbers = _Phone_numbersBL.GetPhone_number(id);
            return phone_Numbers;

        }



        [Route("get_date_for_call/{transcript}/{id}")]
        [HttpGet]
        public Phone_numbersDTO get_phone(string transcript, int id)
        {

            Phone_numbersDTO phone_Numbers = _Phone_numbersBL.GetPhone_number(id);
            return phone_Numbers;

        }


        [Route("getLeadForTelephonist/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getLeadForTelephonist(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_Numbers = _Phone_numbersBL.getLeadForTelephonist(id);
                return phone_Numbers;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [Route("getLeadForMonthForTelephonist/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getLeadForMonthForTelephonist(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_Numbers = _Phone_numbersBL.getLeadForMonthForTelephonist(id);
                return phone_Numbers;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [Route("getLeadForDayForTelephonist/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getLeadForDayForTelephonist(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_Numbers = _Phone_numbersBL.getLeadForDayForTelephonist(id);
                return phone_Numbers;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [Route("getAllLeadForCompany/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getAllLeadForCompany(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_Numbers = _Phone_numbersBL.getAllLeadForCompany(id);
                return phone_Numbers;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        [Route("getLeadForMonthForCompany/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getLeadForMonthForCompany(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_Numbers = _Phone_numbersBL.getLeadForMonthForCompany(id);
                return phone_Numbers;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }


        [Route("Update_phone")]
        [HttpPut]
        public ActionResult<Phone_numbersDTO> Update_phone(Phone_numbersDTO phone)
        {
            try
            {
                Phone_numbersDTO phone_Number = _Phone_numbersBL.Update_phone(phone);
                return phone_Number;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
        [Route("Add_phone/{id_telephonist}")]
        [HttpPost]
        public ActionResult<Phone_numbersDTO> Add_phone(int id_telephonist, Phone_numbersDTO phone)
        {
            try
            {
                DateTime date=new DateTime();
                date = DateTime.UtcNow;
                date = date.AddHours(3);
                phone.CreationDate=date;
                Phone_numbersDTO phone_Number = _Phone_numbersBL.Add_phone(phone, id_telephonist);
                return phone_Number;
            }
            catch (Exception hy)
            {
                return NotFound();
            }

        }

        [Route("getLeadForTelephonistInCompany/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getLeadForTelephonistInCompany(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_Numbers = _Phone_numbersBL.getLeadForTelephonistInCompany(id);
                return phone_Numbers;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }


        [Route("getPhoneForTelephonistInCompany/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getPhoneForTelephonistInCompany(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_Numbers = _Phone_numbersBL.getPhoneForTelephonistInCompany(id);
                return phone_Numbers;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [Route("getPhoneComForTelephonistInCompany/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getPhoneComForTelephonistInCompany(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_Numbers = _Phone_numbersBL.getPhoneComForTelephonistInCompany(id);
                return phone_Numbers;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [Route("getAllPhoneForTelephonistInCompany/{id}")]
        [HttpGet]
        public ActionResult<List<Phone_numbersDTO>> getAllPhoneForTelephonistInCompany(int id)
        {
            try
            {
                List<Phone_numbersDTO> phone_Numbers = _Phone_numbersBL.getAllPhoneForTelephonistInCompany(id);
                return phone_Numbers;
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
    }

}
