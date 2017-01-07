using AutoMapper;
using DAL;
using DomainModel;
using OnlineShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.BL
{
    public class UserManager : IUserManager
    {
        IRepository<User> userRepository;

        static UserManager()
        {
            AutoMapperConfigurator.Configure();
        }

        public UserManager(IUnitOfWork unitOfWork)
        {
            this.userRepository = unitOfWork.UserRepository;
        }
        
        public UserDto Create(UserDto item)
        {
            User convertedUser = Mapper.Map<UserDto, User>(item);
            User user = userRepository.Create(convertedUser);
            userRepository.Save();
            UserDto dto = Mapper.Map<User, UserDto>(user);
            return dto;
        }
        
        public void Delete(int id)
        {
            userRepository.Delete(id);
            userRepository.Save();
        }
        
        public UserDto GetUser(int id)
        {
            User user = userRepository.GetItem(id);
            UserDto dto = Mapper.Map<User, UserDto>(user);
            return dto;
        }
        
        public IEnumerable<UserDto> GetAllUsers()
        {
            var items = userRepository.GetItemsList();
            IEnumerable<UserDto> dtos = Mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(items);
            return dtos;
        }
        
        public void Update(UserDto item)
        {
            User convertedUser = Mapper.Map<UserDto, User>(item);
            userRepository.Update(convertedUser);
            userRepository.Save();
        }
    }
}
