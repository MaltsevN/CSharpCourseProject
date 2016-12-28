using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.DTO;
using OnlineShop.ServiceContracts.Security;
using DAL;
using DomainModel;
using AutoMapper;
using System.ServiceModel.Web;

namespace OnlineShop.ServiceContracts
{
    public class AccountContract : IAccountContract
    {
        private readonly IRepository<User> userRepository;

        public AccountContract(IUnitOfWork unitOfWork)
        {
            this.userRepository = unitOfWork.UserRepository;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public AuthenticationTokenDto SignIn(string login, string password)
        {
            AuthenticationTokenDto tokenDto = null;
            password = Encryptor.MD5Hash(password);
            User user = userRepository.GetItemsList().FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
            if(user != null)
            {
                if(user.AuthenticationToken == null)
                {
                    user.AuthenticationToken = new AuthenticationToken()
                    {
                        Login = user.Login,
                        Token = Guid.NewGuid().ToString()
                    };
                    userRepository.Update(user);
                    userRepository.Save();
                }
                
                tokenDto = Mapper.Map<AuthenticationToken, AuthenticationTokenDto>(user.AuthenticationToken);
                tokenDto.UserId = user.Id;
            }

            return tokenDto;
        }
    }
}
