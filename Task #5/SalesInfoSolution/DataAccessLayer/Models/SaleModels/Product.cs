namespace DataAccessLayer.Models.SaleModels
{
    public class Product
    {
        public Product(long id, string productName, int productCost)
        {
            Id = id;
            ProductName = productName;
            ProductCost = productCost;
        }

        public long Id { get; private set; }
        public string ProductName { get; private set; }
        public int ProductCost { get; private set; }

        public void SetId(long id)
        {
            Id = id;
        }

        public void SetProductName(string productName)
        {
            ProductName = productName;
        }

        public void SetProductCost(int productCost)
        {
            ProductCost = productCost;
        }
    }
}
