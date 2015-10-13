using SweetGift.Interfaces;

namespace SweetGift.Data
{
    enum WrapperType
    {
        LooseWrapper,
        TightWrapper
    }

    class Wrapper
    {
        public WrapperType WrapperType { get; set; }
        public int Weight { get; set; }
    }
}
