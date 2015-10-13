using SweetGift.Interfaces;

namespace SweetGift.Data
{
    enum WrapperType
    {
        TightWrapper,
        LooseWrapper
    }

    class Wrapper
    {
        public WrapperType WrapperType { get; set; }
        public int Weight { get; set; }
    }
}
