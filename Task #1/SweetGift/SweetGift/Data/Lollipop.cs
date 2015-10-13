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
        public InediblePart Stick { get; set; }

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
