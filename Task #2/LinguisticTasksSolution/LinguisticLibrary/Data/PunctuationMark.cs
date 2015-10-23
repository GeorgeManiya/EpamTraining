using LinguisticLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Data
{
    public enum PunctuationMarkType
    {
        EndMark,                // dot in the end of sentence
        CommaMark,              // default comma
        QuestionMark,           // question in the end of sentence
        ExclamationMark,        // exclamation in the end of sentence
        AccentMark,             // exclamation with question
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

    static class DefaultPunctuationMarks
    {
        private static ICollection<PunctuationMark> _terminalPunctuationMarks;
        public static ICollection<PunctuationMark> TerminalPunctuationMarks
        {
            get { return _terminalPunctuationMarks; }
        }

        private static ICollection<PunctuationMark> _internalPunctuationMarks;
        public static ICollection<PunctuationMark> InternalPunctuationMarks
        {
            get { return _internalPunctuationMarks; }
        }

        private static PunctuationMark _spaceMark;
        public static PunctuationMark SpaceMark
        {
            get { return _spaceMark; }
        }

        static DefaultPunctuationMarks()
        {
            _spaceMark = new PunctuationMark(" ", PunctuationMarkType.SpaceMark);

            _terminalPunctuationMarks = new Collection<PunctuationMark>()
            {
                new PunctuationMark(".", PunctuationMarkType.EndMark),
                new PunctuationMark("?", PunctuationMarkType.QuestionMark),
                new PunctuationMark("!", PunctuationMarkType.ExclamationMark),
                new PunctuationMark("...", PunctuationMarkType.UnfinishedMark),
                new PunctuationMark("?!", PunctuationMarkType.AccentMark),
                new PunctuationMark("!?", PunctuationMarkType.AccentMark)
            };

            _internalPunctuationMarks = new Collection<PunctuationMark>()
            {
                new PunctuationMark(",", PunctuationMarkType.CommaMark),
                new PunctuationMark(".", PunctuationMarkType.AbreviationMark),
                new PunctuationMark(";", PunctuationMarkType.EnumerationMark),
                new PunctuationMark(":", PunctuationMarkType.GeneralizationMark),
                new PunctuationMark("-", PunctuationMarkType.ConnectionMark),
                new PunctuationMark("-", PunctuationMarkType.IllustrationMark),
                new PunctuationMark("(", PunctuationMarkType.AdditionalMark),
                new PunctuationMark(")", PunctuationMarkType.AdditionalMark),
                new PunctuationMark("'", PunctuationMarkType.CitationMark)
            };
        }
    }
}
