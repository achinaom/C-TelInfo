using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;
namespace DataObject
{
    public enum eLevel
    {
        low=1,
        medium,
        high

    }
    public class AgreenentsAndRefusalWord : Word
    {
        public eLevel level { get; set; }
        public AgreenentsAndRefusalWord(string w, eType t, eLevel l) : base(w, t)
        {
            this.level = l;
        }
        public AgreenentsAndRefusalWord()
        {
                
        }
    }
}