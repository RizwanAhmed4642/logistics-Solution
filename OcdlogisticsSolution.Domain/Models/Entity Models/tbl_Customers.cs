//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OcdlogisticsSolution.DomainModels.Models.Entity_Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Customers
    {
        public string UserId { get; set; }
        public int CustomerTypeId { get; set; }
        public string DateJoined { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual tbl_CustomerType tbl_CustomerType { get; set; }
    }
}
