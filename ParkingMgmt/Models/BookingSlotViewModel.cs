using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingMgmt.Models
{
    public class BookingSlotViewModel
    {
        public int Id { get; set; }
        public string VehicleNumber { get; set; }
        public Nullable<System.DateTime> AllocatedDate { get; set; }
        public Nullable<System.TimeSpan> AllocatedTime { get; set; }
        public string VehicleType { get; set; }

        
    }
}