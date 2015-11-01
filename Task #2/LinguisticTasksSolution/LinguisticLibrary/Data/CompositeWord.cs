using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinguisticLibrary.Interfaces;

namespace LinguisticLibrary.Data
{
    public class CompositeWord : CombineTextElement<ISingleTextElement>, IWord
    {
        public SingleTextElementInnerOption InnerOption
        {
            get
            {
                return this.Any() ? this.First().InnerOption : SingleTextElementInnerOption.None;
            }
            set
            {
                if (this.Any())
                {
                    this.First().InnerOption = value;
                }
            }
        }
    }
}
