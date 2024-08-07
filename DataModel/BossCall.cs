using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class BossCall : DataItem
    {
        //public DateTime? CountdownAt5Time { get; set; }
        //public DateTime? CountdownAt4Time { get; set; }

        //public int GroupId { get; set; }

        public BossCall()
        {
            
        }

        public DateTime MatchTimeFirst { get; set; }
        public DateTime MatchTimeSecond { get; set; }

        public bool IsValid { get; set; }
         

        public int OCRLastMatch { get; set; }

        public string OCRMatchList { get; set; }

         
    }
}
