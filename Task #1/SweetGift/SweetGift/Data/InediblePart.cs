using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweetGift.Data
{
    enum InediblePartType
    {
        FromWood,
        FromPlastic
    }

    class InediblePart
    {
        public int Weight { get; set; }
        public InediblePartType MadeOf { get; set; }
    }
}
