using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DAL;
using DomainModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace OnlineShop.ServiceContracts
{
    public class UserContract : IUserContract
    {
        IRepository<User> user;

        public UserContract(IRepository<User> user)
        {
            this.user = user;
        }
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Create", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void Create(User item)
        {
            user.Create(item);
            user.Save();
        }
        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Delete", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void Delete(int id)
        {
            user.Delete(id);
            user.Save();
        }
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetItem/{id}", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public User GetItem(string id)
        {
            return user.GetItem(Convert.ToInt32(id));
        }
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetItemsList", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public IEnumerable<User> GetItemsList()
        {
            return user.GetItemsList();
        }
        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "Update", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public void Update(User item)
        {
            user.Update(item);
            user.Save();
        }
    }
}
