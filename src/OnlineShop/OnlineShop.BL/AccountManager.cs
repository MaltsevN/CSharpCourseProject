using AutoMapper;
using DAL;
using DomainModel;
using OnlineShop.BL.Security;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL
{
    public class AccountManager : IAccountManager
    {
        private readonly IRepository<User> userRepository;

        static AccountManager()
        {
            AutoMapperConfigurator.Configure();
        }

        public AccountManager(IUnitOfWork unitOfWork)
        {
            this.userRepository = unitOfWork.UserRepository;
        }
        
        public AuthenticationTokenDto SignIn(string login, string password)
        {
            AuthenticationTokenDto tokenDto = null;
            password = Encryptor.MD5Hash(password);
            User user = userRepository.GetItemsList().FirstOrDefault(u => u.Login.Equals(login) && u.Password.Equals(password));
            if (user != null)
            {
                if (user.AuthenticationToken == null)
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
