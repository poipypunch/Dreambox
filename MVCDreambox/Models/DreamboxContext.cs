using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
        public DbSet<tbUser> tbUsers { get; set; }
        public DbSet<PackageMapping> PackageMappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<Channel>()
            //   .HasMany(c => c.packages).WithMany(i => i.Channels)
            //   .Map(t => t.MapLeftKey("ChannelID")
            //       .MapRightKey("PackageID")
            //       .ToTable("PackageMapping"));
        }
    }
}