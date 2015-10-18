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
        public ChocolateEgg() { }

        public ChocolateEgg(int weight, int sugar, int chocolate, string name, string companyName, Wrapper wrapper, GiftComponentMakingType makingType, InediblePart toy) 
            : base(weight, sugar, name, companyName, wrapper, makingType)
        {
            _chocolate = chocolate;
            _toy = toy;
        }

        private int _chocolate;

        public int Chocolate
        {
            get { return _chocolate; }
            set { _chocolate = value; }
        }

        private InediblePart _toy;

        public InediblePart Toy
        {
            get { return _toy; }
            set { _toy = value; }
        }

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
