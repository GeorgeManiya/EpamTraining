using SweetGift.Interfaces;

namespace SweetGift.Data
{
    abstract class GiftComponent : IGiftComponent
    {
        public int Weight { get; set; }

        public int Sugar { get; set; }

        public Wrapper Wrapper { get; set; }

        public GiftComponentMakingType MakingType { get; set; }

        public string Name { get; set; }

        public string CompanyName { get; set; }

        public virtual int NetWeight
        {
            get
            {
                return Wrapper != null
                    ? Weight - Wrapper.Weight
                    : Weight;
            }
        }
    }
}
