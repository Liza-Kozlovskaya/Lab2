using System;
using System.IO;
using System.Linq;
using System.Text;
using Text_Handler.Parser;

namespace Text_Handler
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var parser = new TextParser();
            var streamReader = new StreamReader(@"..\..\File\Text.txt", Encoding.Default);
            var text = parser.Parse(streamReader);

            //Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины.
            foreach (var word in text.GetWordsFromInterrogativeSentences(9))
            {
                Console.WriteLine(word.Chars);
            }

            Console.ReadKey();
        }
    }
}