using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TraceTools;

namespace LegacyMvcApplication.Controllers
{
    public class ValuesController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<object>> GetAsync()
        {
            return await Values();
        }

        private static async Task<IList<object>> Values()
        {
            var result = new List<object>();

            ActivityManager.Log("init", result);

            await Task.Delay(200);

            ActivityManager.Log("await", result);

            await Task.Run(() =>
            {
                ActivityManager.Log("Task.Run", result);
            });

            ActivityManager.Log("await", result);

            await Task.Delay(200).ConfigureAwait(false);

            ActivityManager.Log("not synchronised", result);

            Parallel.For(0, 2, i =>
            {
                ActivityManager.Log("parallel" + i, result);
            });

            return result;
        }
    }
}
