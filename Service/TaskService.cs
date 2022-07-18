using IService;
using Serilog;

namespace Service
{
    public class TaskService : ITaskService
    {
        private readonly ILogger _logger;

        public TaskService(ILogger logger)
        {
            _logger = logger;
        }

        public void Hello()
        {
            _logger.Information("Hello");
        }
    }
}
