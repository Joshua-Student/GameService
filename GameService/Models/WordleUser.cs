using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class WordleUser
    {
        public bool MachinePlayer { get; set; }
        private readonly WordleModel model;
        bool gameInProgress = true;
        int turns = 0;

        public WordleUser(WordleModel model)
        {
            this.model = model;
        }

        public ReturnObject Move(string move)
        {
            ReturnObject ro = new ReturnObject();
            StringBuilder result = new StringBuilder();
            List<WordleResponse> response = new List<WordleResponse>();

            // Checks if game is in session
            if (!gameInProgress)
            {
                ro.Valid = false;
                ro.Message = "Game is over";
                return ro;
            }

            // Checks the guess with the model and gets a response
            response = model.CheckGuess(move.ToLower());

            if(response.First() == WordleResponse.ILLEGAL_WORD)
            {
                ro.Valid = false;
                ro.Message = "Illegal word";
                return ro;
            }

            result = FormatResponse(response);

            if (turns == 6)
                gameInProgress = false;

            ro.Valid = true;
            ro.Message = result.ToString();
            return ro;

        }

        private StringBuilder FormatResponse(List<WordleResponse> response)
        {
            StringBuilder sb = new StringBuilder();
            int win = 0;
            foreach (WordleResponse r in response)
            {
                switch (r)
                {
                    case WordleResponse.CORRECT:
                        sb.Append("C ");
                        win++;
                        break;
                    case WordleResponse.DIFFERENT_POSITION:
                        sb.Append("DP ");
                        break;
                    case WordleResponse.WRONG:
                        sb.Append("W ");
                        break;
                    default:
                        break;
                }
            }
            
            turns++;
            if (win == 5)
                gameInProgress = false;

            return sb;
        }

        public bool IsMachinePlayer()
        {
            return MachinePlayer;
        }
    }
}
