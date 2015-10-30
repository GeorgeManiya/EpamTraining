using LinguisticLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace LinguisticLibrary.Data
{
    class Word : CombineTextElement<ISingleTextElement>, ISingleTextElement
    {
        public SingleTextElementInnerOption InnerOption { get; set; }

        public bool IsDecomposite { get { return this.Count > 1; } }
    }
}
