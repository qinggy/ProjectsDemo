using IS.Esd.Repository.Infrastructrue;
using IS.Esd.Repository.Infrastructrue.Contract;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Business.Infrastructrue
{
    public class BusinessStructureMapper
    {
        //public static void Run()
        //{
        //    ObjectFactory.Initialize(a =>
        //    {
        //        a.AddRegistry(new BusinessRegistry());
        //    });
        //}
    }

    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<IUnitOfWork>().Use<UnitOfWork>();
        }
    }
}
