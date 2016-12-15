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
    interface IUserContract
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Create")]
        void Create(User item);

        [OperationContract]
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Delete")]
        void Delete(int id);

        [OperationContract]
        User GetItem(int id);

        [OperationContract]
        IEnumerable<User> GetItemsList();

        [OperationContract]
        void Update(User item);
    }
}
