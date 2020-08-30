using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using PPETracker.Models;

namespace PPETracker.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        

        public void OnGetAsync()
        {
            
        }

        public void OnPost(string provider, string returnUrl = null)
        {
           
        }

        public void OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            
        }

        public void OnPostConfirmationAsync(string returnUrl = null)
        {
            
        }
    }
}
