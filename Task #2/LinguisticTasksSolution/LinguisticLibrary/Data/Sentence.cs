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
            if (!IsNotFinished)
            {
                var endMark = (PunctuationMark)this.Last();
                this.Remove(endMark);

                var sentencePart = TextParser.ParseSentenceString(value, new PunctuationMark());
                foreach(var textElement in sentencePart)
                {
                    this.Add(textElement);
                }

                this.Add(endMark);
            }
            else
            {
                var terminalPunctuationMark = value.GetFirstOrDefaultPunctuationMark(DefaultPunctuationMarks.TerminalPunctuationMarks);
                if (terminalPunctuationMark.HasValue)
                {
                    var index = value.IndexOfPunctuationMark(terminalPunctuationMark);
                    value = value.Substring(0, index);
                }

                var sentencePart = TextParser.ParseSentenceString(value, terminalPunctuationMark);
                foreach (var textElement in sentencePart)
                {
                    this.Add(textElement);
                }
            }
        }
    }
}
