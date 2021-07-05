using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using DataObject;
using AutoMapper;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Threading.Tasks;
using DAL.models;

namespace BL
{
    public class Text_Decoding
    {
        Time_unit Time_unit_change;
        bool degel_EX_T;
        static List<Word> Words_list;//הרשימה של כל המילים של מאגר המילים
        NumberWord number_w = new NumberWord();
        int cnt_true;//משתנה של רמות מילות הסרוב
        int cnt_false;//משתנה של רמות מילות ההסכמה
        DateTime dateCompare = new DateTime(2001, 01, 01, 00, 00, 00);//תאריך להשוואה
        DateTime next_call_date = new DateTime(2001, 01, 01, 00, 00, 00);//תאריך השיחה הבאה
        DateTime next_call_time = new DateTime(2001, 01, 01, 00, 00, 00);// זמן השיחה הבאה
        bool has_date;//היה תאריך
        bool has_time;//היה שעה
        bool find_word_dagel = false;//דגל למצב שמצאנו מילה מסוג זיהוי זמן(כמו בעוד
        bool degel = false;//להצהרה כי היה תאריך או שעה שהסקנו מהשיחה
        CallsDTO final_call = new CallsDTO();
        TimeWord time_word;
        DateTime date1 = new DateTime();
        IMapper iMapper;
        ICallsBL _callsBL;
        bool degel_was_answer = false;
        static bool degelRead=false;

        public Text_Decoding()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });
            iMapper = config.CreateMapper();
            if(!degelRead)
            readFromText();
            degelRead = true;
        }






        //קריאה מהקובץ טקסט
        public void readFromText()
        {
            Words_list = new List<Word>();
            AgreenentsAndRefusalWord agreenentsAndRefusalWord = new AgreenentsAndRefusalWord();
            agreenentsAndRefusalWord = null;
            TimeWord time_eord = new TimeWord();
            Word word = new Word();
            NumberWord numberWord = new NumberWord();
            using (StreamReader reader = System.IO.File.OpenText(@"C:\Users\1\Desktop\Word final.txt"))
            {
                string currentWord;
                while ((currentWord = reader.ReadLine()) != null)

                {
                    string[] wordarr1 = currentWord.Split(',');
                    if (wordarr1[1].Contains('0'))
                    {
                        agreenentsAndRefusalWord = new AgreenentsAndRefusalWord();
                        agreenentsAndRefusalWord = JsonConvert.DeserializeObject<AgreenentsAndRefusalWord>(currentWord);
                        Words_list.Add(agreenentsAndRefusalWord);
                    }
                    else if (wordarr1[1].Contains('1'))
                    {
                        agreenentsAndRefusalWord = new AgreenentsAndRefusalWord();
                        agreenentsAndRefusalWord = JsonConvert.DeserializeObject<AgreenentsAndRefusalWord>(currentWord);
                        Words_list.Add(agreenentsAndRefusalWord);
                    }
                    else if (wordarr1[1].Contains('2'))
                    {
                        time_eord = new TimeWord();
                        time_eord = JsonConvert.DeserializeObject<TimeWord>(currentWord);
                        Words_list.Add(time_eord);
                    }
                    //לא מוגדר
                    else if (wordarr1[1].Contains('3'))
                    {
                        word = new Word();
                        word = JsonConvert.DeserializeObject<Word>(currentWord);
                        Words_list.Add(word);
                    }
                    else if (wordarr1[1].Contains('4'))
                    {
                        numberWord = new NumberWord();
                        numberWord = JsonConvert.DeserializeObject<NumberWord>(currentWord);
                        Words_list.Add(numberWord);
                    }
                    else if (wordarr1[1].Contains('5'))
                    {
                        Identification_timeWord identification_TimeWord = new Identification_timeWord();
                        identification_TimeWord = JsonConvert.DeserializeObject<Identification_timeWord>(currentWord);
                        Words_list.Add(identification_TimeWord);
                    }
                }

            }
        }
        // עדכון תאריך או שעת השיחה
        public void set_next_call(int number, Etype_Identification_time etype)
        {
            DateTime t = DateTime.UtcNow;
            t = t.AddHours(3);//כדי להגיע לשעה הנוכחית
            if (etype == Etype_Identification_time.more || etype == Etype_Identification_time.will_com)
            {
                if (time_word.unit_to_add_type == Time_unit.minuts)
                {
                    next_call_time = t.AddMinutes((Double)number);
                    Time_unit_change = Time_unit.minuts;
                }
                else if (time_word.unit_to_add_type == Time_unit.hours)
                {
                    next_call_time = t.AddHours((Double)number);
                    Time_unit_change = Time_unit.hours;

                }
                else if (time_word.unit_to_add_type == Time_unit.month)
                {
                    next_call_date = t.AddMonths(number);
                    Time_unit_change = Time_unit.month;

                }
                else if (time_word.unit_to_add_type == Time_unit.week)
                {

                    next_call_date = t.AddDays(number * 7);
                    Time_unit_change = Time_unit.week;

                }
                else if (time_word.unit_to_add_type == Time_unit.year)
                {

                    next_call_date = t.AddYears(number);
                    Time_unit_change = Time_unit.year;

                }
                else if (time_word.unit_to_add_type == Time_unit.days)
                {
                    next_call_date = t.AddDays(number);
                    Time_unit_change = Time_unit.month;

                }
            }
            else
            {
                if (t.Hour > number)
                {
                    if (number + 12 > t.Hour)
                        number = number + 12;
                }

                if (etype == Etype_Identification_time.befor)
                {
                    next_call_time = new DateTime(2001, 01, 01, number - 1, 30, 00);
                    Time_unit_change = Time_unit.hours;

                }
                if (etype == Etype_Identification_time.after)
                {
                    next_call_time = new DateTime(2001, 01, 01, number, 30, 00);
                    Time_unit_change = Time_unit.hours;

                }
                if (etype == Etype_Identification_time.exactly)
                {
                    Time_unit_change = Time_unit.hours;
                    next_call_time = new DateTime(2001, 01, 01, number, 00, 00);
                }
                if (etype == Etype_Identification_time.between)
                {
                    Time_unit_change = Time_unit.hours;
                    next_call_time = new DateTime(2001, 01, 01, number, 00, 00);
                }
            }
        }
        //שינוי שיחה במקרה של שעה מדוייקת עם בוקר\ערב
        public void set_next_call(int number)
        {
            //אם כבר נקבעה שעה אז עכשיו נדע אז נזיז על פי בוקר או ערב
            if (next_call_time != dateCompare)
            {
                Time_unit_change = Time_unit.hours;
                if (time_word.text == "בוקר" && next_call_time.Hour > 12)
                    next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour - 12, next_call_time.Minute, next_call_time.Second);
                else if (next_call_time.Hour < 12 && time_word.text != "בוקר")
                    next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour + 12, next_call_time.Minute, next_call_time.Second);
                return;
            }
            else
            {
                //במקרה של-בשש בערב\בתשע בבוקר
                if (time_word.time_Type == Time_Type.time && degel_EX_T == true)
                {
                    Time_unit_change = Time_unit.hours;
                    if (time_word.text == "בוקר")
                        next_call_time = new DateTime(2001, 01, 01, number, 00, 00);
                    else if (time_word.text == "ערב" || time_word.text == "צהריים")
                        next_call_time = new DateTime(2001, 01, 01, number + 12, 00, 00);
                }
                //במקרה של בבוקר.\בערב ללא מספר מדוייק
                else if (time_word.time_Type == Time_Type.time && degel_EX_T == false)
                {
                    Time_unit_change = Time_unit.hours;
                    next_call_time = time_word.calltime;
                }
            }
        }
        //פיענוח הטקסט
        public Respons Decode(string Transcript)
        {
            int temp;
            degel_EX_T = false;//דגל למצב שהיה כתוב ב-תשע\בתשע בערב
            List<NumberWord> NumberWords_list = new List<NumberWord>();
            AgreenentsAndRefusalWord agreenentsAndRefusalWord;
            Identification_timeWord identification_TimeWord = new Identification_timeWord();
            int x;
            string[] arr_Transcript = Transcript.Split(' ');
            //identification_TimeWord = null;
            //מעבר על כל מילה בתמליל
            for (int i = 0; i < arr_Transcript.Length; i++)
            {
                string currentWord = arr_Transcript[i];
                string currentWord_without_begining = "";
                x = find_Word(arr_Transcript[i]);
                if (x == -1)
                {
                    // אם המילה מתחילה באות ב  אז נחפש את המילה ללא האות ב
                    if (currentWord[0] == 'ב' || currentWord[0] == 'כ' || currentWord[0] == 'ל' || currentWord[0] == 'ה' || currentWord[0] == 'מ' || currentWord[0] == 'ש')
                    {
                        for (int k = 1; k < currentWord.Length; k++)
                        {
                            currentWord_without_begining += currentWord[k];
                        }
                        x = find_Word(currentWord_without_begining);
                        if (x == -1)
                        {
                            continue;
                        }
                        //אם זה מסוג בארבע / בחמש
                        if (currentWord[0] == 'ב' && Words_list[x].word_type == eType.number)
                        {
                            NumberWord number = (NumberWord)Words_list[x];
                            if (number.type_number == Etype_number.number)
                            {
                                NumberWords_list.Add((NumberWord)Words_list[x]);
                                degel_EX_T = true;
                                if (i != arr_Transcript.Length - 1)
                                    continue;
                            }
                        }

                    }
                    else
                    {
                        if (degel_EX_T == true)
                        {
                            Identification_timeWord identification_TimeWord1 = new Identification_timeWord();
                            set_next_call(NumberWords_list[NumberWords_list.Count() - 1].number, Etype_Identification_time.exactly);
                            NumberWords_list.Remove(number_w);
                            degel_EX_T = false;
                            time_word = null;
                        }
                        continue;
                    }
                }

                //אם מצאנו מילה מהמאגרים-אז - נבדוק אם המילה הינה מסוג -לא מוגדר ואם כן אז נמשיך לעבור כל עוד זה לא מוגדר
                while (Words_list[x].word_type == eType.Not_set)
                {
                    if (i == arr_Transcript.Length - 1)
                        continue;
                    //אם המילה הינה מסוג לא מוגדר אז נחפש את המילה הזו עם המילה הבאה עד איפה שנמצא
                    x = find_Word_by_index(x + 1, Words_list[x].text + " " + arr_Transcript[i + 1]);
                    if (x == -1)
                        break;
                    i++;
                }
                if (x == -1)
                    continue;
                while (Words_list[x].word_type == eType.number)
                {
                    temp = x;
                    if (i == arr_Transcript.Length - 1)
                        break;
                    //אם המילה הינה מסוג מספר אז נחפש את המילה הזו עם המילה הבאה עד איפה שנמצא
                    x = find_Word_by_index(x + 1, Words_list[x].text + " " + arr_Transcript[i + 1]);
                    if (x == -1)
                    {
                        x = temp;
                        break;
                    }
                    else if (x != temp)
                    {
                        i++;

                        break;

                    }
                }

                if (x == -1)
                    continue;
               

                //אם המילה מסוג סרוב
                if (Words_list[x].word_type == eType.Refusal)
                {
                    agreenentsAndRefusalWord = (AgreenentsAndRefusalWord)Words_list[x];
                    cnt_false += (int)agreenentsAndRefusalWord.level;
                }
                //אם המילה מסוג הסכמה

                if (Words_list[x].word_type == eType.Agreement)
                {
                    
                    agreenentsAndRefusalWord = (AgreenentsAndRefusalWord)Words_list[x];
                    if (agreenentsAndRefusalWord.text == "לא עונה" || agreenentsAndRefusalWord.text == "לא עונים"||
                        agreenentsAndRefusalWord.text == "לא ענה" || agreenentsAndRefusalWord.text == "לא ענו"|| agreenentsAndRefusalWord.text == "לא ענתה")
                        degel_was_answer = true;
                    cnt_true += (int)agreenentsAndRefusalWord.level;
                }
                //אם המילה מסוג מספר נוסיף לרשימה של מספרים
                if (Words_list[x].word_type == eType.number)
                {
                    NumberWord number = (NumberWord)Words_list[x];
                    if (number.type_number == Etype_number.number)
                    {
                        NumberWords_list.Add((NumberWord)Words_list[x]);
                        if (identification_TimeWord!= null)
                        {
                            //לאחר שמצאנו מספר  נבדוק אם המספר היה צמוד למילה מסוג פתיחת תהליך זיהוי זמן- לדוגמא: לפני שש , לאחר תשע
                            if (NumberWords_list[NumberWords_list.Count() - 1].type_number == Etype_number.number && (identification_TimeWord.Etype_Identification == Etype_Identification_time.befor
                                || identification_TimeWord.Etype_Identification == Etype_Identification_time.after ||
                                identification_TimeWord.Etype_Identification == Etype_Identification_time.exactly))
                            {
                                set_next_call(NumberWords_list[NumberWords_list.Count() - 1].number, identification_TimeWord.Etype_Identification);
                                NumberWords_list.Remove(number_w);
                                find_word_dagel = false;
                                identification_TimeWord = null;
                                continue;
                            }
                            if (NumberWords_list[NumberWords_list.Count() - 1].type_number == Etype_number.number && (identification_TimeWord.Etype_Identification == Etype_Identification_time.between))
                            {
                                if (NumberWords_list.Count >= 2)
                                {
                                    int num;
                                    if (NumberWords_list[NumberWords_list.Count() - 2].number > 10)
                                        num = NumberWords_list[NumberWords_list.Count() - 2].number + (NumberWords_list[NumberWords_list.Count() - 1].number / 2);
                                    else
                                        num = (NumberWords_list[NumberWords_list.Count() - 1].number + NumberWords_list[NumberWords_list.Count() - 2].number) / 2;
                                    set_next_call(num, Etype_Identification_time.between);
                                    //לשנות שעה
                                }
                            }
                        }
                    }
                    //מסוג וחצי ורבע
                    else if (number.type_number == Etype_number.not_set)
                    {


                        int t;
                        if (number.text == "וחצי")
                            //אם השינוי האחרון היה בימים
                            if (Time_unit_change == Time_unit.days)
                            {
                                if (next_call_time.Hour + 12 >= 24)
                                    next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour + 12 - 24, next_call_time.Minute, next_call_time.Second);
                                else
                                    next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour + 12, next_call_time.Minute, next_call_time.Second);

                            }

                            else if (Time_unit_change == Time_unit.hours)
                            {
                                if (next_call_time.Minute + 30 >= 60)
                                    next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour + 1, next_call_time.Minute + 30 - 60, next_call_time.Second);
                                else
                                    next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour, next_call_time.Minute + 30, next_call_time.Second);


                            }

                            else if (Time_unit_change == Time_unit.minuts)
                            {
                                if (next_call_time.Second + 30 >= 60)
                                    next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour, next_call_time.Minute + 1, next_call_time.Second + 30 - 60);
                                else
                                    next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour, next_call_time.Minute, next_call_time.Second + 30);
                            }
                            else if (Time_unit_change == Time_unit.month)
                            {
                                if (next_call_date.Day + 15 >= 30)
                                    next_call_date = new DateTime(next_call_date.Year, next_call_date.Month + 1, next_call_date.Day + 15 - 30, 00, 00, 00);
                                else
                                    next_call_date = new DateTime(next_call_date.Year, next_call_date.Month, next_call_date.Day + 15, 00, 00, 00);
                            }
                            else if (Time_unit_change == Time_unit.week)
                            {
                                if (next_call_date.Day + 3 >= 30)
                                    next_call_date = new DateTime(next_call_date.Year, next_call_date.Month + 1, next_call_date.Day + 3 - 30, 0, 0, 0);
                                else
                                    next_call_date = new DateTime(next_call_date.Year, next_call_date.Month, next_call_date.Day + 3, 0, 0, 0);

                            }
                            else if (Time_unit_change == Time_unit.year)
                            {
                                if (next_call_date.Month + 6 >= 12) { 
                                    if(next_call_date.Month + 6 - 12!=0)
                                    next_call_date = new DateTime(next_call_date.Year + 1, next_call_date.Month + 6 - 12, next_call_date.Day, 0, 0, 0);
                              else
                                        next_call_date = new DateTime(next_call_date.Year + 1, 1, next_call_date.Day, 0, 0, 0);

                                }
                                else
                                    next_call_date = new DateTime(next_call_date.Year, next_call_date.Month + 6, next_call_date.Day, 0, 0, 0);

                            }
                    }
                    if (number.text == "ורבע")
                    {
                        //אם השינוי האחרון היה בימים
                        if (Time_unit_change == Time_unit.days)
                            next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour + 15, next_call_time.Minute, next_call_time.Second);

                        else if (Time_unit_change == Time_unit.hours)
                        {
                            if (next_call_time.Minute + 15 >= 60)
                                next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour + 1, next_call_time.Minute + 15 - 60, next_call_time.Second);
                            else
                                next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour, next_call_time.Minute + 15, next_call_time.Second);


                        }

                        else if (Time_unit_change == Time_unit.minuts)
                            next_call_time = new DateTime(2001, 01, 01, next_call_time.Hour, next_call_time.Minute, next_call_time.Second + 15);
                        else if (Time_unit_change == Time_unit.week)
                            next_call_date = new DateTime(next_call_date.Year, next_call_date.Month, next_call_date.Day + 2, 00, 00, 00);
                        else if (Time_unit_change == Time_unit.month)
                            next_call_date = new DateTime(next_call_date.Year, next_call_date.Month, next_call_date.Day + 7, 00, 00, 00);
                        else if (Time_unit_change == Time_unit.year)
                            next_call_date = new DateTime(next_call_date.Year, next_call_date.Month + 3, next_call_date.Day, 00, 00, 00);
                    }
                }
                if (Words_list[x].word_type == eType.find_time)
                {
                    find_word_dagel = true;
                    identification_TimeWord = (Identification_timeWord)Words_list[x];
                    if (identification_TimeWord.Etype_Identification == Etype_Identification_time.will_com && time_word != null)
                    {
                        set_next_call(1, Etype_Identification_time.will_com);
                        time_word = null;
                        find_word_dagel = false;

                        identification_TimeWord = null;
                    }
                    continue;
                }
                // אם המילה הינה מסוג זמן
                if (Words_list[x].word_type == eType.time)
                {
                    time_word = (TimeWord)Words_list[x];
                    if (time_word.time_Type == Time_Type.add)
                    {
                        //אם המילה הינה ימים, דקו ת, חודשים, שנים וכן הלאה
                        //ואם זיהינו תבנית מסוג עוד 10 ימים, עוד עשר חודשים ...
                        if (time_word.unit_to_add_type != Time_unit.unknow && time_word.amount == 0 && find_word_dagel == true)
                        {
                            if (NumberWords_list.Count() != 0)
                            {//אם המספר הינו מסוד אחד, שתיים , שלוש, כמה ,מספר, ...
                                number_w = NumberWords_list[NumberWords_list.Count() - 1];

                                if (NumberWords_list[NumberWords_list.Count() - 1].type_number == Etype_number.number && identification_TimeWord.Etype_Identification == Etype_Identification_time.more)
                                {
                                    set_next_call(number_w.number, identification_TimeWord.Etype_Identification);
                                    NumberWords_list.Remove(number_w);
                                    find_word_dagel = false;
                                    identification_TimeWord = null;

                                }
                            }
                        }
                        //מסוג עוד שעה
                        if (time_word.unit_to_add_type != Time_unit.unknow && time_word.amount != 0 && find_word_dagel == true && identification_TimeWord.Etype_Identification == Etype_Identification_time.more)
                        {
                            set_next_call(time_word.amount, identification_TimeWord.Etype_Identification);
                            find_word_dagel = false;
                            identification_TimeWord = null;
                        }
                        if (time_word.unit_to_add_type != Time_unit.unknow && time_word.amount != 0)
                        {
                            set_next_call(time_word.amount, Etype_Identification_time.more);

                        }


                    }
                    //מסוג בוקר\ ערב\צהריים
                    if (time_word.time_Type == Time_Type.time && time_word.unit_to_add_type == Time_unit.unknow)
                    {
                        if (degel_EX_T == true)
                        {
                            set_next_call(NumberWords_list[NumberWords_list.Count() - 1].number);
                            NumberWords_list.Remove(number_w);
                            degel_EX_T = false;
                            time_word = null;
                            continue;
                        }
                        else
                        {
                            set_next_call(0);
                            time_word = null;
                            continue;

                        }
                    }
                    if (time_word.time_Type == Time_Type.date && time_word.unit_to_add_type == Time_unit.unknow)
                    {
                        if (degel_EX_T == true)
                        {
                            set_next_call(NumberWords_list[NumberWords_list.Count() - 1].number);
                            NumberWords_list.Remove(number_w);
                            degel_EX_T = false;
                            time_word = null;
                            continue;

                        }
                        else
                        {
                            set_next_call(0);
                            time_word = null;
                            continue;

                        }
                    }
                }
                if (degel_EX_T == true)
                {
                    Identification_timeWord identification_TimeWord1 = new Identification_timeWord();
                    set_next_call(NumberWords_list[NumberWords_list.Count() - 1].number, Etype_Identification_time.exactly);
                    NumberWords_list.Remove(number_w);
                    degel_EX_T = false;
                    time_word = null;
                }
            }
            Respons respons = new Respons();

            if (next_call_date != dateCompare || next_call_time != dateCompare)
             {
                //אם יש תאריך
                if (next_call_date != dateCompare)
                {
                    //אם נמצאה שעה
                    if (next_call_time != dateCompare)
                    {
                        date1 = new DateTime(next_call_date.Year, next_call_date.Month, next_call_date.Day, next_call_time.Hour, next_call_time.Minute, next_call_time.Second);
                        degel = true;
                        //respons = new Respons("המערכת זיהתה לפי תמלול השיחה כי אתה מעוניין לקבוע שיחה לתאריך ", date1);
                    }
                    //-אם לא נמצאה שעה
                    //נקבע לשעה העכשיות(כי הצלחנו לתפוס אותו עכשיו:)
                    else
                    {
                        DateTime t = DateTime.UtcNow.AddHours(3);
                        date1 = new DateTime(next_call_date.Year, next_call_date.Month, next_call_date.Day, t.Hour, t.Minute, t.Second);
                        degel = true;
                    }


                }
                //אם יש שעה בלי תאריך
                if (next_call_time != dateCompare && next_call_date == dateCompare)
                {
                    DateTime t = DateTime.UtcNow;
                    t = t.AddHours(3);//כדי להגיע לשעה הנוכחית

                    //קביעת שיחה להיום עם השעה שנתנו לנו
                    if (t.Hour <= next_call_time.Hour)
                        date1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, next_call_time.Hour, next_call_time.Minute, next_call_time.Second);
                    //קביעת שיחה למחר- כי עברה השעה להיום
                    else
                        date1 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day + 1, next_call_time.Hour, next_call_time.Minute, next_call_time.Second);
                }
                if (cnt_false >= 100 || (cnt_false > cnt_true))
                {
                    //2-קבע שיחה, אשפה
                    respons = new Respons("המערכת זיהתה לפי תמלול השיחה כי הלקוח לא מעוניין כרגע, אך זיהתה יחד עם זאת אפשרות לשיחה לתאריך שלהל'ן, אם הינך מעוניין בשיחה לתאריך זה לחץ על קביעת  שיחה, אם הינך מעוניין לזריקת איש קשר זה לחץ על אשפה ", date1, 2);
                }
                //else if (cnt_true >= 100)
                // {
                //     //1-קבע שיחה, ביטול
                //     respons = new Respons(".המערכת זיהתה לפי תמלול השיחה כי הלקוח מעוניין בשיחה לתארך שלהלן', אם הינך מעוניין/נת לקבוע שיחה לתאריך זה לחץ על קבע שיחה, אחרת לחץ על ביטול", date1);
                // }
                else
                {
                    //1-קבע שיחה, ביטול
                    respons = new Respons(".המערכת זיהתה לפי תמלול השיחה כי הלקוח מעוניין בשיחה לתארך שלהלן', אם הינך מעוניין/נת לקבוע שיחה לתאריך זה לחץ על קבע שיחה, אחרת לחץ על ביטול", date1, 1);
                }


            }
            else
            {
                //
                if (cnt_true >= 100)
                {
                    date1 = DateTime.UtcNow;
                    //date1 = DateTime.UtcNow.AddHours(3);
                    date1 = date1.AddDays(1);
                    //2-קבע שיחה, ביטול
                    respons = new Respons(".המערכת זיהתה לפי תמלול השיחה כי הלקוח מעוניין בשיחה ,האם הינך מעוניין לקביעת שיחה למחר לתאריך שלהלן', אם הינך מעוניין/נת לקבוע שיחה לתאריך זה לחץ על קבע שיחה, אחרת לחץ על ביטול", date1, 1);
                }
                if (cnt_true > cnt_false)
                {
                    if (degel_was_answer == true)
                    {
                        date1 = DateTime.UtcNow;

                        //date1 = DateTime.UtcNow.AddHours(3);
                        date1 = date1.AddHours(2);
                        respons = new Respons(".המערכת זיהתה לפי תמלול השיחה כי הלקוח לא ענה לשיחה ,האם הינך מעוניין לקביעת שיחה לעוד שעתיים לתאריך שלהלן', אם הינך מעוניין/נת לקבוע שיחה לתאריך זה לחץ על קבע שיחה, אחרת לחץ על ביטול", date1, 1);

                    }
                    else {
                        date1 = DateTime.UtcNow;
                        //date1 = DateTime.UtcNow.AddHours(3);
                        date1 = date1.AddDays(10);
                    //2-קבע שיחה, ביטול
                    respons = new Respons(".המערכת זיהתה לפי תמלול השיחה כי הלקוח מעוניין בשיחה ,האם הינך מעוניין לקביעת שיחה לעוד עשרה ימים לתאריך שלהלן', אם הינך מעוניין/נת לקבוע שיחה לתאריך זה לחץ על קבע שיחה, אחרת לחץ על ביטול", date1, 1);
                    }
                }

                if (cnt_false > cnt_true)
                {
                    //סירוב קשה-קביעת שיחה לעוד חודשיים
                    if (cnt_false >= 100)
                    {
                        date1 = DateTime.UtcNow;
                        //date1 = date1.AddHours(3);
                        date1 = date1.AddMonths(3);
                        //2-קבע שיחה, אשפה
                        respons = new Respons(" המערכת זיהתה לפי תמלול השיחה כי האיש קשר אינו מעוניין בשיחה וביצירת קשר, האם בכל מקרה הנך מעונין בשיחה לעוד כשלושה חודשים לתאריך שלהלן, לאם הינך מעוניין/נת לקבוע שיחה לתאריך זה לחץ על קבע שיחה , אם הינך מעוניין לזריקת איש קשר זה לחץ על אשפה ", date1, 2);

                        //_callsBL.GetFreeDateCall(idTelephonist, 3, idPhoneNumber);
                    }
                    //השיחה זוהת כסירוב קל- קביעת שיחה לעוד חודש



                    else if (cnt_false < 100)
                    {
                        date1 = DateTime.UtcNow;
                        //date1 = date1.AddHours(3);
                        date1 = date1.AddMonths(1);
                        //1-קבע שיחה, אשפה
                        respons = new Respons(" המערכת זיהתה לפי תמלול השיחה כי הלקוח אינו מעוניין בתרומה, וכי הלקוח אינו מעוניין בשיחה, האם בכל מקרה הנך מעונין לקבוע שיחה לעוד חודש לתאריך שלהלן, אם הינך מעוניין/נת בקיבעת שיחה לתאריך זה לחץ על קבע שיחה , אם הינך מעוניין לזריקת איש קשר זה לחץ על אפשה ", date1, 2);
                        //_callsBL.GetFreeDateCall(idTelephonist, 3, idPhoneNumber);
                    }
                }



            }
            if (respons.text == null)
            {
                //אישור
                respons = new Respons(". לפי תמלול השיחה שנשלח -המערכת לא הצליחה לזהות נתונים לגבי השיחה , אנא נסה שוב עם תמלול יותר מוגדר  ", date1, 3);
            }
            else
            {
                if (respons.dateCall.Hour > 22 || respons.dateCall.Hour < 8)
                {
                    respons.text +=".הפרדה שים לב השעה שזוהתה אינה שעה המתאימה לשעות הלקוחות, נסה לשלוח תמלול שיתאים את השעות";
                }
            }
            return respons;

        }
        //חיפוש בינארי על מאגר המילים מאידקס מסויים
        public int find_Word_by_index(int index, string text)
        {
            int right = Words_list.Count() - 1;
            int left = index;
            int middle;
            while (left <= right)
            {
                middle = ((left + right) / 2);
                if (string.Compare(Words_list[middle].text, text) > 0)
                    right = middle - 1;
                else
                   if (string.Compare(Words_list[middle].text, text) < 0)
                    left = middle + 1;
                else return middle;
            }
            return -1;
        }

        //חיפוש בינארי על כל המארך מהתחלה
        public int find_Word(string text)
        {
            return (find_Word_by_index(0, text));
        }
    }
}

