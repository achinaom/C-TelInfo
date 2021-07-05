using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public enum Time_Type
    {
        add=1,
        date,
        time
    }

    public enum Time_unit
    {
        minuts=1,
        hours,
        days,
        week,
        month,
        year,
        unknow,
    }
    public class TimeWord : Word
    {
        public Time_Type time_Type { get; set; }
        public Time_unit unit_to_add_type { get; set; }
        public int amount { get; set; }
        public DateTime calldate { get; set; }
        public DateTime calltime { get; set; }
        public TimeWord(string w, eType t, Time_Type time_Type, Time_unit unit, int amount, DateTime calldate, DateTime calltime) : base(w, t)
        {
            this.time_Type = time_Type;
            this.unit_to_add_type = unit;
            this.amount = amount;
            this.calldate = calldate;
            this.calltime = calltime;
        }
        public TimeWord()
        {

        }
    }
}
