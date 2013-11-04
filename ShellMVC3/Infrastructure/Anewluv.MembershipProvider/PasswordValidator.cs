using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using Shell.MVC2.Data.AuthenticationAndMembership;

namespace Security
{
    public class PasswordValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {

        
            if (string.Equals(userName, "Alice", StringComparison.OrdinalIgnoreCase)
                && password == "Password123!@#") return;
            throw new SecurityTokenValidationException();
        }
    }
}
