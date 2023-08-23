using Bookstore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Bookstore.Permissions;

public class BookstorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BookstorePermissions.GroupName, L("Permission:BookStore"));
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BookstorePermissions.MyPermission1, L("Permission:MyPermission1"));
        var bookPermission = myGroup.AddPermission(BookstorePermissions.Book.Default, L("Permission:Books"));
        bookPermission.AddChild(BookstorePermissions.Book.Create, L("Permission:Books.Create"));
        bookPermission.AddChild(BookstorePermissions.Book.Edit, L("Permission:Books.Edit"));
        bookPermission.AddChild(BookstorePermissions.Book.Delete, L("Permission:Books.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookstoreResource>(name);
    }
}
