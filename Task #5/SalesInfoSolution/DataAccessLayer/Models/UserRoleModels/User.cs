namespace DataAccessLayer.Models.UserRoleModels
{
    public class User
    {
        public User(long id, string name, Role role)
        {
            Id = id;
            Name = name;
            Role = role;
        }

        public long Id { get; private set; }
        public string Name { get; private set; }
        public Role Role { get; private set; }

        public void SetId(long id)
        {
            Id = id;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetRole(Role role)
        {
            Role = role;
        }
    }
}
