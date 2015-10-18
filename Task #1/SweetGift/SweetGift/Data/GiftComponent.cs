using SweetGift.Interfaces;

namespace SweetGift.Data
{
    abstract class GiftComponent : IGiftComponent
    {
        private int _weight;
        private int _sugar;
        private string _name;
        private string _companyName;
        private Wrapper _wrapper;
        private GiftComponentMakingType _makingType;

        public GiftComponent() { }

        public GiftComponent(int weight, int sugar, string name, string companyName, Wrapper wrapper, GiftComponentMakingType makingType)
        {
            _weight = weight;
            _sugar = sugar;
            _name = name;
            _companyName = companyName;
            _wrapper = wrapper;
            _makingType = makingType;
        }

        public int Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }

        public int Sugar
        {
            get { return _sugar; }
            set { _sugar = value; }
        }

        public Wrapper Wrapper
        {
            get { return _wrapper; }
            set { _wrapper = value; }
        }

        public GiftComponentMakingType MakingType
        {
            get { return _makingType; }
            set { _makingType = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }

        public virtual int NetWeight
        {
            get
            {
                return Wrapper != null
                    ? Weight - Wrapper.Weight
                    : Weight;
            }
        }

        public override string ToString()
        {
            var type = GetType();
            return type.Name;
        }
    }
}
