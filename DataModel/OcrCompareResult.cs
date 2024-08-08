using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyStopwatch.DataModel
{
    public class OCRCompareResult<T>
    {
        public bool IsSuccess { get; set; }

        public T CompareTarget { get; set; }

        public string Info { get; set; }




        //create instance by calling the static method Create(...)
        private OCRCompareResult(bool success, string info, T target)
        {
            IsSuccess = success;
            Info = info;
            CompareTarget = target;
        }

        public static OCRCompareResult<T> Create(bool success, string info, T target)
        {
            return new OCRCompareResult<T>(success, info, target); 
        }
    }
}
