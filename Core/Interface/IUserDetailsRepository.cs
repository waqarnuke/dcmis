using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interface;

public interface IUserDetailsRepository : IRepository<UserDetails>
{
    //void UpdateUserDetail(UserDetails userDetails);   
    bool IsExists(int id);
    Task<bool> SaveChangesAsync();
    // Task<IReadOnlyList<UserDetails>> GetUserDetailsAsync();
    // Task<UserDetails?> GetUserDetailByIdAsync(int id);
    // Task<UserDetails?> GetUserDetailByUserIdAsync(string userId);
    // void AddUserDetail(UserDetails userDetails);
    // void UpdateUserDetail(UserDetails userDetails);
    // void DeleteUserDetail(UserDetails userDetails);
    // bool UserDetailExists(int id);
    // Task<bool> SaveChangesAsync();
}
