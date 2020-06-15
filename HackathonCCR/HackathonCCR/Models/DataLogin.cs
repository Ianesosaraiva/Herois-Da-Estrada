using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HackathonCCR.Models
{
    public class DataLogin
    {

        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string email { get; set; }
        [Required]
        public string senha { get; set; }
    }
}