using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public enum Etype_number
    {
        number=1,
        not_set
    }
    public class NumberWord:Word
    {
        public int number { get; set; }
        public Etype_number type_number { get; set; }
        public NumberWord(string w, eType t, int number,Etype_number e) : base(w, t)
        {
            type_number = e;
            this.type_number =Etype_number.number;
            this.number = number;
        }
        public NumberWord(string w, eType t, Etype_number type_Number) : base(w, t)
        {
            this.type_number = type_Number;
            this.number = 0;
        }
        public NumberWord()
        {
        }
    }
}
