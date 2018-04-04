using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DinoTechDataSyncService.WCF.Utilities
{
    public static class CommonFunctions
    {

        public static string GenerateToken()
        {
            Guid g = Guid.NewGuid();
            string token = Convert.ToBase64String(g.ToByteArray());
            token = token.Replace("=", "");
            token = token.Replace("+", "");
            return token;

        }
    }
}