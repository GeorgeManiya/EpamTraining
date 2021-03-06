﻿using LinguisticLibrary.Data;
using LinguisticLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticFeaturesAnalyzer.Data
{
    static class ActiveTextComponentFactory
    {
        public static ISingleTextElement GetActivatedSingleElement(ISingleTextElement textElement)
        {
            if(textElement is PunctuationMark)
            {
                return textElement;
            }
            else if(textElement is Word)
            {
                return new ActiveWord() { StringValue = textElement.StringValue, InnerOption = textElement.InnerOption };
            }
            else if(textElement is CompositeWord)
            {
                var compositeWord = new ActiveCompositeWord();
                foreach(var element in (CompositeWord)textElement)
                {
                    compositeWord.Add(GetActivatedSingleElement(element));
                }
                return compositeWord;
            }

            return null;
        }

        public static ActiveSentence GetActivatedSentence(Sentence sentence)
        {
            var activeSentence = new ActiveSentence();
            foreach (var element in sentence)
            {
                activeSentence.Add(GetActivatedSingleElement(element));
            }
            return activeSentence;
        }

        public static Text GetActivatedText(Text text)
        {
            var activeText = new Text();
            foreach(var paragraph in text)
            {
                var activeParagraph = new Paragraph();
                foreach(var sentence in paragraph)
                {
                    activeParagraph.Add(GetActivatedSentence(sentence));
                }
                activeText.Add(activeParagraph);
            }

            return activeText;
        }
    }
}
