using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class WordleMachine
    {
        private readonly WordleModel model;
        public StringBuilder wrongLetters;
        public char[] correctPosition;
        public List<StringBuilder> difPosition;

        public WordleMachine(WordleModel model)
        {
            this.model = model;

            // This will collect all the wrong letters we get into one string
            wrongLetters = new StringBuilder();

            // This will collect correct letters for each spot in the word
            correctPosition = new char[5];
            for (int i = 0; i < correctPosition.Length; i++)
            {
                correctPosition[i] = '-';
            }

            // This will collect all the different position letters for each spot in the word
            difPosition = new List<StringBuilder>(5);
            for (int i = 0; i < difPosition.Capacity; i++)
            {
                difPosition.Add(new StringBuilder(""));
            }
        }

        public ReturnObject Move()
        {
            ReturnObject ro = new ReturnObject();
            StringBuilder guesses = new StringBuilder();

            // Get random word for first guess
            Random rand = new Random();
            int r = rand.Next(model.words.Count);
            string guess = model.words.ElementAt(r);

            List<WordleResponse> response = new List<WordleResponse>();

            for (int i = 0; i < 6; i++)
            {
                guesses.Append($"Guess {i + 1}: {guess.ToUpper()} ");

                response = model.CheckGuess(guess);

                CheckResponse(response, guess);

                if (CheckIfWin(response))
                {
                    ro.Message = "I win.";
                    break;
                }
                else if (i < 5)
                    guess = GetNextGuess();
                else
                    ro.Message = "I lose";

            }

            ro.Valid = true;
            ro.OpponentsMove = guesses.ToString();
            return ro;
        }

        // Searches list for next guess based off of the info we collected
        public string GetNextGuess()
        {
            // Creates Regex string to correctly search the list
            StringBuilder guess = CreateRegex();

            // Get list of all the possible words from the file
            var myRegex = new Regex(guess.ToString().ToLower());
            List<string> resultList = model.words.Where(f => myRegex.IsMatch(f)).ToList();

            // Get random word from that list
            Random rand = new Random();
            int nextGuess = rand.Next(resultList.Count);

            return resultList.ElementAt(nextGuess);
        }

        public StringBuilder CreateRegex()
        {
            StringBuilder guess = new StringBuilder("(?=");
            string wrong = wrongLetters.ToString();
            for (int i = 0; i < 5; i++)
            {
                if (correctPosition[i] != '-')
                {
                    guess.Append(correctPosition[i]);
                    continue;
                }
                else if (difPosition.ElementAt(i).ToString() != "")
                {
                    guess.Append($"[^{wrong}{difPosition.ElementAt(i)}]");
                }
                else
                {
                    guess.Append($"[^{wrong}]");
                }
            }

            guess.Append(')');

            // Gather all the different postion letters. There may be repeats but that's ok.
            StringBuilder difPos = new StringBuilder();
            foreach (var item in difPosition)
            {
                if (item.ToString() != "")
                {
                    difPos.Append(item.ToString());
                }
            }

            foreach (char c in difPos.ToString())
            {
                guess.Append($"(?=.*{c}.*)");
            }

            return guess;
        }

        // Returns true if all the letters are correct
        public bool CheckIfWin(List<WordleResponse> response)
        {
            foreach (var item in response)
            {
                if (item != WordleResponse.CORRECT)
                    return false;
            }
            return true;
        }

        public void CheckResponse(List<WordleResponse> response, string guess)
        {
            for (int i = 0; i < response.Count; i++)
            {
                switch (response.ElementAt(i))
                {
                    case WordleResponse.CORRECT:
                        correctPosition[i] = guess[i];
                        break;
                    case WordleResponse.DIFFERENT_POSITION:
                        difPosition.ElementAt(i).Append(guess[i]);
                        break;
                    case WordleResponse.WRONG:
                        wrongLetters.Append(guess[i]);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
