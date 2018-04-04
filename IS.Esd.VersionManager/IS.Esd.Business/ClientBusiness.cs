using IS.Esd.Business.Interface;
using IS.Esd.Repository;
using IS.Esd.Repository.Infrastructrue.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IS.Esd.Business
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly ClientRepositroy clientRepository;
        private readonly IUnitOfWork unitOfWork;
        public ClientBusiness(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            clientRepository = new ClientRepositroy(_unitOfWork);
        }

        public Guid CreateClient(Domain.Entities.Client client)
        {
            return clientRepository.Create(client);
        }

        public void DeleteClient(Guid id)
        {
            clientRepository.Delete(id);
        }

        public void Update(Domain.Entities.Client client)
        {
            clientRepository.Update(client);
        }

        public IEnumerable<Domain.Entities.Client> GetAll()
        {
            return clientRepository.GetAll();
        }


        public Domain.Entities.Client SingleOrDefault(Guid id)
        {
            return clientRepository.SingleOrDefault(a => a.Id == id);
        }
    }
}
