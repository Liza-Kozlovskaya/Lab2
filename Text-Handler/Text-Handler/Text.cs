using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Text_Handler.Interfaces;
using Text_Handler.Separators;
using Text_Handler.TextObjects;

namespace Text_Handler
{
    public class Text
    {
        public Text()
        {
            Sentences = new List<ISentence>();
        }

        public Text(IEnumerable<ISentence> sentences) : this()
        {
            foreach (var sentence in sentences)
            {
                Sentences.Add(sentence);
            }
        }

        public IList<ISentence> Sentences { get; set; }

        public ISentence this[int index] => Sentences[index];

        //Получение предложений в порядке возрастания
        public IEnumerable<ISentence> GetSentencesInAscendingOrder() => Sentences.OrderBy(x => x.Items.Count);

        //Получение слов из вопросительных предложений
        public IEnumerable<IWord> GetWordsFromInterrogativeSentences(int length)
        {
            var result = new List<IWord>();

            foreach (var sentence in Sentences.Where(sentence => sentence.IsInterrogative))
            {
                //исключение повторения слова
                result.AddRange(sentence.GetWordsWithoutRepetition(length));
            }

            return result.GroupBy(x => x.Chars.ToLower()).Select(x => x.First()).ToList();
        }
    }
}