using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace ServiceInterface
{
    [ServiceContract]
    public interface ICookService
    {
        [OperationContract]
        string GoToKitchen(string cookName, string hotel);

        [OperationContract]
        Cook GetCookInfo();
    }
}
