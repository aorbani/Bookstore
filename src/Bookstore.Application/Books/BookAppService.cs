using Bookstore.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Bookstore.Books
{
    public class BookAppService : 
        CrudAppService<Book,
            BookDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBookDto>,
        IBookAppService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        public BookAppService(IRepository<Book, Guid> bookRepository) : base(bookRepository)
        {
            _bookRepository = bookRepository;
            GetPolicyName = BookstorePermissions.Book.Default;
            GetListPolicyName = BookstorePermissions.Book.Default;
            CreatePolicyName = BookstorePermissions.Book.Create;
            UpdatePolicyName = BookstorePermissions.Book.Edit;
            DeletePolicyName = BookstorePermissions.Book.Delete;

        }

    }
}
