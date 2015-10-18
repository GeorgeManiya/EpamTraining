using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweetGift.Interfaces;

namespace SweetGift.Data
{
    class Lollipop : GiftComponent
    {
        public Lollipop() { }

        public Lollipop(int weight, int sugar, string name, string companyName, Wrapper wrapper, GiftComponentMakingType makingType, InediblePart stick) 
            : base(weight, sugar, name, companyName, wrapper, makingType)
        {
            _stick = stick;
        }

        private InediblePart _stick;

        public InediblePart Stick
        {
            get { return _stick; }
            set { _stick = value; }
        }

        public override int NetWeight
        {
            get
            {
                return Wrapper != null
                    ? Stick != null
                        ? Weight - Wrapper.Weight - Stick.Weight
                        : Weight - Wrapper.Weight
                    : Weight;
            }
        }
    }
}
