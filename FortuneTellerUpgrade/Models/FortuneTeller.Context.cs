﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FortuneTellerUpgrade.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FortuneTellerMVCEntities2 : DbContext
    {
        public FortuneTellerMVCEntities2()
            : base("name=FortuneTellerMVCEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<customer> Customers { get; set; }
        public virtual DbSet<Month> Months { get; set; }
    }
}
