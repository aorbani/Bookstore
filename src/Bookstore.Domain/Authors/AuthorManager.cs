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
        private readonly IRepository<Author,Guid> _authorRepository;
        public AuthorManager(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task  CreateAsync(string name, DateTime birthDate, string shortBio=null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            if(await _authorRepository.FindAsync(i => i.Name == name) != null)
            {
                throw new AuthorAlreadyExistException(name);
            }
        }
    }
}
