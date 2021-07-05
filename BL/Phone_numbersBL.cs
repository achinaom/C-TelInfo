using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DataObject;
using System.Linq;
using AutoMapper;
using System.IO;
using DAL.models;

namespace BL
{
    public class Phone_numbersBL : IPhone_numbersBL
    {
        IPhone_numbersDAL _Phone_numbersDAL;
        ITelephonistCompaniesBL _telephonistCompaniesBL;
        ICallsBL _callsBL;
        ICallsDAL _callsDAL;
        IMapper iMapper;
        public Phone_numbersBL(ITelephonistCompaniesBL telephonistCompaniesBL, IPhone_numbersDAL phone_NumbersDAL, ICallsBL callsBL, ICallsDAL callsdal)
        {
            _callsDAL = callsdal;
            _callsBL = callsBL;
            _Phone_numbersDAL = phone_NumbersDAL;
            _telephonistCompaniesBL = telephonistCompaniesBL;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            iMapper = config.CreateMapper();
        }
        //פונקצייה הממירה מ:
        //phone_NumbersDTO to PhoneNumbers
        private PhoneNumbers ConvertToPhoneNumbers(Phone_numbersDTO phone_NumbersDTO)
        {
            PhoneNumbers phoneNumbers = new PhoneNumbers();
            phoneNumbers = iMapper.Map<Phone_numbersDTO, PhoneNumbers>(phone_NumbersDTO);
            return phoneNumbers;
        }
        //החזרת כל מספרי הטלפון
        public List<Phone_numbersDTO> GetAll()
        {
            List<PhoneNumbers> Phone_numbersList = _Phone_numbersDAL.GetPhoneNumbers();
            List<Phone_numbersDTO> Phone_numbersDTOList = new List<Phone_numbersDTO>();
            Phone_numbersDTOList = iMapper.Map<List<PhoneNumbers>, List<Phone_numbersDTO>>(Phone_numbersList);
            return Phone_numbersDTOList;
        }

        //הוספת מספר טלפון 
        public void AddLeads(Phone_numbersDTO phone_NumbersDTO)
        {
            PhoneNumbers phoneNumbers = ConvertToPhoneNumbers(phone_NumbersDTO);
            _Phone_numbersDAL.AddPhoneNumbers(phoneNumbers);
        }
        //שליפת מספר טלפון לפי קוד מספר טלפון
        public Phone_numbersDTO GetPhone_number(int id)
        {
            List<PhoneNumbers> Phone_numbersList = _Phone_numbersDAL.GetPhoneNumbers();
            List<Phone_numbersDTO> Phone_numbersDTOList = new List<Phone_numbersDTO>();
            Phone_numbersDTO Phone_numbersDTO = new Phone_numbersDTO();

            foreach (var item1 in Phone_numbersList)
            {
                if (item1.Id == id)
                {
                    Phone_numbersDTO = iMapper.Map<PhoneNumbers, Phone_numbersDTO>(item1);
                    break;
                }
            }
            return Phone_numbersDTO;
        }



        //הוספת לידים לחברה מסויימת לפי מספר חברה מקובץ אקסל שהמנהל מעלה  
        //וכן חילוק של כל מספרי הטלפון לטלפניות בחברה
        public List<LeadsAndCallsDTO> addXL(int id, string name)
        {
            int time_call = 1;
            Phone_numbersDTO o = new Phone_numbersDTO();
            StreamReader read = new StreamReader(name, Encoding.Default);
            string str = read.ReadToEnd();
            string[] arr = str.Split('\n');
            List<TelephonistCompaniesDTO> telephonistCompaniesDTOList = _telephonistCompaniesBL.GetTelephonistCompaniesById(id);

             DateTime date1= DateTime.UtcNow.AddHours(3);
            int sum = (arr.Length - 1) / _telephonistCompaniesBL.GetTelephonistCompaniesById(id).Count();
            int k = telephonistCompaniesDTOList.Count() - 1;
            CallsDTO calls = new CallsDTO();
            LeadsAndCallsDTO leadsAndCallsDTO = new LeadsAndCallsDTO();
            List<LeadsAndCallsDTO> leadsAndCallsDTOs = new List<LeadsAndCallsDTO>();
            int temp = sum;
            for (int i = 1; i <
                arr.Length - 1; i++)
            {

                if (sum == 0)
                {
                    sum += temp;
                    k--;
                }
                if (k < 0)
                {
                    k = telephonistCompaniesDTOList.Count() - 1;
                }
                int j = 0;
                string[] arr1 = arr[i].Split(',');
                o = new Phone_numbersDTO();
                o.Phone = arr1[j++].ToString();
                o.Tz = arr1[j++].ToString();
                o.Address = (arr1[j++]).ToString();
                o.FirstName = (arr1[j++]).ToString();
                o.Mail = (arr1[j++]).ToString();
                o.LastName = (arr1[j++]).ToString();
                o.City = (arr1[j++]).ToString();
                o.Phone2 = (arr1[j++]).ToString();
                o.PlaceWorking = (arr1[j++]).ToString();
                o.mikud = (arr1[j++]).ToString();
                o.CreationDate = date1;
                o.Status = (int)estatus.לא_טופל;
                o.type = (int)etype_phone.ליד;
                o.IdCompanies = id;
                PhoneNumbers o1 = iMapper.Map<Phone_numbersDTO, PhoneNumbers>(o);
                _Phone_numbersDAL.AddPhoneNumbers(o1);
                int id1 = _Phone_numbersDAL.GetPhoneNumbers()[_Phone_numbersDAL.GetPhoneNumbers().Count() - 1].Id;
                //הוספת הליד
                List<PhoneNumbers> l = _Phone_numbersDAL.GetPhoneNumbers();
                //הוספת ליד לטלפנית מסויימת     
                calls = new CallsDTO();
        
                calls.TranscriptCall = null;
                calls.IdTelephonist = telephonistCompaniesDTOList[k].Id;
                calls.IdPhoneNumber = id1;
                calls.DateCall = DateTime.UtcNow;
                calls.TimeCall = date1.AddMinutes(time_call++ * 10);
                

                calls.Done = 0;
                List<CallsDTO> callsDTOs = _callsBL.Add_call_for_telephonnist(calls);
               Calls c2  = _callsDAL.GetAllCals()[_callsDAL.GetAllCals().Count() - 1];
                calls = iMapper.Map<Calls, CallsDTO>(c2);
                int id2 = _callsDAL.GetAllCals()[_callsDAL.GetAllCals().Count - 1].Id;
                leadsAndCallsDTO = new LeadsAndCallsDTO();
                leadsAndCallsDTO.countcall = 0;
                leadsAndCallsDTO.DateCall = null;
                leadsAndCallsDTO.IdCall = id2;
                leadsAndCallsDTO.leadAddress = o.Address;
                leadsAndCallsDTO.leadId = id1;
                leadsAndCallsDTO.leadMail = o.Mail;
                leadsAndCallsDTO.leadFirstName = o.FirstName;
                leadsAndCallsDTO.leadLastName = o.LastName;
                leadsAndCallsDTO.leadCity = o.City;
                leadsAndCallsDTO.leadCreationDate = o.CreationDate;
                leadsAndCallsDTO.leadFirstName = o.FirstName;
                leadsAndCallsDTO.leadPhoneNumber = o.Phone;
                leadsAndCallsDTO.leadStatus = o.Status;
                leadsAndCallsDTO.leadType = o.type;
                leadsAndCallsDTO.leadTz = o.Tz;
                leadsAndCallsDTO.TimeCall = calls.TimeCall;
                leadsAndCallsDTO.IdTelephonist_c = (int)calls.IdTelephonist;
                leadsAndCallsDTO.leadPhone2 = o.Phone2;
                leadsAndCallsDTO.leadPlaceWorking = o.PlaceWorking;
                leadsAndCallsDTO.Done = 0;
                leadsAndCallsDTOs.Add(leadsAndCallsDTO);
                sum--;
            }
            return leadsAndCallsDTOs;
        }
        //החזרת כל הלדים של טלפנית
        public List<Phone_numbersDTO> getLeadForTelephonist(int idtelephonist)
        {
            List<LeadsAndCallsDTO> callsAndPhones = _callsBL.get_All_calls_for_telephonist(idtelephonist);
            List<Phone_numbersDTO> phone_numbersDTOs = new List<Phone_numbersDTO>();
            foreach (var item in callsAndPhones)
            {
                if (item.leadType == (int)etype_phone.ליד)
                {
                    phone_numbersDTOs.Add(GetPhone_number(item.leadId));
                }
            }
            return phone_numbersDTOs;
        }
        //החזרת כל הלדים של טלפנית לחודש זה
        public List<Phone_numbersDTO> getLeadForMonthForTelephonist(int idtelephonist)
        {
            List<LeadsAndCallsDTO> callsAndPhones = _callsBL.get_All_calls_for_telephonist(idtelephonist);
            List<Phone_numbersDTO> phone_numbersDTOs = new List<Phone_numbersDTO>();
            foreach (var item in callsAndPhones)
            {
                if (item.leadType == (int)etype_phone.ליד && item.leadCreationDate.Value.Year == DateTime.Today.Year && item.leadCreationDate.Value.Month == DateTime.Today.Month)
                {
                    phone_numbersDTOs.Add(GetPhone_number(item.leadId));
                }
            }
            return phone_numbersDTOs;
        }
        //החזרת כל הלדים של טלפנית ליום זה
        public List<Phone_numbersDTO> getLeadForDayForTelephonist(int idtelephonist)
        {
            List<LeadsAndCallsDTO> callsAndPhones = _callsBL.get_All_calls_for_telephonist(idtelephonist);
            List<Phone_numbersDTO> phone_numbersDTOs = new List<Phone_numbersDTO>();
            foreach (var item in callsAndPhones)
            {
                if(item.leadCreationDate.Value.Month==6&& item.leadCreationDate.Value.Day==22)
                if (item.leadType == (int)etype_phone.ליד && item.leadCreationDate.Value.Date == DateTime.Today.Date)
                {
                    phone_numbersDTOs.Add(GetPhone_number(item.leadId));
                }
            }
            return phone_numbersDTOs;
        }
        //החזרת כל הלידים לחודש זה של חברה מסויימת 
        public List<Phone_numbersDTO> getLeadForMonthForCompany(int idCompany)
        {
            List<Phone_numbersDTO> phone_NumberList = GetAll();
            List<Phone_numbersDTO> phone_numbersDTOs = new List<Phone_numbersDTO>();
            foreach (var item in phone_NumberList)
            {
                if (item.type == (int)etype_phone.ליד && item.CreationDate.Value.Date == DateTime.Today.Date && item.IdCompanies == idCompany)
                {
                    phone_numbersDTOs.Add(item);
                }
            }
            return phone_numbersDTOs;
        }
        //החזרת כל הלידים של חברה מסויימת 
        public List<Phone_numbersDTO> getAllLeadForCompany(int idCompany)
        {
            List<Phone_numbersDTO> phone_NumberList = GetAll();
            List<Phone_numbersDTO> phone_numbersDTOs = new List<Phone_numbersDTO>();
            foreach (var item in phone_NumberList)
            {
                if (item.type == (int)etype_phone.ליד && item.IdCompanies == idCompany)
                {
                    phone_numbersDTOs.Add(item);
                }
            }
            return phone_numbersDTOs;
        }
        //עדכון טלפון
        public Phone_numbersDTO Update_phone(Phone_numbersDTO phone_)
        {
            PhoneNumbers phone = iMapper.Map<Phone_numbersDTO, PhoneNumbers>(phone_);
            _Phone_numbersDAL.updatePhoneNumber(phone);
            List<PhoneNumbers> phone_Numbers = _Phone_numbersDAL.GetPhoneNumbers();
            phone = phone_Numbers.FirstOrDefault(x => x.Id == phone_.Id);
            phone_ = iMapper.Map<PhoneNumbers, Phone_numbersDTO>(phone);
            return phone_;
        }
        //הוספת מספר טלפון למערכת והוספת שיחה לטלפנית
        public Phone_numbersDTO Add_phone(Phone_numbersDTO phone_, int id)
        {
            PhoneNumbers phone = iMapper.Map<Phone_numbersDTO, PhoneNumbers>(phone_);
            phone.Status =(int) estatus.לא_טופל;
            _Phone_numbersDAL.AddPhoneNumbers(phone);
            List<PhoneNumbers> phone_Numbers = _Phone_numbersDAL.GetPhoneNumbers();
            phone = phone_Numbers[phone_Numbers.Count - 1];
            DateTime date = new DateTime();
            date = DateTime.UtcNow;
            date = date.AddHours(3);
            phone_ = iMapper.Map<PhoneNumbers, Phone_numbersDTO>(phone);
            CallsDTO call = new CallsDTO();
            call.DateCall = date;
            call.TimeCall = date;
            call.TranscriptCall = null;
            call.IdTelephonist = id;
            call.IdPhoneNumber = phone_.Id;
            call.TranscriptCall = "";
            call.Done = 0;
            _callsBL.Add_call_for_telephonnist(call);
            return phone_;
        }

        //החזרת כל הלדים של טלפנית
        public List<Phone_numbersDTO> getLeadForTelephonistInCompany(int idtelephonist)
        {
            List<LeadsAndCallsDTO> callsAndPhones = _callsBL.get_All_calls_for_telephonist(idtelephonist);
            List<Phone_numbersDTO> phone_numbersDTOs = new List<Phone_numbersDTO>();
            Phone_numbersDTO phone_NumbersDTO;

            foreach (var item in callsAndPhones)
            {
                if (item.leadType == (int)etype_phone.ליד)
                {
                    if (!phone_numbersDTOs.Any(l => l.Id == item.leadId))
                    {
                        phone_NumbersDTO = new Phone_numbersDTO();
                        phone_NumbersDTO = GetPhone_number(item.leadId);
                        phone_numbersDTOs.Add(phone_NumbersDTO);
                    }
                }
            }
            return phone_numbersDTOs;
        }

        //החזרת כל מספרי הטלפון שהטלפנית ייצרה של טלפנית
        public List<Phone_numbersDTO> getPhoneForTelephonistInCompany(int idtelephonist)
        {
            List<LeadsAndCallsDTO> callsAndPhones = _callsBL.get_All_calls_for_telephonist(idtelephonist);
            List<Phone_numbersDTO> phone_numbersDTOs = new List<Phone_numbersDTO>();
            Phone_numbersDTO phone_NumbersDTO;

            foreach (var item in callsAndPhones)
            {
                if (item.leadType == (int)etype_phone.מהטלפנית)
                {
                    if (!phone_numbersDTOs.Any(l => l.Id == item.leadId))
                    {
                        phone_NumbersDTO = new Phone_numbersDTO();
                        phone_NumbersDTO = GetPhone_number(item.leadId);
                        phone_numbersDTOs.Add(phone_NumbersDTO);
                    }
                }
            }
            return phone_numbersDTOs;
        }
        //החזרת כל מבפרי הטלפון שנכנסו אלינו והטלפנית  אחראית עליהם
        public List<Phone_numbersDTO> getPhoneComForTelephonistInCompany(int idtelephonist)
        {
            List<LeadsAndCallsDTO> callsAndPhones = _callsBL.get_All_calls_for_telephonist(idtelephonist);
            List<Phone_numbersDTO> phone_numbersDTOs = new List<Phone_numbersDTO>();
            Phone_numbersDTO phone_NumbersDTO;

            foreach (var item in callsAndPhones)
            {
                if (item.leadType == (int)etype_phone.נכנס_אלינו)
                {
                    if (!phone_numbersDTOs.Any(l => l.Id == item.leadId))
                    {
                        phone_NumbersDTO = new Phone_numbersDTO();
                        phone_NumbersDTO = GetPhone_number(item.leadId);
                        phone_numbersDTOs.Add(phone_NumbersDTO);
                    }
                }
            }
            return phone_numbersDTOs;
        }

        //החזרת כללל מספרי הטלפון
        public List<Phone_numbersDTO> getAllPhoneForTelephonistInCompany(int idtelephonist)
        {
            List<LeadsAndCallsDTO> callsAndPhones = _callsBL.get_All_calls_for_telephonist(idtelephonist);
            List<Phone_numbersDTO> phone_numbersDTOs = new List<Phone_numbersDTO>();
            Phone_numbersDTO phone_NumbersDTO;
            foreach (var item in callsAndPhones)
            {
                if (!phone_numbersDTOs.Any(l => l.Id == item.leadId))
                {
                    phone_NumbersDTO = new Phone_numbersDTO();
                    phone_NumbersDTO = GetPhone_number(item.leadId);
                    phone_numbersDTOs.Add(phone_NumbersDTO);
                }

            }
            return phone_numbersDTOs;

        }
    }
}

