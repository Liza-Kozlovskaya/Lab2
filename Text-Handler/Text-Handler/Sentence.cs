using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Text_Handler.Interfaces;
using Text_Handler.Separators;

namespace Text_Handler.TextObjects
{
    public class Sentence : ISentence
    {
        private int _i;

        public Sentence()
        {
            Items = new List<ISentenceItem>();
        }

        public Sentence(IEnumerable<ISentenceItem> items) : this()
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public IList<ISentenceItem> Items { get; }

        public bool IsInterrogative
            => Items.Last().Chars == "?" || Items.Last().Chars == "?!" || Items.Last().Chars == "!?";

        //слова без повторения
        public IEnumerable<IWord> GetWordsWithoutRepetition(int length)
        {
            return Items.Where(x => x is Word).Cast<Word>().Where(x => x.Length == length);
        }

        public string SentenceToString()
        {
            var sb = new StringBuilder();

            GlueWords(-1, sb);
            return sb.ToString();
        }

        private void GlueWords(int index, StringBuilder sb)
        {
            _i = index;
            var flag = false;
            while (true)
            {
                _i++;

                if (_i >= Items.Count) break;

                sb.Append(Items[_i].Chars);
                var nextElement = Items.ElementAtOrDefault(_i + 1);

                if (nextElement == null) continue;

                if (PunctuationSeparator.EndPunctuationSeparator.Contains(Items[_i].Chars) ||
                    PunctuationSeparator.OperationPunctuationSeparator.Contains(Items[_i].Chars) ||
                    PunctuationSeparator.ClosePunctuationSeparator.Contains(nextElement.Chars) ||
                    PunctuationSeparator.InnerPunctuationSeparator.Contains(nextElement.Chars) ||
                    PunctuationSeparator.EndPunctuationSeparator.Contains(nextElement.Chars) ||
                    PunctuationSeparator.OperationPunctuationSeparator.Contains(nextElement.Chars)) continue;

                if (PunctuationSeparator.ClosePunctuationSeparator.Contains(Items[_i].Chars))
                {
                    break;
                }

                if (PunctuationSeparator.OpenPunctuationSeparator.Contains(Items[_i].Chars) ||
                    PunctuationSeparator.RepeatPunctuationSeparator.Contains(Items[_i].Chars))
                {
                    GlueWords(_i, sb);
                }

                if (!PunctuationSeparator.ClosePunctuationSeparator.Contains(nextElement.Chars))
                {
                    sb.Append(" ");
                }
            }
        }
    }
}