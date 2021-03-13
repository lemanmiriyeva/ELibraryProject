using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibrary.Entities.Concrete;

namespace ELibrary.Business.Abstract
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        List<Employee> GetByFirstName(string name);
        List<Employee> GetByLastName(string name);
        List<Employee> GetByAge(int min, int max);
        List<Employee> GetByPosition(string position);
        List<Employee> GetBySalary(decimal min, decimal max);
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
    }
}
