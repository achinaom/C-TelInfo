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
    public class CallsController : ControllerBase
    {
        ICallsBL _CallsBL;

        public CallsController(ICallsBL CallsBL)
        {
            _CallsBL = CallsBL;
        }

        [Route("get_calls_for_today/{id}")]
        [HttpGet]
        public ActionResult<List<LeadsAndCallsDTO>> get_calls_for_today(int id)
        {
            try
            {
                List<LeadsAndCallsDTO> LeadsAndCallsDTO = _CallsBL.get_calls_for_today(id);
                return LeadsAndCallsDTO;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [Route("get_calls_for_today_allcall/{id}")]
        [HttpGet]
        public ActionResult<List<LeadsAndCallsDTO>> get_calls_for_today_allcall(int id)
        {
            try
            {
                List<LeadsAndCallsDTO> LeadsAndCallsDTO = _CallsBL.get_calls_for_today_allcall(id);
                return LeadsAndCallsDTO;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [Route("get_calls_of_a_certain_date/{id}/{date}")]
        [HttpGet]
        public ActionResult<List<LeadsAndCallsDTO>> get_calls_of_a_certain_date(int id, DateTime date)
        {
            try
            {
                List<LeadsAndCallsDTO> LeadsAndCallsDTO = _CallsBL.get_calls_of_a_certain_date(id, date);
                return LeadsAndCallsDTO;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }
        [Route("get_callsLead_by_id/{id}")]
        [HttpGet]
        public ActionResult<LeadsAndCallsDTO> get_callsLead_by_id(int id)
        {
            try
            {
                LeadsAndCallsDTO LeadsAndCallsDTO = _CallsBL.get_callsLead_by_id(id);

                return LeadsAndCallsDTO;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [Route("update_call")]
        [HttpPut]
        public ActionResult<CallsDTO> update_call(CallsDTO call)
        {
            try
            {
                CallsDTO call1 = _CallsBL.Update_call(call);

                return call1;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [Route("update_call_transcript")]
        [HttpPut]
        public ActionResult<CallsDTO> update_call_transcript(CallsDTO call)
        {
            try
            {
                CallsDTO call1 = _CallsBL.Update_call1(call);

                return call1;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }





        //[Route("Returned_from_the_system/{id}")]
        //[HttpGet]
        //public ActionResult<List<LeadsAndCallsDTO>> Returned_from_the_system(int id)
        //{
        //    try
        //    {
        //        List<LeadsAndCallsDTO> LeadsAndCallsDTO = _CallsBL.Returned_from_the_system(id);
        //        return LeadsAndCallsDTO;
        //    }
        //    catch (Exception)
        //    {

        //        return NotFound();
        //    }

        //}




        [Route("get_no_answer__calls/{id}")]
        [HttpGet]
        public ActionResult<List<LeadsAndCallsDTO>> get_no_answer__calls(int id)
        {
            try
            {
                List<LeadsAndCallsDTO> LeadsAndCallsDTO = _CallsBL.get_no_answer__calls(id);
                return LeadsAndCallsDTO;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }


        [Route("delete_call/{id_call}")]
        [HttpDelete]
        public ActionResult delete_call(int id_call)
        {
            try
            {
                _CallsBL.DeleteCall(id_call);
                return Ok();

            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [Route("get_all_calls_for_phone_and_telephonist/{id_phone}/{id_telephonist}")]
        [HttpGet]
        public ActionResult<List<CallsDTO>> get_all_calls_for_phone_and_telephonist(int id_phone, int id_telephonist)
        {
            try
            {
                List<CallsDTO> callsDTOs = _CallsBL.get_all_calls_for_phone_and_telephonist(id_telephonist, id_phone);
                return callsDTOs;
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [Route("get_all_history_calls_for_phone_and_telephonist/{id_phone}/{id_telephonist}")]
        [HttpGet]
        public ActionResult<List<CallsDTO>> get_all_history_calls_for_phone_and_telephonist(int id_phone, int id_telephonist)
        {
            try
            {
                List<CallsDTO> callsDTOs = _CallsBL.get_all_history_calls_for_phone_and_telephonist(id_telephonist, id_phone);
                return callsDTOs;
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        [Route("get_all_future_calls_for_phone_and_telephonist/{id_phone}/{id_telephonist}")]
        [HttpGet]
        public ActionResult<List<CallsDTO>> get_all_future_calls_for_phone_and_telephonist(int id_phone, int id_telephonist)
        {
            try
            {
                List<CallsDTO> callsDTOs = _CallsBL.get_all_future_calls_for_phone_and_telephonist(id_telephonist, id_phone);
                return callsDTOs;
            }
            catch (Exception e)
            {

                return NotFound();
            }
        }

        //החזרת כל השיחות של חברה מסויימת של כל הטלפניות
        [Route("get_All_calls_for_Company/{id}")]
        [HttpGet]
        public ActionResult<List<LeadsAndCallsDTO>> get_All_calls_for_Company(int id)
        {
            try
            {
               List<LeadsAndCallsDTO> LeadsAndCallsDTO = _CallsBL.get_All_calls_for_Company(id);

                return LeadsAndCallsDTO;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }


        [Route("get_All_calls_for_telephonist/{id}")]
        [HttpGet]
        public ActionResult<List<LeadsAndCallsDTO>> get_All_calls_for_telephonist(int id)
        {
            try
            {
                List<LeadsAndCallsDTO> LeadsAndCallsDTO = _CallsBL.get_All_calls_for_Company(id);

                return LeadsAndCallsDTO;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }


        //הוספת שיחה חדשה 

        //[Route("add_call_for_telephonnist")]
        [HttpPost]
        public ActionResult<List<CallsDTO>> add_call_for_telephonnist([FromBody]CallsDTO call1)
        {
            try
            {
                List<CallsDTO> callsDTOs = _CallsBL.Add_call_for_telephonnist(call1);
                return callsDTOs;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }

        [Route("get_time/{ntime}/{ndate}")]
        [HttpGet]
        public ActionResult<List<CallsDTO>> get_time(DateTime ntime, DateTime ndate)
        {
            try
            {
                List<CallsDTO> c = new List<CallsDTO>();
                return c;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }
        

         [Route("get_call_by_id_call/{id}")]
        [HttpGet]
        public ActionResult<CallsDTO> get_call_by_id_call(int id)
        {
            try
            {
                CallsDTO c = _CallsBL.get_call_by_id_call(id);
                return c;
            }
            catch (Exception)
            {

                return NotFound();
            }

        }
    }
}
