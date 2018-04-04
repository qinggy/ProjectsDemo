using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace WebApiAuthorization
{
    public class SessionHanlder : HttpControllerHandler, IRequiresSessionState
    {
        public SessionHanlder(RouteData routData)
            : base(routData)
        {
        }
    }
}