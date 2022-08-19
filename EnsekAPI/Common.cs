using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnsekAPI
{
    public class Common
    {
    }

    [Serializable]
    public class MeterReading
    {
        public int MeterReadingId { get; set; }

        public int AccountId { get; set; }

        public DateTime MeterReadingDateTime { get; set; }

        public int MeterReadValue { get; set; }




    }

    public class AccountInfo
    {


        public int AccountId { get; set; }
        public String FirstName { get; set; }

        public string LastName { get; set; }


    }

}
