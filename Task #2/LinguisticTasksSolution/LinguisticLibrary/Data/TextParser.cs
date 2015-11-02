using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LinguisticLibrary.Interfaces;

namespace LinguisticLibrary.Data
{
    public static class TextParser
    {
        public static Text Parse(TextReader reader)
        {
            var text = new Text();

            var line = reader.ReadLine();
            while(!string.IsNullOrEmpty(line))
            {
                var paragraph = new Paragraph();
                text.Add(paragraph);

                while (!string.IsNullOrEmpty(line))
                {
                    var sentenceString = line;
                    var terminalPunctuationMark = line.GetFirstOrDefaultPunctuationMark(DefaultPunctuationMarks.TerminalPunctuationMarks);
                    if (terminalPunctuationMark.HasValue)
                    {
                        var index = line.IndexOfPunctuationMark(terminalPunctuationMark);
                        sentenceString = line.Substring(0, index).TrimStart(' ');
                        line = line.Remove(0, index + terminalPunctuationMark.StringValue.Length);

                        if(text.Count > 1)
                        {
                            var lastParagraph = text.Last(p => p.HasValue);
                            if (lastParagraph.Any())
                            {
                                var lastSentence = lastParagraph.Last();
                                if (lastSentence.IsNotFinished)
                                {
                                    text.Remove(paragraph);
                                    paragraph = lastParagraph;
                                    lastSentence.Concat(sentenceString);
                                    continue;
                                }
                            }
                        }
                    }
                    else
                    {
                        line = string.Empty;
                    }

                    var sentence = ParseSentenceString(sentenceString, terminalPunctuationMark);
                    paragraph.Add(sentence);
                }

                line = reader.ReadLine();
            }

            return text;
        }

        public static Text Parse(string filePath)
        {
            var reader = new StreamReader(filePath, Encoding.Default);

            return Parse(reader);
        }

        public static Sentence ParseSentenceString(string source, PunctuationMark endMark)
        {
            var sentence = new Sentence();

            // Get split parts of the sentence
            var splitParts = source.Split(new char[] { ' ' });

            foreach (var part in splitParts)
            {
                var sPart = part;

                // Get inner punctuation marks in the split part
                var innerPunctuationMarks = sPart.GetPunctuationMarks(DefaultPunctuationMarks.InternalPunctuationMarks);
                if (innerPunctuationMarks.Any())
                {
                    var sentenceParts = new List<ISingleTextElement>();
                    var compositeWord = new CompositeWord();

                    // If split part contains punctuation marks, split this part to single text elements
                    foreach (var mark in innerPunctuationMarks)
                    {
                        var markLenght = mark.StringValue.Length;
                        var index = sPart.IndexOfPunctuationMark(mark);

                        var leftPart = sPart.Substring(0, index);
                        if (!string.IsNullOrEmpty(leftPart))
                        {
                            var word = new Word() { StringValue = leftPart };
                            compositeWord.Add(word);
                            if (!sentenceParts.Contains(compositeWord))
                                sentenceParts.Add(compositeWord);
                        }

                        if(index == 0)
                        {
                            sentenceParts.Add(mark);
                        }
                        else if(index + markLenght < sPart.Length)
                        {
                            compositeWord.Add(mark);
                            if (!sentenceParts.Contains(compositeWord))
                                sentenceParts.Add(compositeWord);
                        }
                        else
                        {
                            sentenceParts.Add(mark);
                        }

                        sPart = sPart.Substring(index + markLenght);
                    }

                    if (!string.IsNullOrEmpty(sPart))
                    {
                        var word = new Word() { StringValue = sPart };
                        compositeWord.Add(word);
                        if (!sentenceParts.Contains(compositeWord))
                            sentenceParts.Add(compositeWord);
                    }

                    // Add single text elements to sentence
                    foreach(var sentencePart in sentenceParts)
                    {
                        if (sentencePart == sentenceParts.Last())
                            sentencePart.InnerOption = SingleTextElementInnerOption.RightSpace;
                        sentence.Add(sentencePart);
                    }
                }
                else
                {
                    var word = new Word() { StringValue = sPart, InnerOption = SingleTextElementInnerOption.RightSpace };
                    sentence.Add(word);
                }
            }

            if (endMark.HasValue)
            {
                if(sentence.Last().InnerOption == SingleTextElementInnerOption.RightSpace)
                {
                    sentence.Last().InnerOption = SingleTextElementInnerOption.None;
                }
                sentence.Add(endMark);
            }

            return sentence;
        }


        #region Extensions

        public static bool ContainsPunctuationMark(this string source, PunctuationMark punctuationMark)
        {
            return source.IndexOfPunctuationMark(punctuationMark) != -1;
        }

        public static int IndexOfPunctuationMark(this string source, PunctuationMark punctuationMark)
        {
            var tempSource = source;
            var tempIndex = 0;

            if (tempSource.Contains(punctuationMark.StringValue))
            {
                var punctuationIndex = tempSource.IndexOf(punctuationMark.StringValue);

                while (punctuationIndex != -1 && tempIndex < source.Length)
                {
                    tempIndex += punctuationIndex;
                    var leftPart = tempSource.Substring(0, punctuationIndex);
                    var rightPart = tempSource.Substring(punctuationIndex + punctuationMark.StringValue.Length);
                    var hasLeftSpace = string.IsNullOrEmpty(leftPart) || char.IsWhiteSpace(leftPart.Last());
                    var hasRightSpace = string.IsNullOrEmpty(rightPart) || char.IsWhiteSpace(rightPart.First());

                    switch (punctuationMark.InnerOption)
                    {
                        case SingleTextElementInnerOption.None:
                            {
                                if (!hasLeftSpace && !hasRightSpace)
                                    return tempIndex;
                                break;
                            }
                        case SingleTextElementInnerOption.LeftSpace:
                            {
                                if (hasLeftSpace && !hasRightSpace)
                                    return tempIndex;
                                break;
                            }
                        case SingleTextElementInnerOption.RightSpace:
                            {
                                if (!hasLeftSpace && hasRightSpace)
                                    return tempIndex;
                                break;
                            }
                        case SingleTextElementInnerOption.BothSpace:
                            {
                                if (hasLeftSpace && hasRightSpace)
                                    return tempIndex;
                                break;
                            }
                    }

                    tempSource = tempSource.Substring(punctuationIndex + punctuationMark.StringValue.Length);
                    tempIndex += punctuationMark.StringValue.Length;
                    punctuationIndex = rightPart.IndexOf(punctuationMark.StringValue);
                }
            }

            return -1;
        }

        public static IEnumerable<PunctuationMark> GetPunctuationMarks(this string source, IEnumerable<PunctuationMark> punctuationMarks)
        {
            // Ordered by string value collection, will give us ability to find composite punctuation marks first
            var orderedMarks = punctuationMarks.OrderByDescending(mark => mark.StringValue.Length);

            var punctuationString = string.Empty;
            var punctuationDetected = false;

            var leftPart = string.Empty;
            var rightPart = string.Empty;
            var hasLeftSpace = false;
            var hasRightSpace = false;
            var index = 0;

            foreach (var symbol in source)
            {
                if (char.IsPunctuation(symbol))
                {
                    if (!punctuationDetected)
                    {
                        leftPart = source.Substring(0, index);
                        hasLeftSpace = string.IsNullOrEmpty(leftPart) || char.IsWhiteSpace(leftPart.Last());
                    }

                    punctuationDetected = true;
                    punctuationString += symbol;
                }
                else
                {
                    if (punctuationDetected)
                    {
                        rightPart = source.Substring(index - 1 + punctuationString.Length);
                        hasRightSpace = string.IsNullOrEmpty(rightPart) || char.IsWhiteSpace(rightPart.First());

                        var innerOption = hasLeftSpace && hasRightSpace
                            ? SingleTextElementInnerOption.BothSpace
                            : !hasLeftSpace && hasRightSpace
                                ? SingleTextElementInnerOption.RightSpace
                                : hasLeftSpace && !hasRightSpace
                                    ? SingleTextElementInnerOption.LeftSpace
                                    : SingleTextElementInnerOption.None;
                        var punctuationMark = new PunctuationMark() { StringValue = punctuationString, InnerOption = innerOption };
                        if (orderedMarks.Any(mark => mark.StringValue == punctuationMark.StringValue && mark.InnerOption == punctuationMark.InnerOption))
                        {
                            yield return orderedMarks.First(mark => mark.StringValue == punctuationMark.StringValue && mark.InnerOption == punctuationMark.InnerOption);
                        }
                    }

                    punctuationString = string.Empty;
                    punctuationDetected = false;
                }

                index++;
            }

            if (punctuationDetected)
            {
                rightPart = source.Substring(index);
                hasRightSpace = string.IsNullOrEmpty(rightPart) || char.IsWhiteSpace(rightPart.First());

                var innerOption = hasLeftSpace && hasRightSpace
                    ? SingleTextElementInnerOption.BothSpace
                    : !hasLeftSpace && hasRightSpace
                        ? SingleTextElementInnerOption.RightSpace
                        : hasLeftSpace && !hasRightSpace
                            ? SingleTextElementInnerOption.LeftSpace
                            : SingleTextElementInnerOption.None;
                var punctuationMark = new PunctuationMark() { StringValue = punctuationString, InnerOption = innerOption };
                if (orderedMarks.Any(mark => mark.StringValue == punctuationMark.StringValue && mark.InnerOption == punctuationMark.InnerOption))
                {
                    yield return orderedMarks.First(mark => mark.StringValue == punctuationMark.StringValue && mark.InnerOption == punctuationMark.InnerOption);
                }
            }
        }

        public static PunctuationMark GetFirstOrDefaultPunctuationMark(this string source, IEnumerable<PunctuationMark> list)
        {
            var punctuationMarks = source.GetPunctuationMarks(list);
            return punctuationMarks.Any() ? punctuationMarks.First() : new PunctuationMark();
        }

        #endregion
    }
}
