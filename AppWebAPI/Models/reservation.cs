//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AppWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class reservation
    {
        public int tutorUFID { get; set; }
        public int studentUFID { get; set; }
        public System.DateTime fromDateTime { get; set; }
        public System.DateTime toDateTime { get; set; }
        public Nullable<bool> isCancelled { get; set; }
        public Nullable<double> tutorRating { get; set; }
        public Nullable<bool> isCompleted { get; set; }
        public Nullable<double> reservationPrice { get; set; }
        public Nullable<bool> paymentReceived { get; set; }
    
        public virtual user user { get; set; }
        public virtual user user1 { get; set; }
    }
}
