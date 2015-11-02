using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguisticLibrary.Data
{
    public class Text : CombineTextElement<Paragraph>
    {
        public IList<Sentence> GetSentences()
        {
            var sentenceList = new List<Sentence>();
            foreach(var paragraph in this)
            {
                sentenceList.AddRange(paragraph);
            }

            return sentenceList;
        }
    }
}
