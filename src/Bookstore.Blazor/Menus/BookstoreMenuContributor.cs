﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bookstore.Localization;
using Bookstore.MultiTenancy;
using Volo.Abp.Account.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Blazor;
using Volo.Abp.SettingManagement.Blazor.Menus;
using Volo.Abp.TenantManagement.Blazor.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
using Bookstore.Permissions;

namespace Bookstore.Blazor.Menus;

public class BookstoreMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public BookstoreMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<BookstoreResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                BookstoreMenus.Home,
                l["Menu:Home"],
                "/",
                icon: "fas fa-home"
            )
        );
     
        var administration = context.Menu.GetAdministration();

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenus.GroupName, 3);
        var BookMenuItem = new ApplicationMenuItem("BookStore", l["Menu:BookStore"], icon: "fa fa-book");
        context.Menu.AddItem(BookMenuItem);
        if (await context.IsGrantedAsync(BookstorePermissions.Book.Default))
        {
            BookMenuItem.AddItem(new ApplicationMenuItem("BookStore.Books", l["Menu:Books"], url: "/books"));
        }
        if (await context.IsGrantedAsync(BookstorePermissions.Author.Default))
        {
            BookMenuItem.AddItem(new ApplicationMenuItem(
                "BookStore.Authors",
                l["Menu:Authors"],
                url: "/authors"
            ));
        }
        //return Task.CompletedTask;
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var accountStringLocalizer = context.GetLocalizer<AccountResource>();

        var authServerUrl = _configuration["AuthServer:Authority"] ?? "";

        context.Menu.AddItem(new ApplicationMenuItem(
            "Account.Manage",
            accountStringLocalizer["MyAccount"],
            $"{authServerUrl.EnsureEndsWith('/')}Account/Manage?returnUrl={_configuration["App:SelfUrl"]}",
            icon: "fa fa-cog",
            order: 1000,
            null).RequireAuthenticated());

        return Task.CompletedTask;
    }
}
