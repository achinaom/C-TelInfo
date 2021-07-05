using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public enum eType
    {
        Agreement,//0-הסכמה
        Refusal,//1-סרוב
        time,//2-מילת זמן
        Not_set,//3-לא עומד בפני עצמו
        number,//4-מספר
        find_time//5-פתיחת תהליך זיהוי זמן
        ,לא_מובן
    }
    public class Word
    {
        public string text { get; set; }
        public eType word_type { get; set; }
        public Word(string w, eType t)
        {
            this.text = w;
            this.word_type = t;
        }
        public Word()
        {

        }

    }

}
