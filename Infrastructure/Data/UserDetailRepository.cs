using Core.Entities;
using Core.Interface;

namespace Infrastructure.Data;

public class UserDetailsRepository(ApplicationContext context) : Repository<UserDetails>(context), IUserDetailsRepository
{
    public bool IsExists(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0 ;
    }
}
