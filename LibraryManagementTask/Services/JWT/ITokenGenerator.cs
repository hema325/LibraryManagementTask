using LibraryManagementTask.Entities;

namespace LibraryManagementTask.Services.JWT
{
    public interface ITokenGenerator
    {
        string Generate(User user);
    }
}
