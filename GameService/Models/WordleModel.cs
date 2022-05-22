using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class WordleModel
    {
        private const string Path = @"answers.txt";
        private static readonly int LETTERS_IN_ALPHABET = 26;

        public string answer;
        public List<string> words;
        public Dictionary<char, int> answerCounts;

        private readonly Random rand;
        private readonly HashSet<string> WORD_SET;

        public WordleModel()
        {
            words = File.ReadAllLines(Path).ToList();
            WORD_SET = new HashSet<string>(words);
            rand = new Random();
            newGame();
        }

        public void newGame()
        {
            answer = GetAnswer();
        }

        private string GetAnswer()
        {
            int r = rand.Next(words.Count);
            string word = words.ElementAt(r);

            // Initialize and populate array with count of how many of each letter in the answer
            answerCounts = GetLettersCount(word);
            return word;
        }

        /* Counts how many times each letter of the alphabet appears in a word
        *
        *  returns an int[] the length of the alphabet with a count of how many times
        *  the letter at that index of the alphabet appears in the word */
        public Dictionary<char, int> GetLettersCount(string word)
        {
            char[] letters = word.ToCharArray();
            Dictionary<char, int> result = new Dictionary<char, int>(LETTERS_IN_ALPHABET);
            foreach (char c in letters)
                result[c] = result.GetValueOrDefault(c, 0) + 1;
            return result;
        }


        /* Processes the input when the user hits enter/takes a guess
        *
        *  returns true if the entry 100% matches, false otherwise */
        public List<WordleResponse> CheckGuess(string guess)
        {
            int guessLength = guess.Length;
            if (!WORD_SET.Contains(guess.ToLower()))
            {
                List<WordleResponse> tempList = new List<WordleResponse>();
                tempList.Add(WordleResponse.ILLEGAL_WORD);
                return tempList;
            }

            List<WordleResponse> response = new List<WordleResponse>(guessLength);

            // Arrays to keep track of how many unaccounted-for remaining of each letter of the alphabet
            Dictionary<char, int> guessCounts = GetLettersCount(guess);
            Dictionary<char, int> ansCounts = new Dictionary<char, int>(answerCounts);

            // Add the appropriate WordleResponse to the response for each char in the guess
            for (int i = 0; i < guessLength; i++)
            {
                // The char at this index of the guess
                char currentChar = guess.ElementAt(i); 
                if (currentChar == answer.ElementAt(i))
                { 
                    // Char is in correct position
                    response.Add(WordleResponse.CORRECT);
                    guessCounts[currentChar] = guessCounts.GetValueOrDefault(currentChar) - 1;
                    ansCounts[currentChar] = ansCounts.GetValueOrDefault(currentChar) - 1;
                }
                else if (answer.Contains(currentChar))
                {
                    // Char is elsewhere in the answer
                    if (ansCounts.GetValueOrDefault(currentChar, 0) > 0
                        && guessCounts.GetValueOrDefault(currentChar, 0) == 1)
                    {
                        // No duplicates of this char in the guess
                        response.Add(WordleResponse.DIFFERENT_POSITION);
                        guessCounts[currentChar] = guessCounts.GetValueOrDefault(currentChar) - 1;
                        ansCounts[currentChar] = ansCounts.GetValueOrDefault(currentChar) - 1;
                    }
                    else
                    {
                        // Save duplicates as a different response for later
                        response.Add(WordleResponse.ILLEGAL_WORD);
                    }
                }
                else
                { 
                    // Char is not in answer
                    response.Add(WordleResponse.WRONG);
                    guessCounts[currentChar] = guessCounts.GetValueOrDefault(currentChar) - 1;
                }
            }

            // Deal with duplicates (max amount of duplicates is 3, max amount of contiguous duplicates is 2)
            // (We're dealing with them now because all the perfect matches have already been processed)
            for (int i = 0; i < guessLength; i++)
            {
                if (response.ElementAt(i) != WordleResponse.ILLEGAL_WORD) continue;

                char currentChar = guess.ElementAt(i);
                if (ansCounts.GetValueOrDefault(currentChar, 0) > 0)
                { 
                    // There are still unaccounted-for occurrences of this letter in the answer
                    response.RemoveAt(i);
                    response.Insert(i, WordleResponse.DIFFERENT_POSITION);
                    guessCounts[currentChar] = guessCounts.GetValueOrDefault(currentChar) - 1;
                    ansCounts[currentChar] = ansCounts.GetValueOrDefault(currentChar) - 1;
                }
                else
                { 
                    // No more of this letter unaccounted-for in the answer
                    response.RemoveAt(i);
                    response.Insert(i, WordleResponse.WRONG);
                    guessCounts[currentChar] = guessCounts.GetValueOrDefault(currentChar) - 1;
                }
            }

            return response;
        }
    }
}
