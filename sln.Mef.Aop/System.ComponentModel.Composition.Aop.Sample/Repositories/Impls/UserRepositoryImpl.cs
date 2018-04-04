using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Aop.Sample.Aspects.Security;
using System.Linq;
using System.Text;

namespace System.ComponentModel.Composition.Aop.Sample.Repositories.Impls
{
    [SecurityInterceptor]
    [AOPExport(typeof(IUserRepository))]
    public class UserRepositoryImpl : IUserRepository
    {

        [Security("Admin")]
        public virtual bool Add(string username, string password)
        {
            // ...
            return true;
        }

        public virtual bool Exists(Func<Models.User, bool> predicate)
        {
            var user = new Models.User()
            {
                Id = Guid.NewGuid(),
                Name = "Sun.M",
                Password = "123789"
            };

            return predicate.Invoke(user);
        }
    }
}
