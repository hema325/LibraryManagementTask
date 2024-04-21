using LibraryManagementTask.Data.Repositories._BaseRepo;
using LibraryManagementTask.Entities;

namespace LibraryManagementTask.Data.Repositories._PatronsRepo
{
    public class PatronsRepo : BaseRepo<Patron>, IPatronsRepo
    {
        public PatronsRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}
