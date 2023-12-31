﻿using AutoMapper;
using Bookstore.Authors;
using Bookstore.Books;

namespace Bookstore.Blazor;

public class BookstoreBlazorAutoMapperProfile : Profile
{
    public BookstoreBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<BookDto, CreateUpdateBookDto>();
        CreateMap<AuthorDto, UpdateAuthorDto>();
    }
}
