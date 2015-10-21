using LinguisticLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Data
{
    abstract class TextElement : ITextElement
    {
        public virtual string TextValue { get; set; }

        public override string ToString()
        {
            return TextValue;
        }
    }
}
