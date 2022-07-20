
using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        //TODO: ainda não está completamente implementado
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Company>?  Companies {get; set; }
        public DbSet<WorkDay>? WorkDays { get; set; } 
        public DbSet<WorkDaySchedule>? WorkDaySchedules { get; set; } 

        //responsavel por colocar em pratica os ajustes da pasta config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}