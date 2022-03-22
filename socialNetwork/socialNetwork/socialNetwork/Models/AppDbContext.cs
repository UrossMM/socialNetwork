using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Models
{
    public class AppDbContext: IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<GroupUser>()
                .HasOne(b => b.Group)
                .WithMany(ba => ba.GroupUsers)
                .HasForeignKey(bi => bi.GroupId);

            builder.Entity<GroupUser>()
                .HasOne(u => u.User)
                .WithMany(ua => ua.GroupUsers)
                .HasForeignKey(ui => ui.UserId);

           
        }

        //public DbSet<Users> AllUsers { get; set; } ovo ne treba jer koristimo aspnetusers
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
    }
}
