using Bookstore.Authors;
using Bookstore.Books;
using Microsoft.AspNetCore.Identity;
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
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public BookStoreDataSeederContributor(IRepository<Book, Guid> bookRepository, IAuthorRepository authorRepository, AuthorManager authorManager)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
             _authorManager = authorManager;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _bookRepository.GetCountAsync() > 0)
            {
                return;
            }
            
                var hashem = await _authorRepository.InsertAsync(
                    await _authorManager.CreateAsync(
                        "Hashem AlGhaili",
                        new DateTime(1997, 08, 17),
                        "Great Scientist")

                );
                var aseel = await _authorRepository.InsertAsync(
                    await _authorManager.CreateAsync(
                        "Aseel AlOrbani",
                        new DateTime(1997, 08, 17),
                        "Great Engineer")

                );
            
              await _bookRepository.InsertAsync(
                    new Book { 
                        AuthorId= hashem.Id,
                        Name = "1984", 
                        Price = 10.5f, 
                        Type = BookType.Fantastic, 
                        PublishDate = new DateTime(1997, 8, 17) 
                    }, autoSave:true);
                await _bookRepository.InsertAsync(
                    new Book
                    {
                        AuthorId=aseel.Id,
                        Name = "DDD Pattern",
                        Price = 18.5f,
                        Type = BookType.Science,
                        PublishDate = new DateTime(1990, 10, 10)
                    }, autoSave: true);
            
            
        }
    }
}
