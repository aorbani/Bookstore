using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bookstore.Authors
{
    public class AuthorAppService_Tests : BookstoreApplicationTestBase
    {
        private readonly IAuthorAppService _authorAppService;

        public AuthorAppService_Tests()
        {
            _authorAppService = GetRequiredService<IAuthorAppService>();
        }
        [Fact]
        public async Task Should_Get_All_Authors_Without_Any_Filter()
        {
            var result = await _authorAppService.GetListAsync(new GetAuthorListDto());
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
            result.Items.ShouldContain(author => author.Name == "Aseel AlOrbani");
            result.Items.ShouldContain(author => author.Name == "Hashem AlGhaili");
        }
        [Fact]
        public async Task Should_Get_Filtered_Authors()
        {
            var result = await _authorAppService.GetListAsync(new GetAuthorListDto { Filter = "Aseel" });
            result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
            result.Items.ShouldContain(author => author.Name == "Aseel AlOrbani");
            result.Items.ShouldNotContain(author => author.Name == "Hashem AlGhaili");
        }
        [Fact]
        public async Task Should_Create_New_Author()
        {
            var result = await _authorAppService.CreateAsync(
                new CreateAuthorDto { 
                    Name="Ali AbdelAli",
                    BirthDate = new DateTime(1990,08,3), 
                    ShortBio="Great Man"
                });
            result.Id.ShouldNotBe(Guid.Empty);
            result.Name.ShouldBe("Ali AbdelAli");
        }
        [Fact]
        public async Task Should_Not_Create_Duplicate_Author()
        {
            await Assert.ThrowsAsync<AuthorAlreadyExistException>(async () =>
            {
                await _authorAppService.CreateAsync(
                    new CreateAuthorDto
                    {
                        Name = "Aseel AlOrbani",
                        BirthDate = DateTime.Now,
                        ShortBio = "..."
                    });
            });
        }
    }
}
