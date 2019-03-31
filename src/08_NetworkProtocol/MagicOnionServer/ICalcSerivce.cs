using MagicOnion;
using MagicOnion.Server;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MagicOnionServer
{
    /* 加减乘除计算服务
     * UnaryResult 包装返回的参数（返回结果格式化的工作）
     * 
     * **/
    public interface ICalcSerivce : IService<ICalcSerivce>
    {
        UnaryResult<string> test1Async();
        UnaryResult<string> SumAsync(int x, int y);
        UnaryResult<string> ReduceAsync(int x, int y);
        UnaryResult<string> RideAsync(int x, int y);
        UnaryResult<string> ExceptAsync(int x, int y);
    }
    public class CalcSerivce : ServiceBase<ICalcSerivce>, ICalcSerivce
    {
        [CalcFilter]
        public async UnaryResult<string> ExceptAsync(int x, int y)
        {
            return await CalcUtil(x,y,CalcType.Except);
        }

        public async UnaryResult<string> ReduceAsync(int x, int y)
        {
            return await CalcUtil(x, y, CalcType.Reduce);
        }

        public async UnaryResult<string> RideAsync(int x, int y)
        {
            return await CalcUtil(x, y, CalcType.Except);
        }

        public async UnaryResult<string> SumAsync(int x, int y)
        {
            return await CalcUtil(x, y, CalcType.Sum);
        }

        public UnaryResult<string> test1Async()
        {
            throw new NotImplementedException();
        }

        public async Task<string> CalcUtil(int x, int y, CalcType calcType)
        {
            string result = string.Empty;
            switch (calcType)
            {
                case CalcType.Sum:
                    result = (x + y).ToString();
                    break;
                case CalcType.Ride:
                    result = (x - y).ToString();
                    break;
                case CalcType.Reduce:
                    result = (x * y).ToString();
                    break;
                case CalcType.Except:
                    result = (x / y).ToString();
                    break;
                default:
                    break;
            }
            await Task.Delay(100);//延迟响应100毫秒
            return result;
        }
        public enum CalcType
        {
            Sum,
            Ride,
            Reduce,
            Except
        }
    }
    /* MagicOnion 过滤器
     * 
     * 
     * **/
    public class CalcFilter : MagicOnionFilterAttribute
    {
        public CalcFilter() : base(null)
        {

        }
        public CalcFilter(Func<ServiceContext, ValueTask> next)
            : base(next)
        {

        }

        public override ValueTask Invoke(ServiceContext context)
        {
            try
            {
                //执行方法前
                Console.WriteLine(context.ServiceType);
                Console.WriteLine(context.MethodInfo.Name);
                return Next(context);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //执行方法后
                Console.WriteLine(context.ServiceType);
            }
        }
    }


}
