using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
namespace MVCDreambox.Models
{
    public class DreamboxContext : DbContext
    {
        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<PaymentDummy> PaymentDummies { get; set; }

        public DbSet<MemberType> MemberTypes { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<MemberSubscription> MemberSubscriptions { get; set; }
    }
}