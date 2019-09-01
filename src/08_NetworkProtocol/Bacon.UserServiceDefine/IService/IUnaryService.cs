using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bacon.ServiceDefine1.Dto;
using MagicOnion;

namespace Bacon.ServiceDefine1.IService
{
    public interface IUserService: IService<IUserService>
    {
        Task<UnaryResult<List<UserDto>>> Users();
        Task<UnaryResult<UserDto>> User(string id);
        Task<UnaryResult<bool>> AddUser(UserDto user);
        Task<UnaryResult<bool>> DeleteUser(string id);
        Task<UnaryResult<bool>> UpdateUser(UserDto user);
    }
}
