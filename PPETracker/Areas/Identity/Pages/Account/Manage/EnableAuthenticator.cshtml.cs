﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PPETracker.Models;

namespace PPETracker.Areas.Identity.Pages.Account.Manage
{
    public class EnableAuthenticatorModel : PageModel
    {
        

        public void OnGetAsync()
        {
           
        }

        public void OnPostAsync()
        {
            
        }

        private void LoadSharedKeyAndQrCodeUriAsync(ApplicationUser user)
        {
            
        }

        private void FormatKey(string unformattedKey)
        {
           
        }

        private void GenerateQrCodeUri(string email, string unformattedKey)
        {
           
        }
    }
}
