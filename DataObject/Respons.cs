using System;
using System.Collections.Generic;
using System.Text;

namespace DataObject
{
   public class Respons
    {
        public string text { get; set; }
        public DateTime dateCall { get; set; }
        public int type_m { get; set; }
        public Respons()
        {

        }
        public Respons(string text,DateTime date,int type_m)
        {
            this.text = text;
            this.dateCall = date;
            this.type_m = type_m;
        }
       
    }
}
