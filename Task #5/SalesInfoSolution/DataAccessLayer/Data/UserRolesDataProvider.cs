using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities;
using User = DataAccessLayer.Models.UserRoleModels.User;
using Role = DataAccessLayer.Models.UserRoleModels.Role;

namespace DataAccessLayer.Data
{
    public class UserRolesDataProvider : IUserRolesDataProvider
    {
        private UserRolesDBEntities _userRolesEntities;
        private ICollection<Model.Entities.User> _usersEntitiesList;

        public UserRolesDataProvider()
        {
            _userRolesEntities = new UserRolesDBEntities();
        }


        #region IUserRolesDataProvider implementation

        public IEnumerable<User> GetUsers()
        {
            _usersEntitiesList = _userRolesEntities.Users.ToList();
            return _usersEntitiesList.Select(u => UserEntityToModel(u));
        }

        public IEnumerable<Role> GetRoles()
        {
            var roles = _userRolesEntities.Roles.ToList();
            return roles.Select(r => RoleEntityToModel(r));
        }

        public User AddNewUser(string name, Role role)
        {
            var entityUser = new Model.Entities.User()
            {
                UserName = name
            };

            var entityUserRole = new Model.Entities.UserRole()
            {
                User = entityUser,
                RoleID = role.Id
            };

            _userRolesEntities.Users.Add(entityUser);
            _userRolesEntities.UserRoles.Add(entityUserRole);
            _userRolesEntities.SaveChanges();

            return UserEntityToModel(entityUser);
        }

        public void ChangeUserRole(User user, Role newRole)
        {
            var userRole = _userRolesEntities.UserRoles.FirstOrDefault(uR => uR.UserID == user.Id);
            if(userRole == null)
                throw new ArgumentException("Cannot find roles for current user in data base");

            userRole.RoleID = newRole.Id;
            _userRolesEntities.SaveChanges();

            user.SetRole(newRole);
        }

        public void DeleteUser(User user)
        {
            var entityUser = _usersEntitiesList.FirstOrDefault(u => u.UserID == user.Id);
            if (entityUser == null)
                throw new ArgumentException("Cannot find current user in data base");

            _userRolesEntities.Users.Remove(entityUser);
            _userRolesEntities.SaveChanges();

            _usersEntitiesList.Remove(entityUser);
        }

        #endregion


        #region Entities to object

        private User UserEntityToModel(Model.Entities.User user)
        {
            var objectRole = user.UserRoles.Any()
                ? RoleEntityToModel(user.UserRoles.First().Role)
                : null;

            return new User(user.UserID, user.UserName, objectRole);
        }

        private Role RoleEntityToModel(Model.Entities.Role role)
        {
            return new Role(role.RoleID, role.RoleName);
        }

        #endregion
    }
}
