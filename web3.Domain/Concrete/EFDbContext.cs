using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web3.Domain.Entities;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
namespace web3.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        
        public DbSet<Web_User> Users { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EntityTypeConfiguration<Web_User>().ToTable("userinfo"));
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
