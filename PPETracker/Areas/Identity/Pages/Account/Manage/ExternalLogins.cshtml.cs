using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PPETracker.Models;

namespace PPETracker.Areas.Identity.Pages.Account.Manage
{
    public class ExternalLoginsModel : PageModel
    {
        


        public void OnGetAsync()
        {
            
        }

        public void OnPostRemoveLoginAsync(string loginProvider, string providerKey)
        {
            
        }

        public void OnPostLinkLoginAsync(string provider)
        {
            
        }

        public void OnGetLinkLoginCallbackAsync()
        {
            
        }
    }
}
