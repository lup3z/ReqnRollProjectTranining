using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectAPI.Support
{
    public class GetBookingData
    {
            public string firstname { get; set; }
            public string lastname { get; set; }
            public int totalprice { get; set; }
            public bool depositpaid { get; set; }
            public Bookingdates bookingdates { get; set; }

        public class Bookingdates
        {
            public string checkin { get; set; }
            public string checkout { get; set; }
        }

    }
}
