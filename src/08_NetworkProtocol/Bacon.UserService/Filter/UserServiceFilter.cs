using Bacon.Service1.Util;
using MagicOnion.Server;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bacon.Service1.Filter
{
    public class UserServiceFilter : MagicOnionFilterAttribute
    {
        public UserServiceFilter():this(null)
        { }

        public UserServiceFilter(Func<ServiceContext, ValueTask> next) : base(next)
        {

        }
        public async override ValueTask Invoke(ServiceContext context)
        {
            try
            {
                var authContext = context.CallContext.AuthContext;
                
                var locator = context.ServiceLocator;
                //locator.GetService<>()
                await Next(context);
            }
            catch
            {
                context.Items[nameof(UserServiceFilter)] = (FilterCalledStatus)context.Items[nameof(UserServiceFilter)] | FilterCalledStatus.Catch;
                throw;
            }
            finally
            {
                context.Items[nameof(UserServiceFilter)] = (FilterCalledStatus)context.Items[nameof(UserServiceFilter)] | FilterCalledStatus.Finally;
            }
        }
    }
}
