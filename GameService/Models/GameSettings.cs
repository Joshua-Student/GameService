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

        public string Get8CharacterRandomString()
        {
            string path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, 8);  // Return 8 character string
        }
    }
}
