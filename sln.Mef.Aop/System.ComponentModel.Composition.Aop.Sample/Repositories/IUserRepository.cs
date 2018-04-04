using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Aop.Sample.Models;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample.Repositories
{
    public interface IUserRepository
    {
        bool Add(string username, string password);

        bool Exists(Func<User, bool> predicate);
    }
}
