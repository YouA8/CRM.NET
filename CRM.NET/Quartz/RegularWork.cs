using IService;
using Quartz;
using System.Threading.Tasks;

namespace CRM.NET.Quartz
{
    public class RegularWork : IJob
    {
        private readonly ITaskService _taskService;

        public RegularWork(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                _taskService.Hello();
            });
        }
    }
}
