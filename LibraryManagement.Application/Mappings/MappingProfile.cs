using AutoMapper;
using LibraryManagement.Model;
using LibraryManagement.Model.Entities;
using LibraryManagement.Model.Shows;

namespace LibraryManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, ShowBookDto>();
            CreateMap<AddBookDto, Book>();
            CreateMap<UpdateBookDto, Book>();
            CreateMap<Book, UpdateBookDto>();
            //CreateMap<>
        }
    }
}
