using LinguisticLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Data
{
    internal abstract class SingleTextElement : TextElement, ISingleTextElement
    {
        public SingleTextElementInnerOption InnerOption { get; }
    }
}
