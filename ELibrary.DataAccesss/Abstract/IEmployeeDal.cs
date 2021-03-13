using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibrary.Entities.Concrete;

namespace ELibrary.DataAccesss.Abstract
{
    public interface IEmployeeDal : IEntityRepository<Employee>
    {
    }
}
