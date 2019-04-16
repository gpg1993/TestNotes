using MagicOnion;
using MagicOnion.Server;
using MessagePack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MagicOnionTest01
{
    /* 加减乘除计算服务
     * UnaryResult 包装返回的参数（返回结果格式化的工作）
     * 
     * **/
    public interface ICalcService : IService<ICalcService>
    {
        UnaryResult<string> test1Async();
        UnaryResult<string> SumAsync(int x, int y);
        UnaryResult<string> ReduceAsync(int x, int y);
        UnaryResult<string> RideAsync(int x, int y);
        UnaryResult<string> ExceptAsync(int x, int y);
    }
    public class CalcService : ServiceBase<ICalcService>, ICalcService
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

    [MessagePackObject(true)]
    public class Student
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string Age { get; set; }
        public double weight { get; set; }
    }
    public interface IStudentService : IService<IStudentService>
    {
        UnaryResult<Student> GetStudentAsync(int id);
        UnaryResult<bool> AddStudent(Student student);
        UnaryResult<List<Student>> GetAllStudent();
    }
    public class StudentService : ServiceBase<IStudentService>, IStudentService
    {
        public async UnaryResult<bool> AddStudent(Student student)
        {
            await Task.Delay(100);
            students.Add(student);
            return true;
        }
        public async UnaryResult<Student> GetStudentAsync(int id)
        {
            await Task.Delay(100);
            Student student = students.Find(c => c.Id == id);
            return student;
        }
        public UnaryResult<List<Student>> GetAllStudent()
        {
            return UnaryResult(students);
        }
        
        private static List<Student> students = new List<Student>()
        {
            new Student { Id = 1, Age = "11", name = "Alice" },
            new Student { Id = 2, Age = "12", name = "Bob" },
            new Student { Id = 3, Age = "13", name = "Cat" },
            new Student { Id = 4, Age = "14", name = "Dave" }
        };
        
    }
}
