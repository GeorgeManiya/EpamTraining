using SweetGift.Data;

namespace SweetGift.Interfaces
{
    enum GiftComponentMakingType
    {
        HandMade,
        ProductionMade
    }

    interface IGiftComponent : ISweet
    {
        int Weight { get; }
        Wrapper Wrapper { get; }
        GiftComponentMakingType MakingType { get; }
        string Name { get; }
        string CompanyName { get; }
        int NetWeight { get; }
    }
}
