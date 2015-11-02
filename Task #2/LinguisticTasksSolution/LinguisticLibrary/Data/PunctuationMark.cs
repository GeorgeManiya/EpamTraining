using LinguisticLibrary.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
        SpaceMark,              // space
        None
    }

    public struct PunctuationMark : ISingleTextElement
    {
        public PunctuationMark(string stringValue, PunctuationMarkType type, SingleTextElementInnerOption innerOption)
        {
            StringValue = stringValue;
            Type = type;
            InnerOption = innerOption;
        }

        public PunctuationMarkType Type { get; set; }

        public string StringValue { get; set; }

        public bool HasValue
        {
            get { return !string.IsNullOrEmpty(StringValue); }
        }

        public SingleTextElementInnerOption InnerOption { get; set; }

        public override string ToString()
        {
            switch (InnerOption)
            {
                case SingleTextElementInnerOption.None:
                    return StringValue;
                case SingleTextElementInnerOption.LeftSpace:
                    return " " + StringValue;
                case SingleTextElementInnerOption.RightSpace:
                    return StringValue + " ";
                case SingleTextElementInnerOption.BothSpace:
                    return " " + StringValue + " ";
            }

            return StringValue;
        }
    }

    public static class DefaultPunctuationMarks
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
            _spaceMark = new PunctuationMark(" ", PunctuationMarkType.SpaceMark, SingleTextElementInnerOption.None);

            _terminalPunctuationMarks = new Collection<PunctuationMark>()
            {
                new PunctuationMark(".", PunctuationMarkType.EndMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark("?", PunctuationMarkType.QuestionMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark("!", PunctuationMarkType.ExclamationMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark("...", PunctuationMarkType.UnfinishedMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark("?!", PunctuationMarkType.AccentMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark("!?", PunctuationMarkType.AccentMark, SingleTextElementInnerOption.RightSpace)
            };

            _internalPunctuationMarks = new Collection<PunctuationMark>()
            {
                new PunctuationMark(",", PunctuationMarkType.CommaMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark(".", PunctuationMarkType.AbreviationMark, SingleTextElementInnerOption.None),
                new PunctuationMark(".", PunctuationMarkType.AbreviationMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark(";", PunctuationMarkType.EnumerationMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark(":", PunctuationMarkType.GeneralizationMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark("-", PunctuationMarkType.ConnectionMark, SingleTextElementInnerOption.None),
                new PunctuationMark("-", PunctuationMarkType.IllustrationMark, SingleTextElementInnerOption.BothSpace),
                new PunctuationMark("(", PunctuationMarkType.AdditionalMark, SingleTextElementInnerOption.LeftSpace),
                new PunctuationMark(")", PunctuationMarkType.AdditionalMark, SingleTextElementInnerOption.RightSpace),
                new PunctuationMark("\"", PunctuationMarkType.CitationMark, SingleTextElementInnerOption.LeftSpace),
                new PunctuationMark("\"", PunctuationMarkType.CitationMark, SingleTextElementInnerOption.RightSpace)
            };
        }
    }
}
