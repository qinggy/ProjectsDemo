using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Aop.Sample.Aspects.Exception;
using System.ComponentModel.Composition.Aop.Sample.Aspects.Log;
using System.ComponentModel.Composition.Aop.Sample.Repositories;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample.Services.Impls
{
    [LogInterceptor(Order = 0)]
    [ExceptionInterceptor(Order = 1)]
    [AOPExport(typeof(IAccountService))]
    public class AccountServiceImpl : IAccountService
    {
        [Import]
        protected IUserRepository UserRepository { get; set; }


        public virtual  bool CreateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new Exception("用户名或密码不可为空！");

            if (ExistsUser(username))
                throw new Exception("用户名已存在！");

            return UserRepository.Add(username, password);
        }

        public virtual bool ExistsUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("要检测的用户名不可为空！");

            return UserRepository.Exists(x => x.Name == username);
        }
    }
}
