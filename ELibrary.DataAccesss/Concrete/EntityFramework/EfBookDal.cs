using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibrary.DataAccesss.Abstract;
using ELibrary.DataAccesss.Concrete.EntityFramework.Contexts;
using ELibrary.Entities.Concrete;

namespace ELibrary.DataAccesss.Concrete.EntityFramework
{
    public class EfBookDal : EfEntityRepositoryBase<Book, ELibraryContext>, IBookDal
    {
    }
}
