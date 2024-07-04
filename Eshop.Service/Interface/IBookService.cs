using Eshop.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Service.Interface
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book GetDetailsForBook(Guid? id);
        void CreateNewBook(Book p);
        void UpdeteExistingBook(Book p);
        void DeleteBook(Guid id);
    }
}
