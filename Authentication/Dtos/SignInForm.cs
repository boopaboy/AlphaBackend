using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos
{
    public class SignInForm
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
