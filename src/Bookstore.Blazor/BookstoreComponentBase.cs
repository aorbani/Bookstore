using Bookstore.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Bookstore.Blazor;

public abstract class BookstoreComponentBase : AbpComponentBase
{
    protected BookstoreComponentBase()
    {
        LocalizationResource = typeof(BookstoreResource);
    }
}
