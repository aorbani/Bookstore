using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Bookstore.Authors
{
    public class Author :FullAuditedAggregateRoot<Guid>
    {
        public string Name { private set;  get; }
        public DateTime BirthDate { set; get; }
        public string ShortBio { set; get; }
        public Author() { }
        
        internal Author(Guid id,
            [NotNull] string name,
            DateTime birthDate, string shortBio):base(id)
        {
            SetName(name);
            BirthDate = birthDate;
            ShortBio = shortBio;
        }
        internal Author ChangeName(string name)
        {
            SetName(name);
            return this;
        }
        private void SetName(string name)
        {
            Name = Check.NotNullOrEmpty(name,nameof(name), maxLength:AuthorConsts.MaxNameLength);
        }
    }
}
