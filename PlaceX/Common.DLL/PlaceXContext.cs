using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.DLL.Entities;

namespace Common.DLL
{
    public class PlaceXContext : DbContext
    {
        public PlaceXContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Entity<User>().Property(x => x.DateOfBirth).HasColumnType("datetime2");
           modelBuilder.Entity<Location>().Property(x => x.DateOfPublishing).HasColumnType("datetime2");
           modelBuilder.Entity<Photo>().Property(x => x.DateOfPublishing).HasColumnType("datetime2");
           modelBuilder.Entity<Message>().Property(x => x.DateOfSending).HasColumnType("datetime2");
           modelBuilder.Entity<Comment>().Property(x => x.DateOfComment).HasColumnType("datetime2");

            base.OnModelCreating(modelBuilder);
        }
    }
}
