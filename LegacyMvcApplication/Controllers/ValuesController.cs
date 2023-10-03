using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace LegacyMvcApplication.Controllers
{
    public class ValuesController : ApiController
    {
        private static readonly AsyncLocal<string> TraceId = new AsyncLocal<string>();

        public ValuesController()
        {
            var traceId = Guid.NewGuid().ToString();
            TraceId.Value = traceId;
            HttpContext.Current.Items["activity"] = traceId;
        }

        // GET api/values
        public async Task<IEnumerable<object>> GetAsync()
        {
            return await Values();
        }

        private static async Task<IList<object>> Values()
        {
            var result = new List<object>();

            AddTrace("init", result);

            await Task.Delay(200);

            AddTrace("await", result);

            await Task.Run(() =>
            {
                AddTrace("Task.Run", result);
            });

            AddTrace("await", result);

            await Task.Run(() =>
            {
                TraceId.Value = Guid.NewGuid().ToString();
                AddTrace("Task.Run2", result);
            });

            AddTrace("await", result);

            await Task.Delay(200).ConfigureAwait(false);

            AddTrace("not synchronised", result);

            Parallel.For(0, 4, i => AddTrace("parallel" + i, result));

            return result;
        }

        private static void AddTrace(string title, IList<object> result)
        {
            result.Add(new { Title = title, TraceId = TraceId.Value, ContextActivity = HttpContext.Current?.Items["activity"] });
        }
    }
}
