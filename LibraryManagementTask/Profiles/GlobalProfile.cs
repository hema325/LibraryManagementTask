using AutoMapper;
using LibraryManagementTask.Dtos.Books;
using LibraryManagementTask.Dtos.BorrowingRecords;
using LibraryManagementTask.Dtos.Patrons;
using LibraryManagementTask.Dtos.Users;
using LibraryManagementTask.Entities;

namespace LibraryManagementTask.Profiles
{
    public class GlobalProfile: Profile
    {
        public GlobalProfile()
        {
            //users
            CreateMap<User, UserDto>();
            CreateMap<User, AuthResultDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();

            //books
            CreateMap<Book, BookDto>();
            CreateMap<CreateOrUpdateBookDto, Book>();

            //patrons
            CreateMap<Patron, PatronDto>();
            CreateMap<CreateOrUpdatePatronDto, Patron>();

            //BorrowingRecords
            CreateMap<BorrowingRecord, BorrowingRecordDto>();
            CreateMap<CreateBorrowingRecordDto, BorrowingRecord>();
            CreateMap<ReturnBorrowingRecordDto, BorrowingRecord>();
        }
    }
}
