using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Bookstore.Authors
{
    public class AuthorAlreadyExistException : BusinessException
    {
        public AuthorAlreadyExistException(string name) : base(BookstoreDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
