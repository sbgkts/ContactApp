//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContactApp.Models.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mail
    {
        public long MailID { get; set; }
        public string Mail1 { get; set; }
        public System.Guid PersonID { get; set; }
    
        public virtual People People { get; set; }
    }
}
