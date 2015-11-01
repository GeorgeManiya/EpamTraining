using LinguisticLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Data
{
    public abstract class SingleTextElement : TextElement, ISingleTextElement
    {
        public SingleTextElementInnerOption InnerOption { get; set; }

        private string _stringValue;
        public override string StringValue
        {
            get
            {
                switch (InnerOption)
                {
                    case SingleTextElementInnerOption.None:
                        return _stringValue;
                    case SingleTextElementInnerOption.LeftSpace:
                        return " " + _stringValue;
                    case SingleTextElementInnerOption.RightSpace:
                        return _stringValue + " ";
                    case SingleTextElementInnerOption.BothSpace:
                        return " " + _stringValue + " ";
                }

                return _stringValue;
            }

            set
            {
                _stringValue = value;
            }
        }
    }
}
