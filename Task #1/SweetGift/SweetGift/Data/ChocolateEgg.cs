using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweetGift.Interfaces;

namespace SweetGift.Data
{
    class ChocolateEgg : GiftComponent, IChocolate
    {
        public int Chocolate { get; set; }

        public InediblePart Toy { get; set; }

        public override int NetWeight
        {
            get
            {
                return Wrapper != null
                    ? Toy != null
                        ? Weight - Wrapper.Weight - Toy.Weight
                        : Weight - Wrapper.Weight
                    : Weight;
            }
        }
    }
}
