using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Visea_Expense_Manager.Models;

namespace Visea_Expense_Manager.Data
{
    public class MvcNoteContext : DbContext
    {
        public MvcNoteContext(DbContextOptions<MvcNoteContext> options)
    : base(options)
        {

        }

        public DbSet<Note> Note { get; set; }
        public DbSet<Zfile> Zfile { get; set; }
        public DbSet<Conger> Conger { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Formation> Formation { get; set; }
        public DbSet<Type_formation> Type_formation { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<Type_Mission> Type_Mission { get; set; }
        public DbSet<Etape_Mission> Etape_Mission { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<MonthValidation> MonthValidations { get; set; }
        public DbSet<Visea_Expense_Manager.Models.TypeEvent> TypeEvent { get; set; }
        public DbSet<Teletravail> teletravail { get; set; }

    }
}
