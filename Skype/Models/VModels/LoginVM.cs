using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Skype.Models.VModels
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Please enter your login")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
