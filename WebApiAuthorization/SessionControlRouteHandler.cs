using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.WebHost;

namespace WebApiAuthorization
{
    public class SessionControlRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(System.Web.Routing.RequestContext requestContext)
        {
            return new SessionHanlder(requestContext.RouteData);
        }
    }
}