using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ELibrary.Entities.Concrete;

namespace ELibrary.DataAccesss.Concrete.EntityFramework.Mapping
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            ToTable("Books");
            HasKey(b => b.Id);
            Property(b => b.Id).HasColumnName("Id");
            Property(b => b.BookTitle).HasColumnName("BookTitle");
            Property(b => b.Author).HasColumnName("Author");
            Property(b => b.BookCount).HasColumnName("BookCount");
            Property(b => b.Category).HasColumnName("Category");
            Property(b => b.RentalPrice).HasColumnName("RentalPrice");
            Property(b => b.Language).HasColumnName("Language");
            Property(b => b.Size).HasColumnName("Size");
        }
    }
}
