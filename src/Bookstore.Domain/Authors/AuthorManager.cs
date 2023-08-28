using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Bookstore.Authors
{
    public class AuthorManager : DomainService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<Author>  CreateAsync(string name, DateTime birthDate, string shortBio=null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            if (await _authorRepository.FindByNameAsync(name)!=null)
            {
                throw new AuthorAlreadyExistException(name);
            }
            return new Author(GuidGenerator.Create(), name, birthDate, shortBio);

        }
        public async Task ChangeNameAsync(
        [NotNull] Author author,
        [NotNull] string newName)
        {
            Check.NotNull(author, nameof(author));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingAuthor = await _authorRepository.FindByNameAsync(newName);
            if (existingAuthor != null && existingAuthor.Id != author.Id)
            {
                throw new AuthorAlreadyExistException(newName);
            }

            author.ChangeName(newName);
        }
    }
}
