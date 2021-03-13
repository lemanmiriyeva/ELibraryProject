using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibrary.Entities.Concrete;

namespace ELibrary.Business.Abstract
{
    public interface IBookService
    {
        List<Book> GetAll();
        List<Book> GetByName(string name);
        List<Book> GetBySize(int min, int max);
        List<Book> SearchByAuthor(string name);
        List<Book> GetByCount(int count);
        List<Book> GetByCategory(string category);
        List<Book> GetByLanguage(string language);
        List<Book> GetByPrice(decimal min, decimal max);
        Book GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(Book book);


    }
}
