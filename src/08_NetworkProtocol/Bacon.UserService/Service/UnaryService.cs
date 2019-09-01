using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bacon.Service1.Data;
using Bacon.Service1.Filter;
using Bacon.ServiceDefine1;
using Bacon.ServiceDefine1.Dto;
using Bacon.ServiceDefine1.Entity;
using Bacon.ServiceDefine1.IService;
using MagicOnion;
using MagicOnion.Server;
using MongoDB.Bson.IO;
using Newtonsoft.Json;

namespace Bacon.Service1.Service
{
    [UserServiceFilter]
    public class UserService : ServiceBase<IUserService>, IUserService
    {
        public IUserRepository _UserRepository;
        public UserService()
        {
        }

        public async Task<UnaryResult<bool>> AddUser(UserDto user)
        {
            var locator        = Context.ServiceLocator;
            var userRepository = locator.GetService<IUserRepository>();
            var mapper = locator.GetService<IMapper>();
            var userTarget = mapper.Map<UserDto, UserEntity>(user);
            userTarget.id = user.Id;
            userTarget.Account = user.Account;
            await userRepository.AddUserAsync(userTarget);
            var userTarget2 = mapper.Map<UserDto, UserEntity>(user);
            return UnaryResult(true);
        }

        public async Task<UnaryResult<bool>> DeleteUser(string id)
        {
            var locator        = Context.ServiceLocator;
            var userRepository = locator.GetService<IUserRepository>();
            UserEntity userTarget = new UserEntity();
            var result =await userRepository.DeleteUserAsync(id);
            if (result<0)
            {
                return UnaryResult(false);
            }
            return UnaryResult(true);
        }

        public async Task<UnaryResult<bool>> UpdateUser(UserDto user)
        {
            var locator        = Context.ServiceLocator;
            var userRepository = locator.GetService<IUserRepository>();
            var mapper         = locator.GetService<IMapper>();
            var userTarget     = mapper.Map<UserDto, UserEntity>(user);
            var result         = await userRepository.UpdateUserInfoAsync(userTarget);
            if (result <= 0)
            {
                return UnaryResult(false);
            }

            return UnaryResult(true);
        }

        public async Task<UnaryResult<UserDto>> User(string id)
        {
            var locator        = Context.ServiceLocator;
            var userRepository = locator.GetService<IUserRepository>();
            var mapper         = locator.GetService<IMapper>();
            var resultUser = await userRepository.GetUser(id);
            var userTarget = mapper.Map<UserEntity, UserDto>(resultUser);
            return UnaryResult(userTarget);
        }

        public async Task<UnaryResult<List<UserDto>>> Users()
        {
            var locator        = Context.ServiceLocator;
            var userRepository = locator.GetService<IUserRepository>();
            var mapper         = locator.GetService<IMapper>();
            var reAllUsersAsync         = await userRepository.GetAllUsersAsync();
            var listDto = new List<UserDto>();
            foreach (var VARIABLE in reAllUsersAsync)
            {
                var userTarget = mapper.Map<UserEntity, UserDto>(VARIABLE);
                listDto.Add(userTarget);
            }
            return UnaryResult(listDto);
        }
    }
}
