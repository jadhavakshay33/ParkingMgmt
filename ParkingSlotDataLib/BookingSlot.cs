//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ParkingSlotDataLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class BookingSlot
    {
        public int Id { get; set; }
        [Required]
        public string VehicleNumber { get; set; }
        public Nullable<System.DateTime> AllocatedDate { get; set; }
        public Nullable<System.TimeSpan> AllocatedTime { get; set; }
        [Required]
        public string VehicleType { get; set; }
        public Nullable<int> SlotNo { get; set; }
    }
}
