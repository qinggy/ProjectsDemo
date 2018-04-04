using IS.Esd.Business;
using IS.Esd.Business.Infrastructrue;
using IS.Esd.Business.Interface;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IS.Esd.VersionManager.Web.Infrastructure
{
    public class StructureMapperControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            //return base.GetControllerInstance(requestContext, controllerType);
            try
            {
                if (requestContext == null || controllerType == null)
                {
                    return null;
                }

                return (Controller)ObjectFactory.GetInstance(controllerType);
            }
            catch (StructureMapException e)
            {

                throw;
            }

        }
    }

    public static class StructureMapper
    {
        public static void Run()
        {

            ControllerBuilder.Current.SetControllerFactory(new StructureMapperControllerFactory());

            ObjectFactory.Initialize(a =>
            {
                a.AddRegistry(new RepositoryRegistry());
                a.AddRegistry(new BusinessRegistry());
            });

        }
    }

    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<IClientBusiness>().Use<ClientBusiness>();
        }
    }
}