using LinguisticLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Data
{
    public class Sentence : CombineTextElement<ISingleTextElement>
    {
        public PunctuationMarkType Type
        {
            get
            {
                if (this.Any())
                {
                    var lastElement = this.Last();
                    if (lastElement is PunctuationMark)
                    {
                        return ((PunctuationMark)lastElement).Type;
                    }
                }
                
                return PunctuationMarkType.None;
            }
        }

        public bool IsNotFinished
        {
            get { return Type == PunctuationMarkType.None; }
        }

        public void Concat(string value)
        {

        }
    }
}
