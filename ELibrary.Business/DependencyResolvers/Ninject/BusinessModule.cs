using System.Reflection;
using ELibrary.Business.Abstract;
using ELibrary.Business.Concrete;
using ELibrary.DataAccesss.Abstract;
using ELibrary.DataAccesss.Concrete.EntityFramework;
using Ninject.Modules;

namespace ELibrary.Business.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBookService>().To<BookManager>().InSingletonScope();
            Bind<IBookDal>().To<EfBookDal>().InSingletonScope();
            Bind<IEmployeeService>().To<EmployeeManager>().InSingletonScope();
            Bind<IEmployeeDal>().To<EfEmployeeDal>().InSingletonScope();

            var assembly = Assembly.GetExecutingAssembly();
        }
    }
}