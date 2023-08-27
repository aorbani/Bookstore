using Bookstore.Authors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Bookstore.EntityFrameworkCore.Authors
{
    public class EfCoreAuthorRepository: EfCoreRepository<BookstoreDbContext,Author,Guid>, IAuthorRepository
    {
        public EfCoreAuthorRepository(IDbContextProvider<BookstoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<Author> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(author => author.Name == name);

        }

        public async Task<List<Author>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        )
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    author => author.Name.Contains(filter)
                    )
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();

            //.OrderBy(sorting) -- not working as the return set is already sorted by default, check updates of this library


        }
    }
}
