using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PPETracker.Models;

namespace PPETracker.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginWith2faModel : PageModel
    {
        
        public void OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            
        }

        public void OnPostAsync(bool rememberMe, string returnUrl = null)
        {
            
        }
    }
}
