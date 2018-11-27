using System;
using System.Collections.Generic;

namespace Text_Handler.Interfaces
{
    public interface ISentence
    {
        IList<ISentenceItem> Items { get; }

        //является ли вопросительным
        bool IsInterrogative { get; }

        //список слов без повторения
        IEnumerable<IWord> GetWordsWithoutRepetition(int length);
        string SentenceToString();
    }
}