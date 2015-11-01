using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinguisticLibrary.Data;

namespace LinguisticFeaturesAnalyzer.Data
{
    class ActiveWord : Word, IActiveTextComponent
    {
        public bool IsActive { get; set; }
    }
}
