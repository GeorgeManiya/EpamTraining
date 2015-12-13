namespace DataAccessLayer.Models.UserRoleModels
{
    public class Role
    {
        public Role(int id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }

        public int Id { get; private set; }
        public string RoleName { get; private set; }

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetRoleName(string roleName)
        {
            RoleName = roleName;
        }
    }
}
