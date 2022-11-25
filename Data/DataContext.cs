using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)                      //we use this to seed data in the database
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill {Id = 1, Name = "Fireball",Damage = 30},
                new Skill {Id = 2, Name = "Frenzy",Damage = 20},
                new Skill {Id = 3, Name = "Blizzard",Damage = 50}
                
            );
        }

        public DbSet<Character> Characters { get; set; }                            //allows us to query and save the data ... name of the attrib. is the name of the table
        public DbSet<User> Users {get; set;}
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}