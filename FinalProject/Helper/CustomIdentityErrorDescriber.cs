using Microsoft.AspNetCore.Identity;

namespace BackProject.Helpers
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit)
                ,
                Description = "passwordda regem olmalidir"
            };

        }

    }
}