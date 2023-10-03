using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TraceTools;

namespace LegacyMvcApplication.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [HttpGet]
        public async Task<IEnumerable<object>> GetAsync()
        {
            return await Values();
        }

        private static async Task<IList<object>> Values()
        {
            var result = new List<object>();

            LogInfo("init", result);

            await Task.Delay(200);

            LogInfo("await", result);

            await Task.Run(() =>
            {
                LogInfo("Task.Run", result);
            });

            LogInfo("await", result);

            await Task.Delay(200).ConfigureAwait(false);

            LogInfo("not synchronised", result);

            Parallel.For(0, 2, i =>
            {
                LogInfo("parallel" + i, result);
            });

            return result;
        }

        private static void LogInfo(string title, IList<object> result)
        {
            log.InfoFormat("{0} {1}", title, ActivityManager.GetTraceId());

            result.Add(new { Title = title, TraceId = ActivityManager.GetTraceId() });
        }
    }
}
