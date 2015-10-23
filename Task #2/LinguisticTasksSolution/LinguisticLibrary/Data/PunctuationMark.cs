using LinguisticLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Data
{
    enum PunctuationMarkType
    {
        EndMark,                // dot in the end of sentence
        CommaMark,              // default comma
        QuestionMark,           // question in the end of sentence
        ExclamationMark,        // exclamation in the end of sentence
        AbreviationMark,        // dot, that hide some part of the text
        UnfinishedMark,         // three points
        EnumerationMark,        // dot with comma
        GeneralizationMark,     // colon
        ConnectionMark,         // dash inside the word
        IllustrationMark,       // dash between words
        AdditionalMark,         // brackets
        CitationMark,           // quotes
        SpaceMark               // space
    }

    public struct PunctuationMark : ISingleTextElement
    {
        public PunctuationMark(string stringValue, PunctuationMarkType type)
        {
            StringValue = stringValue;
            Type = type;
        }

        public PunctuationMarkType Type { get; set; }

        public string StringValue { get; set; }

        public override string ToString()
        {
            return StringValue;
        }
    }

        }
    }
}
