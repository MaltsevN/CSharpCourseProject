using DAL;
using DomainModel;
using OnlineShop.BL;
using OnlineShop.DAL.EntityFramework;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OnlineShop.ServiceContracts
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public class AuthorizeAttribute : Attribute, IOperationBehavior, IParameterInspector
    {
        private IUnitOfWork unitOfWork { get; set; }

        RankDto[] roles;

        public AuthorizeAttribute(RankDto[] Roles = null)
        {
            this.unitOfWork = new UnitOfWork();
            roles = Roles;
        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {

        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {

        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {

        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            AuthenticationTokenDto authToken = HttpContext.Current.GetToken();

            if (authToken == null)
            {
                throw new WebFaultException<string>("Unauthorized", HttpStatusCode.Unauthorized);
            }

            User user = unitOfWork.UserRepository.GetItemsList().FirstOrDefault(us => us.Login == authToken.Login);

            if (user == null)
            {
                throw new WebFaultException<string>("Unauthorized", HttpStatusCode.Unauthorized);
            }
            else
            {
                if (!user.AuthenticationToken.Token.Equals(authToken.Token))
                {
                    throw new WebFaultException<string>("Unauthorized", HttpStatusCode.Unauthorized);
                }

                if (roles != null)
                {
                    bool isInRole = false;
                    foreach (var role in roles)
                    {
                        if (role == (RankDto)user.Rank)
                        {
                            isInRole = true;
                            break;
                        }
                    }

                    if (!isInRole)
                    {
                        throw new FaultException("This user is not a member of these roles");
                    }
                }
            }

            return null;
        }

        public void Validate(OperationDescription operationDescription)
        {

        }
    }
}
