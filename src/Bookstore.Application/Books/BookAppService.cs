﻿using Bookstore.Authors;
using Bookstore.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace Bookstore.Books
{

    [Authorize(BookstorePermissions.Book.Default)]
    public class BookAppService : 
        CrudAppService<Book,
            BookDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateBookDto>,
        IBookAppService
    {
        private readonly IAuthorRepository _authorRepository;
        public BookAppService(IRepository<Book, Guid> bookRepository, IAuthorRepository authorRepository) : base(bookRepository)
        {
            GetPolicyName = BookstorePermissions.Book.Default;
            GetListPolicyName = BookstorePermissions.Book.Default;
            CreatePolicyName = BookstorePermissions.Book.Create;
            UpdatePolicyName = BookstorePermissions.Book.Edit;
            DeletePolicyName = BookstorePermissions.Book.Delete;
            _authorRepository = authorRepository;
        }
        public override async Task<BookDto> GetAsync(Guid id)
        {
            var queryable = await Repository.GetQueryableAsync();
            var query = from book in queryable
                        join authors in await _authorRepository.GetQueryableAsync() on book.AuthorId equals authors.Id
                        where book.Id == id
                        select new { book, authors };
            var queryResult = await AsyncExecuter.FirstOrDefaultAsync(query);
            if (queryResult == null)
            {
                throw new EntityNotFoundException(typeof(Book), id);
            }
            var bookDto = ObjectMapper.Map<Book, BookDto>(queryResult.book);
            bookDto.AuthorName = queryResult.authors.Name;
            return bookDto;
        }
        public override async Task<PagedResultDto<BookDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await Repository.GetQueryableAsync();

            var query = from book in queryable
                        join authors in await _authorRepository.GetQueryableAsync() on book.AuthorId equals authors.Id
                        select new { book, authors };
            //.OrderBy(NormalizeSorting(input.Sorting))
            query = query
                
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var queryResult = await AsyncExecuter.ToListAsync(query);

            var bookDtos = queryResult.Select(x =>
            {
                var bookDto = ObjectMapper.Map<Book, BookDto>(x.book);
                bookDto.AuthorName = x.authors.Name;
                return bookDto;
            }).ToList();

            var totalCount = await Repository.GetCountAsync();

            return new PagedResultDto<BookDto>(
                totalCount,
                bookDtos
            );
        }

        public async Task<ListResultDto<AuthorLookupDto>> GetAuthorLookupAsync()
        {
            var authors = await _authorRepository.GetListAsync();

            return new ListResultDto<AuthorLookupDto>(
                ObjectMapper.Map<List<Author>, List<AuthorLookupDto>>(authors)
            );
        }
        private static string NormalizeSorting(string sorting)
        {
            if (sorting.IsNullOrEmpty())
            {
                return $"book.{nameof(Book.Name)}";
            }

            if (sorting.Contains("authorName", StringComparison.OrdinalIgnoreCase))
            {
                return sorting.Replace(
                    "authorName",
                    "author.Name",
                    StringComparison.OrdinalIgnoreCase
                );
            }

            return $"book.{sorting}";
        }

    }
}
