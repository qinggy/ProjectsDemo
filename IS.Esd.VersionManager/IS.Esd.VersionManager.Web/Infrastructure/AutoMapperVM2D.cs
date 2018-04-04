using AutoMapper;
using IS.Esd.Domain.Entities;
using IS.Esd.VersionManager.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IS.Esd.VersionManager.Web.Infrastructure
{
    public class AutoMapperVM2D : Profile
    {
        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            {
                a.AddProfile<AutoMapperVM2D>();
            });
        }

        protected override void Configure()
        {
            base.Configure();

            #region VM to Domain
            AutoMapper.Mapper.CreateMap<ClientVM, Client>();
            AutoMapper.Mapper.CreateMap<ProductVM, Product>();
            AutoMapper.Mapper.CreateMap<RoleVM, Role>();
            #endregion

            #region Domain to VM

            AutoMapper.Mapper.CreateMap<Client, ClientVM>();
            AutoMapper.Mapper.CreateMap<Product, ProductVM>();
            AutoMapper.Mapper.CreateMap<Role, RoleVM>();
            #endregion
        }
    }
}