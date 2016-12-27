using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using DAL;
using DomainModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using OnlineShop.DTO;
using AutoMapper;

namespace OnlineShop.ServiceContracts
{
    public class UserContract : IUserContract
    {
        IRepository<User> userRepository;

        public UserContract(IUnitOfWork unitOfWork)
        {
            this.userRepository = unitOfWork.UserRepository;
        }

        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public UserDto Create(UserDto item)
        {
            User convertedUser = Mapper.Map<UserDto, User>(item);
            User user = userRepository.Create(convertedUser);
            userRepository.Save();
            UserDto dto = Mapper.Map<User, UserDto>(user);
            return dto;
        }

        [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Delete(int id)
        {
            userRepository.Delete(id);
            userRepository.Save();
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetUser/{id}")]
        public UserDto GetItem(string id)
        {
            User user = userRepository.GetItem(Convert.ToInt32(id));
            UserDto dto = Mapper.Map<User, UserDto>(user);
            return dto;
        }

        [WebGet(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, UriTemplate = "GetAllUsers")]
        public IEnumerable<UserDto> GetItemsList()
        {
            var items = userRepository.GetItemsList();
            IEnumerable<UserDto> dtos = Mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(items);
            return dtos;
        }

        [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        public void Update(UserDto item)
        {
            User convertedUser = Mapper.Map<UserDto, User>(item);
            userRepository.Update(convertedUser);
            userRepository.Save();
        }
    }
}

