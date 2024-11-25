using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K2.Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TaskUtils.PerformTask("Tekstas.txt", "RedTekstas.txt");
        }
        public class TaskUtils
        {
            public static bool NoDigits(string line)
            {
                char[] numbers = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                if (line.IndexOfAny(numbers) != -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            public static int NumberDiffrentVowelsInLine(string line)
            {
                int count = 0;
                string vowels = "aąęėįųūiuoeay";
                line = line.ToLower();
                List<char> existingvowels = new List<char>();
                for (int i = 0; i < line.Length - 1; i++)
                {
                    if (vowels.Contains(line[i]) == true)
                    {
                        if (existingvowels.Contains(line[i]) == false)
                        {
                            count++;
                            existingvowels.Add(line[i]);
                        }
                    }
                }
                return count;
            }
            public static string FindWord1Line(string line, string punctuation)
            {
                string LongestWord = "";
                string[] indwords = line.Split(punctuation.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in indwords)
                {
                    int vowcount = NumberDiffrentVowelsInLine(word);
                    if (vowcount >= 3 && word.Length > LongestWord.Length)
                    {
                        StringBuilder longestword = new StringBuilder();
                        longestword.Append(word);
                        int cursor = line.IndexOf(word);
                        cursor += word.Length;
                        while (cursor < line.Length && punctuation.Contains(line[cursor]) == true)
                        {
                            longestword.Append(line[cursor]);
                            cursor++;
                        }
                        LongestWord = longestword.ToString();
                    }
                }
                return LongestWord;
            }
            public static string EditLine(string line, string punctuation, string word)
            {
                StringBuilder newLine = new StringBuilder();
                newLine.Append(word);
                int wordplace = line.IndexOf(word);
                newLine.Append(line.Substring(0, wordplace));
                newLine.Append(line.Substring(wordplace + word.Length));
                return newLine.ToString();
            }
            public static string FindWord2Line(string line, string punctuation)
            {
                string[] Words = line.Split(punctuation.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int i = Words.Length - 1; i >= 0; i--)
                {
                    if (NoDigits(Words[i]) == true)
                    {
                        string word2 = Words[i];
                        int cursor = line.IndexOf(word2);
                        cursor += word2.Length;
                        while (cursor < line.Length && punctuation.Contains(line[cursor]) == true)
                        {
                            word2.Append(line[cursor]);
                            cursor++;
                        }
                        return word2;
                    }
                }
                return " ";
            }
            public static void PerformTask(string fd, string fr)
            {
                using (StreamWriter writer = new StreamWriter(fr))
                {
                    using (StreamReader reader = new StreamReader(fd))

                    {
                        string line;
                        string punctuation = reader.ReadLine();
                        while ((line = reader.ReadLine()) != null)
                        {
                            string Word1 = FindWord1Line(line, punctuation);
                            string FixedLine = EditLine(line, punctuation, Word1);
                            writer.WriteLine(FixedLine);
                        }

                    }
                    using (StreamReader reader = new StreamReader(fd))
                    {
                        string line;
                        string punctuation = reader.ReadLine();
                        while ((line = reader.ReadLine()) != null)
                        {
                            string Word2 = FindWord2Line(line, punctuation);
                            writer.WriteLine(Word2);
                        }

                    }
                }
            }
        }
    }
}
