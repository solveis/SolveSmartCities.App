//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SolveChicago.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Communication
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string UserId { get; set; }
        public Nullable<bool> Success { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
