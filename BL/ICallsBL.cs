using System;
using System.Collections.Generic;
using System.Text;
using DataObject;
using DAL.models;
namespace BL
{
    public interface ICallsBL
    {
        public List<LeadsAndCallsDTO> get_calls_for_today(int id);
        public List<LeadsAndCallsDTO> get_calls_for_today_allcall(int id);
        public List<LeadsAndCallsDTO> get_no_answer__calls(int id);
        //public List<LeadsAndCallsDTO> Returned_from_the_system(int id);
        public List<LeadsAndCallsDTO> get_calls_of_a_certain_date(int id, DateTime date);
        //public LeadsAndCallsDTO get_callsLead_by_id(int id);
        public List< CallsDTO> Add_call_for_telephonnist(CallsDTO callsDTO);
        public LeadsAndCallsDTO get_callsLead_by_id(int id);
        public void DeleteCall(int id);
        public List<CallsDTO> get_all_calls_for_phone_and_telephonist(int id_telephonist, int id_phone);
        //החזרת תאריך פנוי על מנת לקבוע שיחה
        //מקבל טלפנית בחברה, מספר טלפון , כמות ימים להוספה מהיום לתאריך 
        public CallsDTO GetFreeDateCall(int id_telephonist, int day, int idphoone_number);

        //החזרת כל השיחות של טלפנית מסויימת בחברה
        public List<LeadsAndCallsDTO> get_All_calls_for_telephonist(int id);
        public List<LeadsAndCallsDTO> get_All_calls_for_telephonist1(int id);
        //החזרת כל השיחות של חברה מסויימת של כל הטלפניות(מקבל מספר חברה)
        public List<LeadsAndCallsDTO> get_All_calls_for_Company(int id);
        public List<CallsDTO> get_all_history_calls_for_phone_and_telephonist(int id_telephonist, int id_phone);
        public List<CallsDTO> get_all_future_calls_for_phone_and_telephonist(int id_telephonist, int id_phone);
        //עדכון שיחה לשעה והתאריך של היום
        public CallsDTO Update_call(CallsDTO call);
        public CallsDTO Update_call1(CallsDTO call);
        public CallsDTO get_call_by_id_call(int id);



    }
}
