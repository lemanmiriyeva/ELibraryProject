using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Transactions;
using ELibrary.Business.Abstract;
using ELibrary.Business.Aspects.PostSharp.ValidationAspects.FluentValidationAspects;
using ELibrary.Business.CrossCuttingConcerns.Caching.Microsoft;
using ELibrary.Business.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using ELibrary.Business.Utility;
using ELibrary.DataAccesss.Abstract;
using ELibrary.Entities.Concrete;
using ELibrary.Business.Aspects.Autofac.Caching;
using ELibrary.Business.CrossCuttingConcerns.Validation.FluentValidation;

namespace ELibrary.Business.Concrete
{
    public class BookManager : IBookService
    {
        [NonSerialized]
        private readonly IBookDal _bookDal;

        public BookManager(IBookDal bookDal)
        {
            _bookDal = bookDal;
        }

        public List<Book> GetAll()
        {
            return _bookDal.GetAll();
        }

        public List<Book> GetByName(string name)
        {
            return _bookDal.GetAll(b => b.BookTitle.Contains(name)).ToList();
        }

        public List<Book> GetBySize(int min, int max)
        {
            return _bookDal.GetAll(b => b.Size >= min && b.Size <= max).ToList();
        }

        public List<Book> SearchByAuthor(string name)
        {
            return _bookDal.GetAll(b => b.Author.Contains(name)).ToList();
        }


        public List<Book> GetByCount(int count)
        {
            return _bookDal.GetAll(b => b.BookCount == count).ToList();
        }

        public List<Book> GetByCategory(string category)
        {
            return _bookDal.GetAll(b => b.Category.Contains(category)).ToList();
        }

        public List<Book> GetByLanguage(string language)
        {
            return _bookDal.GetAll(b => b.Language.Contains(language)).ToList();
        }

        public List<Book> GetByPrice(decimal min, decimal max)
        {
            return _bookDal.GetAll(b => b.RentalPrice >= min && b.RentalPrice <= max).ToList();
        }


        public Book GetById(int id)
        {
            return _bookDal.Get(b => b.Id == id);
        }

        [FluentValidationAspect(typeof(BookValidator))]
        [CacheRemoveAspect("Get")]
        public void Add(Book book)
        {
           var result= Utilities.Run(CheckBookCountExceed(1700), CheckMaintenanceTime(0,6));
           if (!result) 
           {
               throw new Exception(Messages.BookCountExceed);
           }
           _bookDal.Add(book);
        }
        public void Update(Book book)
        {
            _bookDal.Update(book);
        }
        [FluentValidationAspect(typeof(BookValidator))]
        public void Delete(Book book)
        {
            _bookDal.Delete(book);
        }

        public void BookTransferring(Book book)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _bookDal.Delete(book);
                    //Business codes
                    _bookDal.Add(book);
                    scope.Complete();
                }
                catch
                {
                    // ignored
                }

                scope.Dispose();
               
            }

        }
        private bool CheckBookCountExceed(long count)
        {
            if (GetAll().Count > count)
            {
                return true;
            }

            return false;
        }

        private bool CheckMaintenanceTime(int min,int max)
        {
            if (DateTime.Now.Hour > min & DateTime.Now.Hour < max)
            {
                return true;
            }

            return false;
        }
    }
}
