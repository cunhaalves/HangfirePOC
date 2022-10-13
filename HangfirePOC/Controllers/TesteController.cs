using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Mvc;

namespace HangfirePOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteController : ControllerBase
    {

        public TesteController()
        {
        }

        [HttpPost]
        [Route(nameof(TesteController.FireAndForget))]
        public void FireAndForget()
        {
            Work work = new Work();
            var a = BackgroundJob.Enqueue(() => work.DoWork());
        }

        [HttpPost]
        [Route(nameof(TesteController.Schedule))]
        public void Schedule()
        {
            Work work = new Work();
            var b = BackgroundJob.Schedule(() => work.DoWork(), TimeSpan.FromSeconds(10));
        }

        [HttpPost]
        [Route(nameof(TesteController.Recurring))]
        public void Recurring()
        {
            Work work = new Work();
            RecurringJob.AddOrUpdate(() => work.DoWork(), Cron.Minutely);
        }

        [HttpGet]
        [Route(nameof(TesteController.GetProcessingJobs))]
        public IActionResult GetProcessingJobs()
        {
            var api = JobStorage.Current.GetMonitoringApi();
            var processingJobs = api.ProcessingJobs(0, 100);

            return Ok(new
            {
                success = true,
                data = processingJobs.Select(j=>j.Key).ToArray()
            });
        }

        [HttpGet]
        [Route(nameof(TesteController.GetJob))]
        public IActionResult GetJob(string jobId)
        {
            IStorageConnection connection = JobStorage.Current.GetConnection();
            JobData jobData = connection.GetJobData(jobId);
            return Ok(new
            {
                success = true,
                data = jobData.State
            });
        }

        [HttpGet]
        [Route(nameof(TesteController.GetStateDataFromJob))]
        public IActionResult GetStateDataFromJob(string jobId)
        {
            IStorageConnection connection = JobStorage.Current.GetConnection();
            StateData stateData = connection.GetStateData(jobId);
            return Ok(new
            {
                success = true,
                data = stateData
            });
        }
    }
}