using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public enum WordleResponse
    {
        CORRECT,
        DIFFERENT_POSITION,
        WRONG,
        ILLEGAL_WORD
    }
}
