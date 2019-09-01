using Grpc.Core;
using MagicOnion.Client;
using System;
using System.Threading.Tasks;
using Bacon.ServiceDefine1.Dto;
using Bacon.ServiceDefine1.IService;
using MagicOnion;
using System.Collections.Generic;

namespace Bacon.Client1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Client1();
        }

        public static async Task Client1()
        {
            //var channelCredentials = new SslCredentials(File.ReadAllText("roots.pem"));
            //var channelCredentials = await GoogleGrpcCredentials.GetApplicationDefaultAsync();
            //标准gRPC通道
            Channel channel  = new Channel("192.168.126.1", 5031, ChannelCredentials.Insecure);
            
            var service1 = MagicOnionClient.Create<IUserService>(channel);
            UserDto user =new UserDto();
            user.Id = 1;
            user.Account = "bacon";
            user.RealName = "高培根";
            user.NickName = "培根";
            user.Gender = true;
            user.MobilePhone = "123456789";
            await service1.AddUser(user);

        }
    }

    public class UserServiceClient : MagicOnionClientBase<IUserService>, IUserService
    {

        protected override MagicOnionClientBase<IUserService> Clone()
        {
            throw new NotImplementedException();
        }
        public Task<UnaryResult<bool>> AddUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<UnaryResult<bool>> DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UnaryResult<bool>> UpdateUser(UserDto user)
        {
            throw new NotImplementedException();
        }

        public Task<UnaryResult<UserDto>> User(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UnaryResult<List<UserDto>>> Users()
        {
            throw new NotImplementedException();
        }
    }
}
