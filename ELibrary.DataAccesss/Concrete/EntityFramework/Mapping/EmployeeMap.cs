using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibrary.Entities.Concrete;

namespace ELibrary.DataAccesss.Concrete.EntityFramework.Mapping
{
    public class EmployeeMap : EntityTypeConfiguration<Employee>
    {
        public EmployeeMap()
        {
            ToTable("Employees");

            HasKey(e => e.Id);

            Property(e => e.Id).HasColumnName("Id");
            Property(e => e.FirstName).HasColumnName("FirstName");
            Property(e => e.LastName).HasColumnName("LastName");
            Property(e => e.Age).HasColumnName("Age");
            Property(e => e.Position).HasColumnName("Position");
            Property(e => e.Salary).HasColumnName("Salary");
        }
    }
}
