using AutoMapper;
using LibraryManagement.Application.DTOs.Loan;
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
            CreateMap<ShowLoanDto, Loan>().ReverseMap();
            CreateMap<LoanDto, Loan>().ReverseMap();
        }
    }
}
