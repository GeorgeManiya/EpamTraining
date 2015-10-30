using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Interfaces
{
    public enum SingleTextElementInnerOption
    {
        None,
        LeftSpace,
        RightSpace,
        BothSpace
    }

    public interface ISingleTextElement : ITextElement
    {
         SingleTextElementInnerOption InnerOption { get; }
    }
}
