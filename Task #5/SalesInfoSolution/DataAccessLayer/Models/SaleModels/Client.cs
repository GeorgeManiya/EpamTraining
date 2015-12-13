namespace DataAccessLayer.Models.SaleModels
{
    public class Client
    {
        public Client(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; private set; }
        public string Name { get; private set; }

        public void SetId(long id)
        {
            Id = id;
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
