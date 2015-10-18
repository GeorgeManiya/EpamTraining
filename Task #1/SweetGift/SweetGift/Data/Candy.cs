using SweetGift.Interfaces;

namespace SweetGift.Data
{
    class Candy : GiftComponent, IChocolate
    {
        public Candy() { }

        public Candy(int weight, int sugar, int chocolate, string name, string companyName, Wrapper wrapper, GiftComponentMakingType makingType) 
            : base(weight, sugar, name, companyName, wrapper, makingType)
        {
            _chocolate = chocolate;
        }

        private int _chocolate;

        public int Chocolate
        {
            get { return _chocolate; }
            set { _chocolate = value; }
        }
    }
}
