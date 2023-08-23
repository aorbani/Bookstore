using Bookstore.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Bookstore.Data
{
    public class BookStoreDataSeederContributor 
        :IDataSeedContributor,ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;

        public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() <= 0)
            {
                await _bookRepository.InsertAsync(
                    new Book { Name = "1984", 
                        Price = 10.5f, 
                        Type = BookType.Fantastic, 
                        PublishDate = new DateTime(1997, 8, 17) 
                    }, autoSave:true);
                await _bookRepository.InsertAsync(
                    new Book
                    {
                        Name = "DDD Pattern",
                        Price = 18.5f,
                        Type = BookType.Science,
                        PublishDate = new DateTime(1990, 10, 10)
                    }, autoSave: true);
            }
        }
    }
}
