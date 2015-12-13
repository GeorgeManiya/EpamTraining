namespace BusinessLogicLayer.Models.SaleModels
{
    public class Manager
    {
        public Manager(long id, string name)
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

        public override string ToString()
        {
            return Name;
        }
    }
}
