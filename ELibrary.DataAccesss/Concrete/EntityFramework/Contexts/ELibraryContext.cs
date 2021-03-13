using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibrary.DataAccesss.Concrete.EntityFramework.Mapping;
using ELibrary.Entities.Concrete;

namespace ELibrary.DataAccesss.Concrete.EntityFramework.Contexts
{
    public class ELibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BookMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
        }
    }
}
