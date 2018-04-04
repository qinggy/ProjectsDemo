using IS.Esd.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Business.Interface
{
    public interface IClientBusiness
    {
        Guid CreateClient(Client client);

        void DeleteClient(Guid id);

        void Update(Client client);

        IEnumerable<Client> GetAll();

        Client SingleOrDefault(Guid id);
    }
}
