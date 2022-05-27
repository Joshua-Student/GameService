using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameService.Models
{
    public class GameSettings
    {
        [Required]
        public string? Game { get; set; }
        [Required]
        public bool? Machine { get; set; }

    }
}
