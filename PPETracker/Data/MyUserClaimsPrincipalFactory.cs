using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PPETracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PPETracker.Data
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public MyUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;
            
            //if IsAdmin is true, add new claim
            if(user.IsAdmin == true)
            {
                var claim = new Claim("IsAdmin", "Y");
                identity.AddClaim(claim);
            }
            else
            {
                var claim = new Claim("IsAdmin", "N");
                identity.AddClaim(claim);
            }
            
            return principal;
        }
    }
}
