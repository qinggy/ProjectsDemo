using IS.Esd.Domain.Entities;
using IS.Esd.Repository.Infrastructrue;
using IS.Esd.Repository.Infrastructrue.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Repository
{
    public class ClientRepositroy : BaseRepository<Client>
    {
        public ClientRepositroy(IUnitOfWork unit)
            : base(unit)
        {

        }
    }
}
