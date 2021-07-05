using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
    public enum Etype_Identification_time
    {
        befor=1,
        after,
        exactly,
        more,
        between,
        will_com

    }
  public  class Identification_timeWord:Word
    {
        public Etype_Identification_time Etype_Identification { get; set; }
        public Identification_timeWord(string w, eType t, Etype_Identification_time Etype_Identification) : base(w, t)
        {
            this.Etype_Identification = Etype_Identification;
        }
      
        public Identification_timeWord()
        {

        }
    }
}
