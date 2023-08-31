using Bookstore.Authors;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace Bookstore.Books
{
    public class BookAppService_Tests :BookstoreApplicationTestBase
    {
        private readonly IBookAppService _bookAppService;
        private readonly IAuthorAppService _authorAppService;
        public BookAppService_Tests()
        {
            _bookAppService = GetRequiredService<IBookAppService>();
            _authorAppService = GetRequiredService<IAuthorAppService>();
        }
        [Fact]
        public async Task Should_Get_List_Of_Books()
        {
            //act
            var result = await _bookAppService.GetListAsync(
                new PagedAndSortedResultRequestDto()
                );
            //Assert
            result.TotalCount.ShouldBeGreaterThan(0);
            result.Items.ShouldContain(b => b.Name == "1984" &&
                                    b.AuthorName == "Hashem AlGhaili");
        }
        [Fact]
        public async Task Should_Create_A_Valid_Book()
        {
            var authors = await _authorAppService.GetListAsync(new GetAuthorListDto());
            var firstAuthor = authors.Items.First();
            var result = await _bookAppService.CreateAsync(
                new CreateUpdateBookDto
                {
                    AuthorId=firstAuthor.Id,
                    Name = "New test book",
                    Price = 5f,
                    PublishDate = DateTime.Now,
                    Type = BookType.Horror
                }
            );
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("New test book");
        }
        [Fact]
        public async Task Should_Not_Create_A_Book_Without_Name()
        {
            var expection = await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _bookAppService.CreateAsync(
               new CreateUpdateBookDto
               {
                   Name = "",
                   Price = 5f,
                   PublishDate = DateTime.Now,
                   Type = BookType.Horror
               }
              );
            }
           
            );
            expection.ValidationErrors.ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
            
        }
    }
}
