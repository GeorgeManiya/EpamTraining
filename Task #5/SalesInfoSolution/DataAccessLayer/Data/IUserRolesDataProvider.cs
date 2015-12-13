using DataAccessLayer.Models.UserRoleModels;
using System.Collections.Generic;

namespace DataAccessLayer.Data
{
    interface IUserRolesDataProvider
    {
        IEnumerable<User> GetUsers();
        IEnumerable<Role> GetRoles();
        User AddNewUser(string name, Role role);
        void ChangeUserRole(User user, Role newRole);
        void DeleteUser(User user);
    }
}
