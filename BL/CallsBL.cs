using System;
using System.Collections.Generic;
using System.Text;
using BL;
using DAL;
using DataObject;
using AutoMapper;
using DAL.models;
using System.Linq;

namespace BL
{
    public class CallsBL : ICallsBL
    {
        ICallsDAL _CallsDAL;
        IPhone_numbersBL _phone_NumbersBL;
        ITelephonistCompaniesBL _telephonistCompaniesBL;
        IPhone_numbersDAL _phone_NumbersDAL;
        ICompaniesBL _companiesBL;

        IMapper iMapper;
        public CallsBL(ICallsDAL callsDal, ITelephonistCompaniesBL telephonistCompaniesBL, IPhone_numbersDAL phone_NumbersDAL, ICompaniesBL companiesBL)
        {
            _companiesBL = companiesBL;
            _phone_NumbersDAL = phone_NumbersDAL;
            _telephonistCompaniesBL = telephonistCompaniesBL;
            _CallsDAL = callsDal;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            iMapper = config.CreateMapper();
        }

        //(שליפת כל השיחות של טלפנית מסויימת בחברה שיש לה להיום עוד לעשות(גדול מהשעה הנוכחית)  
        public List<LeadsAndCallsDTO> get_calls_for_today(int id)
        {

            List<Calls> CallsList = _CallsDAL.GetAllCals();
            List<LeadsAndCallsDTO> leadsAndCallsDTOlist = new List<LeadsAndCallsDTO>();
            CallsDTO callsDTO = new CallsDTO();
            LeadsAndCallsDTO leadsAndCallsDTO = new LeadsAndCallsDTO();

            List<Phone_numbersDTO> listPhonesNumber = new List<Phone_numbersDTO>();
            Phone_numbersDTO phone_NumbersDTO = new Phone_numbersDTO();
            int idCompany = _telephonistCompaniesBL.GetIdCompaniesByIdTelephonist(id);
            DateTime date1 = new DateTime();
            date1 = DateTime.UtcNow;
            date1 = date1.AddHours(3);
            DateTime dateTime = new DateTime();

            //date1.AddHours(3);
            foreach (var item1 in CallsList)
            {
                if (item1.IdTelephonist == id)
                {
                    phone_NumbersDTO = GetPhone_number((int)item1.IdPhoneNumber);
                    if (item1.DateCall != null && item1.TimeCall != null)
                        dateTime = item1.DateCall;
                        //if (item1.DateCall.Value.Year == date1.Year && item1.DateCall.Value.Month ==date1.Month && item1.DateCall.Value.Day == date1.Day && item1.TimeCall.Value.Hour >= date1.Hour&&item1.Done==0)
                        if (dateTime.Year == date1.Year && dateTime.Month == date1.Month && dateTime.Day == date1.Day && item1.Done == 0)
                        {
                            leadsAndCallsDTO = new LeadsAndCallsDTO();
                            callsDTO = iMapper.Map<Calls, CallsDTO>(item1);
                            leadsAndCallsDTO.leadId = phone_NumbersDTO.Id;
                            leadsAndCallsDTO.leadPhoneNumber = phone_NumbersDTO.Phone;
                            leadsAndCallsDTO.leadPhone2 = phone_NumbersDTO.Phone2;
                            leadsAndCallsDTO.leadPlaceWorking = phone_NumbersDTO.PlaceWorking;
                            leadsAndCallsDTO.leadFirstName = phone_NumbersDTO.FirstName;
                            leadsAndCallsDTO.leadLastName = phone_NumbersDTO.LastName;
                            leadsAndCallsDTO.leadCreationDate = phone_NumbersDTO.CreationDate;
                            leadsAndCallsDTO.leadCity = phone_NumbersDTO.City;
                            leadsAndCallsDTO.leadStatus = phone_NumbersDTO.Status;
                            leadsAndCallsDTO.leadType = phone_NumbersDTO.type;
                            leadsAndCallsDTO.leadAddress = phone_NumbersDTO.Address;
                            leadsAndCallsDTO.leadMail = phone_NumbersDTO.Mail;
                            leadsAndCallsDTO.IdCall = item1.Id;
                            leadsAndCallsDTO.DateCall = item1.DateCall;
                            leadsAndCallsDTO.TimeCall = item1.TimeCall;
                            leadsAndCallsDTO.Done = item1.Done;
                        leadsAndCallsDTO.leadTz = phone_NumbersDTO.Tz;

                        leadsAndCallsDTO.IdTelephonist_c = (int)item1.IdTelephonist;
                            //לטפל בזה- בכמות פעמים שלא עונה הליד
                            leadsAndCallsDTO.countcall = 0;
                            leadsAndCallsDTOlist.Add(leadsAndCallsDTO);
                        }
                }
            }
            return leadsAndCallsDTOlist;
        }

        //  שליפת כל השיחות של טלפנית מסויימת בחברה שיש לה להיום כולל השיחות שהשעה קטנה מעכשיו)  
        public List<LeadsAndCallsDTO> get_calls_for_today_allcall(int id)
        {
            List<Calls> CallsList = _CallsDAL.GetAllCals();
            List<LeadsAndCallsDTO> leadsAndCallsDTOlist = new List<LeadsAndCallsDTO>();
            LeadsAndCallsDTO leadsAndCallsDTO = new LeadsAndCallsDTO();
            Phone_numbersDTO phone_NumbersDTO = new Phone_numbersDTO();
            int idCompany = _telephonistCompaniesBL.GetIdCompaniesByIdTelephonist(id);
            DateTime date1 = new DateTime();
            date1 = DateTime.UtcNow;
            date1 = date1.AddHours(3);
            DateTime dateTime = new DateTime();
            foreach (var item1 in CallsList)
            {
                if (item1.IdTelephonist == id)
                {
                    dateTime = item1.DateCall;
                    phone_NumbersDTO = GetPhone_number((int)item1.IdPhoneNumber);
                    if (item1.IdTelephonist == id && dateTime.Year == date1.Year && dateTime.Month == date1.Month && dateTime.Day == date1.Day)
                    {
                        leadsAndCallsDTO = new LeadsAndCallsDTO();
                        leadsAndCallsDTO.leadId = phone_NumbersDTO.Id;
                        leadsAndCallsDTO.leadPhoneNumber = phone_NumbersDTO.Phone;
                        leadsAndCallsDTO.leadPhone2 = phone_NumbersDTO.Phone2;
                        leadsAndCallsDTO.leadPlaceWorking = phone_NumbersDTO.PlaceWorking;
                        leadsAndCallsDTO.leadFirstName = phone_NumbersDTO.FirstName;
                        leadsAndCallsDTO.leadLastName = phone_NumbersDTO.LastName;
                        leadsAndCallsDTO.leadCreationDate = phone_NumbersDTO.CreationDate;
                        leadsAndCallsDTO.leadStatus = phone_NumbersDTO.Status;
                        leadsAndCallsDTO.leadType = phone_NumbersDTO.type;
                        leadsAndCallsDTO.leadCity = phone_NumbersDTO.City;
                        leadsAndCallsDTO.leadAddress = phone_NumbersDTO.Address;
                        leadsAndCallsDTO.leadMail = phone_NumbersDTO.Mail;
                        leadsAndCallsDTO.IdCall = item1.Id;
                        leadsAndCallsDTO.DateCall = item1.DateCall;
                        leadsAndCallsDTO.TimeCall = item1.TimeCall;
                        leadsAndCallsDTO.IdTelephonist_c = (int)item1.IdTelephonist;
                        leadsAndCallsDTO.Done = item1.Done;
                        leadsAndCallsDTO.leadTz = phone_NumbersDTO.Tz;

                        //לטפל בזה- בכמות פעמים שלא עונה הליד
                        leadsAndCallsDTO.countcall = 0;
                        leadsAndCallsDTOlist.Add(leadsAndCallsDTO);
                    }
                }
            }

            return leadsAndCallsDTOlist;
        }
        //החזרת כל מספר טלפון לפי ID
        //ממוקם כאן ולא במספרי טלפון - בגלל הלולאה הנוצרת מההזרקות
        public Phone_numbersDTO GetPhone_number(int id)
        {
            List<PhoneNumbers> Phone_numbersList = _phone_NumbersDAL.GetPhoneNumbers();
            Phone_numbersDTO phone_numbersDTO = new Phone_numbersDTO();

            foreach (var item1 in Phone_numbersList)
            {
                if (item1.Id == id)
                {
                    phone_numbersDTO = iMapper.Map<PhoneNumbers, Phone_numbersDTO>(item1);
                    break;
                }
            }
            return phone_numbersDTO;
        }

        //החזרת שיחות של טלפנית מסוימת לפי תאריך מסויים
        public List<LeadsAndCallsDTO> get_calls_of_a_certain_date(int id, DateTime date)
        {

            List<Calls> CallsList = _CallsDAL.GetAllCals();
            List<LeadsAndCallsDTO> leadsAndCallsDTOlist = new List<LeadsAndCallsDTO>();
            CallsDTO CallsDTO = new CallsDTO();
            LeadsAndCallsDTO leadsAndCallsDTO = new LeadsAndCallsDTO();
            List<Phone_numbersDTO> listPhonesNumber = new List<Phone_numbersDTO>();
            Phone_numbersDTO phone_NumbersDTO = new Phone_numbersDTO();
            int idCompany = _telephonistCompaniesBL.GetIdCompaniesByIdTelephonist(id);
            DateTime dateTime = new DateTime();

            foreach (var item1 in CallsList )
            {
                dateTime = item1.DateCall;
                //להשוות סטטוס
                if (item1.IdTelephonist == id  && dateTime.Date != null)
                {
                    phone_NumbersDTO = GetPhone_number((int)item1.IdPhoneNumber);
                    if (item1.IdTelephonist == id && dateTime.Date == date.Date)
                    {
                        leadsAndCallsDTO = new LeadsAndCallsDTO();
                        CallsDTO = iMapper.Map<Calls, CallsDTO>(item1);
                        leadsAndCallsDTO.leadId = phone_NumbersDTO.Id;
                        leadsAndCallsDTO.leadPhoneNumber = phone_NumbersDTO.Phone;
                        leadsAndCallsDTO.leadPhone2 = phone_NumbersDTO.Phone2;
                        leadsAndCallsDTO.leadPlaceWorking = phone_NumbersDTO.PlaceWorking;
                        leadsAndCallsDTO.leadFirstName = phone_NumbersDTO.FirstName;
                        leadsAndCallsDTO.leadLastName = phone_NumbersDTO.LastName;
                        leadsAndCallsDTO.leadCreationDate = phone_NumbersDTO.CreationDate;
                        leadsAndCallsDTO.leadStatus = phone_NumbersDTO.Status;
                        leadsAndCallsDTO.leadType = phone_NumbersDTO.type;
                        leadsAndCallsDTO.leadCity = phone_NumbersDTO.City;
                        leadsAndCallsDTO.leadAddress = phone_NumbersDTO.Address;
                        leadsAndCallsDTO.leadMail = phone_NumbersDTO.Mail;
                        leadsAndCallsDTO.IdCall = item1.Id;
                        leadsAndCallsDTO.DateCall = item1.DateCall;
                        leadsAndCallsDTO.TimeCall = item1.TimeCall;
                        leadsAndCallsDTO.Done = item1.Done;
                        leadsAndCallsDTO.IdTelephonist_c = (int)item1.IdTelephonist;
                        leadsAndCallsDTO.Done = item1.Done;
                        leadsAndCallsDTO.leadTz = phone_NumbersDTO.Tz;

                        //לטפל בזה- בכמות פעמים שלא עונה הליד
                        leadsAndCallsDTO.countcall = 0;
                        leadsAndCallsDTOlist.Add(leadsAndCallsDTO);
                    }
                }
            }
            return leadsAndCallsDTOlist;
        }
        //מקבל מספר טלפנית בחברה ומחזיר את כל השיחות שלא ענים לה
        public List<LeadsAndCallsDTO> get_no_answer__calls(int id)
        {
            List<Calls> CallsList = _CallsDAL.GetAllCals();
            List<LeadsAndCallsDTO> leadsAndCallsDTOlist = new List<LeadsAndCallsDTO>();
            CallsDTO CallsDTO = new CallsDTO();
            LeadsAndCallsDTO leadsAndCallsDTO = new LeadsAndCallsDTO();
            List<Phone_numbersDTO> listPhonesNumber = new List<Phone_numbersDTO>();
            Phone_numbersDTO phone_NumbersDTO = new Phone_numbersDTO();
            int idCompany = _telephonistCompaniesBL.GetIdCompaniesByIdTelephonist(id);
            bool bool1;
            //מיון רשימת השיחות
            foreach (var item1 in CallsList)
            {
                if (item1.IdTelephonist == id)
                {
                    phone_NumbersDTO = GetPhone_number((int)item1.IdPhoneNumber);
                    //בדיקה שברשימה לא הכנסנו כבר את הליד
                    if (phone_NumbersDTO.Status == (int)estatus.לא_עונים)
                    {
                        bool1 = leadsAndCallsDTOlist.Any(x => x.leadId == phone_NumbersDTO.Id);
                        if (bool1 == false)
                        {
                            CallsDTO = iMapper.Map<Calls, CallsDTO>(item1);
                            leadsAndCallsDTO = new LeadsAndCallsDTO();
                            leadsAndCallsDTO.leadId = phone_NumbersDTO.Id;
                            leadsAndCallsDTO.leadPhoneNumber = phone_NumbersDTO.Phone;
                            leadsAndCallsDTO.leadPhone2 = phone_NumbersDTO.Phone2;
                            leadsAndCallsDTO.leadPlaceWorking = phone_NumbersDTO.PlaceWorking;
                            leadsAndCallsDTO.leadFirstName = phone_NumbersDTO.FirstName;
                            leadsAndCallsDTO.leadLastName = phone_NumbersDTO.LastName;
                            leadsAndCallsDTO.leadCreationDate = phone_NumbersDTO.CreationDate;
                            leadsAndCallsDTO.leadCity = phone_NumbersDTO.City;
                            leadsAndCallsDTO.leadStatus = phone_NumbersDTO.Status;
                            leadsAndCallsDTO.leadType = phone_NumbersDTO.type;
                            leadsAndCallsDTO.leadAddress = phone_NumbersDTO.Address;
                            leadsAndCallsDTO.leadMail = phone_NumbersDTO.Mail;
                            leadsAndCallsDTO.leadTz = phone_NumbersDTO.Tz;

                            leadsAndCallsDTO.IdCall = item1.Id;
                            leadsAndCallsDTO.DateCall = item1.DateCall;
                            leadsAndCallsDTO.TimeCall = item1.TimeCall;
                            leadsAndCallsDTO.IdTelephonist_c = (int)item1.IdTelephonist;
                            leadsAndCallsDTO.Done = item1.Done;
                            //לטפל בזה- בכמות פעמים שלא עונה הליד
                            leadsAndCallsDTO.countcall = 0;
                            leadsAndCallsDTOlist.Add(leadsAndCallsDTO);
                        }
                    }
                }
            }
            return leadsAndCallsDTOlist;
        }

        //public List<LeadsAndCallsDTO> Returned_from_the_system(int id)
        //{
        //    List<Calls> CallsList = _CallsDAL.GetAllCals();
        //    List<LeadsAndCallsDTO> leadsAndCallsDTOlist = new List<LeadsAndCallsDTO>();
        //    LeadsAndCallsDTO leadsAndCallsDTO = new LeadsAndCallsDTO();
        //    List<Phone_numbersDTO> listPhonesNumber = new List<Phone_numbersDTO>();
        //    Phone_numbersDTO phone_NumbersDTO = new Phone_numbersDTO();
        //    int idCompany = _telephonistCompaniesBL.GetIdCompaniesByIdTelephonist(id);
        //    foreach (var item1 in CallsList)
        //    {
        //        if (item1.IdTelephonist == id)
        //        {
        //            phone_NumbersDTO = GetPhone_number(item1.IdPhoneNumber);
        //            if (phone_NumbersDTO.Status == (int)estatus.התקבל_מהמערכת_ולא_טופל)
        //            {
        //                leadsAndCallsDTO = new LeadsAndCallsDTO();
        //                leadsAndCallsDTO.leadId = phone_NumbersDTO.Id;
        //                leadsAndCallsDTO.leadPhoneNumber = phone_NumbersDTO.Phone;
        //                leadsAndCallsDTO.leadPhone2 = phone_NumbersDTO.Phone2;
        //                leadsAndCallsDTO.leadPlaceWorking = phone_NumbersDTO.PlaceWorking;
        //                leadsAndCallsDTO.leadFirstName = phone_NumbersDTO.FirstName;
        //                leadsAndCallsDTO.leadLastName = phone_NumbersDTO.LastName;
        //                leadsAndCallsDTO.leadCreationDate = phone_NumbersDTO.CreationDate;
        //                leadsAndCallsDTO.leadCity = phone_NumbersDTO.City;
        //                leadsAndCallsDTO.leadStatus = phone_NumbersDTO.Status;
        //                leadsAndCallsDTO.leadType = phone_NumbersDTO.type;
        //                leadsAndCallsDTO.leadAddress = phone_NumbersDTO.Address;
        //                leadsAndCallsDTO.leadMail = phone_NumbersDTO.Mail;
        //                leadsAndCallsDTO.IdCall = item1.Id;
        //                leadsAndCallsDTO.DateCall = item1.DateCall;
        //                leadsAndCallsDTO.TimeCall = item1.TimeCall;
        //                leadsAndCallsDTO.IdTelephonist_c = (int)item1.IdTelephonist;
        //                //לטפל בזה- בכמות פעמים שלא עונה הליד
        //                leadsAndCallsDTO.countcall = 0;
        //                leadsAndCallsDTOlist.Add(leadsAndCallsDTO);
        //            }
        //        }
        //    }
        //    return leadsAndCallsDTOlist;
        //}


        //הוספת שיחה
        public List<CallsDTO> Add_call_for_telephonnist(CallsDTO callsDTO)
        {
            Calls call = iMapper.Map<CallsDTO, Calls>(callsDTO);
            Phone_numbersDTO phone_NumbersDTO = GetPhone_number((int)call.IdPhoneNumber);
            phone_NumbersDTO.Status = (int)estatus.שיחה_עתידית;
            PhoneNumbers phoneNumbers = iMapper.Map<Phone_numbersDTO, PhoneNumbers>(phone_NumbersDTO);
            _phone_NumbersDAL.updatePhoneNumber(phoneNumbers);

            _CallsDAL.AddCalls(call);

            return get_all_calls_for_phone_and_telephonist((int)call.IdTelephonist, (int)call.IdPhoneNumber);
        }
        //החזרת שיחת ואיש קשר של שיחה מסויימת- על פי קוד שיחה
        public LeadsAndCallsDTO get_callsLead_by_id(int id)
        {
            List<Calls> CallsList = _CallsDAL.GetAllCals();
            LeadsAndCallsDTO leadsAndCallsDTO = new LeadsAndCallsDTO();
            Phone_numbersDTO phone_NumbersDTO = new Phone_numbersDTO();
            foreach (var item1 in CallsList)
            {
                //להשוות סטטוס
                if (item1.Id == id)
                {
                    phone_NumbersDTO = GetPhone_number((int)item1.IdPhoneNumber);
                    leadsAndCallsDTO = new LeadsAndCallsDTO();
                    leadsAndCallsDTO.leadId = phone_NumbersDTO.Id;
                    leadsAndCallsDTO.leadPhoneNumber = phone_NumbersDTO.Phone;
                    leadsAndCallsDTO.leadPhone2 = phone_NumbersDTO.Phone2;
                    leadsAndCallsDTO.leadPlaceWorking = phone_NumbersDTO.PlaceWorking;
                    leadsAndCallsDTO.leadFirstName = phone_NumbersDTO.FirstName;
                    leadsAndCallsDTO.leadLastName = phone_NumbersDTO.LastName;
                    leadsAndCallsDTO.leadCreationDate = phone_NumbersDTO.CreationDate;
                    leadsAndCallsDTO.leadCity = phone_NumbersDTO.City;
                    leadsAndCallsDTO.leadStatus = phone_NumbersDTO.Status;
                    leadsAndCallsDTO.leadType = phone_NumbersDTO.type;
                    leadsAndCallsDTO.leadAddress = phone_NumbersDTO.Address;
                    leadsAndCallsDTO.leadMail = phone_NumbersDTO.Mail;
                    leadsAndCallsDTO.leadTz = phone_NumbersDTO.Tz;

                    leadsAndCallsDTO.IdCall = item1.Id;
                    leadsAndCallsDTO.DateCall = item1.DateCall;
                    leadsAndCallsDTO.TimeCall = item1.TimeCall;
                    leadsAndCallsDTO.IdTelephonist_c = (int)item1.IdTelephonist;
                    leadsAndCallsDTO.Done = item1.Done;
                    //לטפל בזה- בכמות פעמים שלא עונה הליד
                    leadsAndCallsDTO.countcall = 0;
                    break;
                }
            }
            return leadsAndCallsDTO;
        }

        //מחיקת שיחה לפי ID
        public void DeleteCall(int id)
        {
            _CallsDAL.DeleteCall(id);
        }
        //החזרת כל השיחות לפי קוד איש קשר ולפי קוד טלפנית 
        public List<CallsDTO> get_all_calls_for_phone_and_telephonist(int id_telephonist, int id_phone)
        {
            List<Calls> callss = _CallsDAL.GetAllCals();
            List<Calls> calls1 = callss.Where(x => x.IdPhoneNumber == id_phone && x.IdPhoneNumber == id_phone ).ToList();
            List<CallsDTO> callsDTOs = iMapper.Map<List<Calls>, List<CallsDTO>>(calls1);
            return callsDTOs;
        }
        //החזרת כל הסטוריית השיחות לפי קוד איש קשר ולפי קוד טלפנית 
        public List<CallsDTO> get_all_history_calls_for_phone_and_telephonist(int id_telephonist, int id_phone)
        {
            List<Calls> callss = _CallsDAL.GetAllCals();
            DateTime date1 = DateTime.UtcNow;
            date1 = date1.AddHours(3);
            date1 = date1.AddHours(3);
            List<Calls> calls1 = callss.Where(x => x.IdPhoneNumber == id_phone && x.Done == 1 && date1.Date > x.DateCall.Date).ToList();
            List<Calls> calls2 = callss.Where(x => x.IdPhoneNumber == id_phone && x.Done == 1 && x.IdPhoneNumber == id_phone && x.TimeCall.Hour <= date1.Hour && x.TimeCall.Minute < date1.Minute).ToList();
            foreach (var item in calls2)
            {
                calls1.Add(item);
            }
            List<CallsDTO> callsDTOs = iMapper.Map<List<Calls>, List<CallsDTO>>(calls1);
            return callsDTOs;
        }
        //החזרת כל השיחות העתידיות לפי קוד איש קשר ולפי קוד טלפנית 

        public List<CallsDTO> get_all_future_calls_for_phone_and_telephonist(int id_telephonist, int id_phone)
        {
            List<Calls> callss = _CallsDAL.GetAllCals();
            DateTime date1 = DateTime.UtcNow;
            date1 = date1.AddHours(3);
            List<Calls> calls1 = new List<Calls>();
            foreach (var x in callss)
            {
                if (x.TimeCall != null && x.DateCall != null && x.Done == 0)
                {
                    if (x.IdPhoneNumber == id_phone && x.IdTelephonist == id_telephonist && date1.Date <= x.DateCall.Date || (date1.Date == x.DateCall.Date && date1.Hour <= x.TimeCall.Hour && date1.Minute <= x.TimeCall.Minute))
                        calls1.Add(x);
                }
            }
            List<CallsDTO> callsDTOs = iMapper.Map<List<Calls>, List<CallsDTO>>(calls1);
            return callsDTOs;

        }
        //קביעת שיחה לתאריך פנוי
        public CallsDTO GetFreeDateCall(int id_telephonist, int day, int idphoone_number)
        {
            CallsDTO call = new CallsDTO();
            DateTime date = DateTime.Today;
            bool degel = false;
            degel = _CallsDAL.GetAllCalsForTelephonistInCompany(id_telephonist).Any(x => x.DateCall.Year == DateTime.Today.Year && x.DateCall.Month == DateTime.Today.Month && x.DateCall.Day == DateTime.Today.Day + day++ && x.TimeCall.Hour == DateTime.Today.Hour && x.TimeCall.Minute == DateTime.Today.Minute && x.TimeCall.Second == DateTime.Today.Second);
            if (degel = false)
            {
                call.IdPhoneNumber = idphoone_number;
                call.IdTelephonist = id_telephonist;
                call.TranscriptCall = null;
                call.TimeCall = DateTime.Today;
                call.DateCall = DateTime.Today;
                call.DateCall = call.DateCall.Value.AddDays(day);
            }
            else
            {
                while (degel = _CallsDAL.GetAllCalsForTelephonistInCompany(id_telephonist).Any(x => x.DateCall.Year == DateTime.Today.Year && x.DateCall.Month == DateTime.Today.Month && x.DateCall.Day == DateTime.Today.Day + day++ && x.TimeCall.Hour == DateTime.Today.Hour && x.TimeCall.Minute == DateTime.Today.Minute && x.TimeCall.Second == DateTime.Today.Second))
                { }
                call.IdPhoneNumber = idphoone_number;
                call.IdTelephonist = id_telephonist;
                call.TranscriptCall = null;
                call.TimeCall = DateTime.Today;
                call.DateCall = DateTime.Today;
                call.DateCall = call.DateCall.Value.AddDays(day);

            }
            return call;
        }

        //החזרת כל השיחות של טלפנית מסויימת בחברה
        public List<LeadsAndCallsDTO> get_All_calls_for_telephonist(int id)
        {

            List<Calls> CallsList = _CallsDAL.GetAllCals();
            List<LeadsAndCallsDTO> leadsAndCallsDTOlist = new List<LeadsAndCallsDTO>();
            CallsDTO CallsDTO = new CallsDTO();
            LeadsAndCallsDTO leadsAndCallsDTO = new LeadsAndCallsDTO();

            List<Phone_numbersDTO> listPhonesNumber = new List<Phone_numbersDTO>();
            Phone_numbersDTO phone_NumbersDTO = new Phone_numbersDTO();
            int idCompany = _telephonistCompaniesBL.GetIdCompaniesByIdTelephonist(id);
            foreach (var item1 in CallsList)
            {
                //להשוות סטטוס
                if (item1.IdTelephonist == id )
                {
                    phone_NumbersDTO = GetPhone_number((int)item1.IdPhoneNumber);
                    leadsAndCallsDTO = new LeadsAndCallsDTO();
                    CallsDTO = iMapper.Map<Calls, CallsDTO>(item1);
                    leadsAndCallsDTO.leadId = phone_NumbersDTO.Id;
                    leadsAndCallsDTO.leadPhoneNumber = phone_NumbersDTO.Phone;
                    leadsAndCallsDTO.leadPhone2 = phone_NumbersDTO.Phone2;
                    leadsAndCallsDTO.leadPlaceWorking = phone_NumbersDTO.PlaceWorking;
                    leadsAndCallsDTO.leadFirstName = phone_NumbersDTO.FirstName;
                    leadsAndCallsDTO.leadLastName = phone_NumbersDTO.LastName;
                    leadsAndCallsDTO.leadCreationDate = phone_NumbersDTO.CreationDate;
                    leadsAndCallsDTO.leadCity = phone_NumbersDTO.City;
                    leadsAndCallsDTO.leadStatus = phone_NumbersDTO.Status;
                    leadsAndCallsDTO.leadType = phone_NumbersDTO.type;
                    leadsAndCallsDTO.leadAddress = phone_NumbersDTO.Address;
                    leadsAndCallsDTO.leadMail = phone_NumbersDTO.Mail;
                    leadsAndCallsDTO.IdCall = item1.Id;
                    leadsAndCallsDTO.DateCall = item1.DateCall;
                    leadsAndCallsDTO.TimeCall = item1.TimeCall;
                    leadsAndCallsDTO.IdTelephonist_c = (int)item1.IdTelephonist;
                    leadsAndCallsDTO.Done = item1.Done;
                    leadsAndCallsDTO.leadTz = phone_NumbersDTO.Tz;

                    //לטפל בזה- בכמות פעמים שלא עונה הליד
                    leadsAndCallsDTO.countcall = 0;
                    leadsAndCallsDTOlist.Add(leadsAndCallsDTO);
                }
            }
            leadsAndCallsDTOlist = leadsAndCallsDTOlist.OrderBy(x => x.DateCall).ToList();
            return leadsAndCallsDTOlist;
        }

        public List<LeadsAndCallsDTO> get_All_calls_for_telephonist1(int id)
        {

            List<Calls> CallsList = _CallsDAL.GetAllCals();
            List<LeadsAndCallsDTO> leadsAndCallsDTOlist = new List<LeadsAndCallsDTO>();
            CallsDTO CallsDTO = new CallsDTO();
            LeadsAndCallsDTO leadsAndCallsDTO = new LeadsAndCallsDTO();

            List<Phone_numbersDTO> listPhonesNumber = new List<Phone_numbersDTO>();
            Phone_numbersDTO phone_NumbersDTO = new Phone_numbersDTO();
            int idCompany = _telephonistCompaniesBL.GetIdCompaniesByIdTelephonist(id);
            foreach (var item1 in CallsList)
            {
                //להשוות סטטוס
                if (item1.IdTelephonist == id )
                {
                    phone_NumbersDTO = GetPhone_number((int)item1.IdPhoneNumber);
                    leadsAndCallsDTO = new LeadsAndCallsDTO();
                    CallsDTO = iMapper.Map<Calls, CallsDTO>(item1);
                    leadsAndCallsDTO.leadId = phone_NumbersDTO.Id;
                    leadsAndCallsDTO.leadPhoneNumber = phone_NumbersDTO.Phone;
                    leadsAndCallsDTO.leadPhone2 = phone_NumbersDTO.Phone2;
                    leadsAndCallsDTO.leadPlaceWorking = phone_NumbersDTO.PlaceWorking;
                    leadsAndCallsDTO.leadFirstName = phone_NumbersDTO.FirstName;
                    leadsAndCallsDTO.leadLastName = phone_NumbersDTO.LastName;
                    leadsAndCallsDTO.leadCreationDate = phone_NumbersDTO.CreationDate;
                    leadsAndCallsDTO.leadCity = phone_NumbersDTO.City;
                    leadsAndCallsDTO.leadStatus = phone_NumbersDTO.Status;
                    leadsAndCallsDTO.leadType = phone_NumbersDTO.type;
                    leadsAndCallsDTO.leadAddress = phone_NumbersDTO.Address;
                    leadsAndCallsDTO.leadMail = phone_NumbersDTO.Mail;
                    leadsAndCallsDTO.leadTz = phone_NumbersDTO.Tz;

                    leadsAndCallsDTO.IdCall = item1.Id;
                    leadsAndCallsDTO.DateCall = item1.DateCall;
                    leadsAndCallsDTO.TimeCall = item1.TimeCall;
                    leadsAndCallsDTO.IdTelephonist_c = (int)item1.IdTelephonist;
                    leadsAndCallsDTO.Done = item1.Done;
                    //לטפל בזה- בכמות פעמים שלא עונה הליד
                    leadsAndCallsDTO.countcall = 0;
                    leadsAndCallsDTOlist.Add(leadsAndCallsDTO);
                }
            }
            leadsAndCallsDTOlist = leadsAndCallsDTOlist.OrderBy(x => x.DateCall).ToList();
            return leadsAndCallsDTOlist;
        }

        //החזרת כל השיחות של חברה מסויימת של כל הטלפניות(מקבל מספר חברה)
        public List<LeadsAndCallsDTO> get_All_calls_for_Company(int id)
        {
            List<LeadsAndCallsDTO> leadsAndCallsDTOs = new List<LeadsAndCallsDTO>();
            List<TelephonistInCompanyDetails> telephonistInCompanyDetails = _companiesBL.getAlltelephonistInCimpany(id);
            List<LeadsAndCallsDTO> leadsAndCallsDTOs1 = new List<LeadsAndCallsDTO>();
            foreach (var item in telephonistInCompanyDetails)
            {
                leadsAndCallsDTOs1 = get_All_calls_for_telephonist(item.IdTelephonistInCompany).ToList();
                foreach (var item1 in leadsAndCallsDTOs1)
                {
                    leadsAndCallsDTOs.Add(item1);
                }
            }
            leadsAndCallsDTOs = leadsAndCallsDTOs.OrderBy(x => x.DateCall).ToList();
            return leadsAndCallsDTOs;


        }
        public CallsDTO Update_call(CallsDTO call)
        {
            Calls c = iMapper.Map<CallsDTO, Calls>(call);
    
            _CallsDAL.updateCalls(c);
            c = _CallsDAL.GetAllCals().FirstOrDefault(x => x.Id == c.Id);
            call = iMapper.Map<Calls, CallsDTO>(c);
            return call;

        }

        public CallsDTO Update_call1(CallsDTO call)
        {
            Calls c = iMapper.Map<CallsDTO, Calls>(call);
            _CallsDAL.updateCalls(c);
            c = _CallsDAL.GetAllCals().FirstOrDefault(x => x.Id == c.Id);
            call = iMapper.Map<Calls, CallsDTO>(c);
            return call;

        }
        public CallsDTO get_call_by_id_call(int id)
        {
            List<Calls> CallsList = _CallsDAL.GetAllCals();
            Calls calls = new Calls();
            foreach (var item1 in CallsList)
            {
                //להשוות סטטוס
                if (item1.Id == id)
                {
                    calls = item1;

                }
            }
            CallsDTO callsDTO = iMapper.Map<Calls, CallsDTO>(calls);

            return callsDTO;
        }
    }
}