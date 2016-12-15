using System;
using DAL;
using DomainModel;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.ServiceContracts
{
    [ServiceContract]
    interface IOrderContract
    {
        [OperationContract]
        void Create(Order item);

        [OperationContract]
        void Delete(int id);

        [OperationContract]
        Order GetItem(int id);

        [OperationContract]
        IEnumerable<Order> GetItemsList();

        [OperationContract]
        void Update(Order item);
    }
}
