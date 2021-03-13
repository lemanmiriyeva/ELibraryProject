using ELibrary.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Entities.Concrete
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string Author { get; set; }
        public int BookCount { get; set; }
        public string Category { get; set; }
        public decimal RentalPrice { get; set; }
        public string Language { get; set; }
        public int Size { get; set; }
    }
}
