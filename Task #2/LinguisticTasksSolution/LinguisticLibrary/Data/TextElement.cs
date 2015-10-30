using LinguisticLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Data
{
    public abstract class TextElement : ITextElement
    {
        public virtual string StringValue { get; set; }

        public override string ToString()
        {
            return StringValue;
        }
    }
}
