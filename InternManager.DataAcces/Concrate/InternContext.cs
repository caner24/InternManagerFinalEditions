using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InternManager.Entities.Concrate
{
    public class InternContext : DbContext
    {
        public InternContext(DbContextOptions<InternContext> options)
    : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Boss> Bosses { get; set; }


        public DbSet<Intern1> Interns1 { get; set; }

        public DbSet<Intern2> Interns2 { get; set; }

        public DbSet<ISE> Ises { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Intern> Interns { get; set; }

        public DbSet<Kurum> Kurums { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
    }
}
