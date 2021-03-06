﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace CSA_Project.Models
{
    public static class Utilities
    {
        public static string GetFullName(this System.Security.Principal.IPrincipal usr)
        {
            var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("FullName");
            if (fullNameClaim != null)
                return fullNameClaim.Value;

            return "";
        }
        public static string GetID(this System.Security.Principal.IPrincipal usr)
        {
            var id = ((ClaimsIdentity)usr.Identity).FindFirst("id");
            if (id != null)
                return id.Value;

            return "";
        }
    }


}