using System;
using System.Collections.Generic;
using System.Linq;
using ELibrary.Business.Abstract;
using ELibrary.Business.Aspects.PostSharp.ValidationAspects.FluentValidationAspects;
using ELibrary.Business.CrossCuttingConcerns.Validation.FluentValidation;
using ELibrary.DataAccesss.Abstract;
using ELibrary.Entities.Concrete;

namespace ELibrary.Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        [NonSerialized]
        private readonly IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        public List<Employee> GetAll()
        {
            return _employeeDal.GetAll();
        }

        public List<Employee> GetByFirstName(string name)
        {
            return _employeeDal.GetAll(e => e.FirstName.Contains(name)).ToList();
        }

        public List<Employee> GetByLastName(string name)
        {
            return _employeeDal.GetAll(e => e.LastName.Contains(name)).ToList();
        }

        public List<Employee> GetByAge(int min, int max)
        {
            return _employeeDal.GetAll(e => e.Age >= min && e.Age <= max).ToList();
        }

        public List<Employee> GetByPosition(string position)
        {
            return _employeeDal.GetAll(e => e.Position.Contains(position)).ToList();
        }

        public List<Employee> GetBySalary(decimal min, decimal max)
        {
            return _employeeDal.GetAll(e => e.Salary >= min && e.Salary <= max).ToList();
        }

        public Employee GetById(int id)
        {
            return _employeeDal.Get(e => e.Id == id);
        }
        [FluentValidationAspect(typeof(EmployeeValidator))]
        public void Add(Employee employee)
        {
            _employeeDal.Add(employee);
        }
        [FluentValidationAspect(typeof(EmployeeValidator))]
        public void Update(Employee employee)
        {
            _employeeDal.Update(employee);
        }
        [FluentValidationAspect(typeof(EmployeeValidator))]
        public void Delete(Employee employee)
        {
            _employeeDal.Delete(employee);
        }
    }
}
