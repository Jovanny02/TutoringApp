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
    
    public partial class EducationSection
    {
        public int UFID { get; set; }
        public string Major { get; set; }
        public string University { get; set; }
        public int fromYear { get; set; }
        public int toYear { get; set; }
    
        public virtual user user { get; set; }
    }
}
