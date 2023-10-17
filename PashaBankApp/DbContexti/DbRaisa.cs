using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PashaBankApp.Models;

namespace PashaBankApp.DbContexti
{
    public class DbRaisa:IdentityDbContext<Manager,IdentityRole,string>
    {
        public DbRaisa(DbContextOptions<DbRaisa> ops) : base(ops) { }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<PersonalInfo> personalInfos { get; set; }
        public DbSet<ContactInfo> contactInfos { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<DistributorSale> distributorSales { get; set; }
        public DbSet<Error> errors { get; set; }
        public DbSet<Log> log { get; set; }
        public DbSet<Bonus> bonus { get; set; }
        public DbSet<Manager> manager { get; set; }
      
       

    }
}
