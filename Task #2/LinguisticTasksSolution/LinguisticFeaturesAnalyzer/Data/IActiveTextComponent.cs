using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticFeaturesAnalyzer.Data
{
    interface IActiveTextComponent
    {
        bool IsActive { get; set; }
    }
}
