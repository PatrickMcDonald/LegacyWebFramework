using System.Threading.Tasks;
using LegacyWcfService.Models;
using LegacyWcfService.Tracing;

namespace LegacyWcfService
{
    public class UserService : IUserService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task AsyncMethod(string request)
        {
            log.InfoFormat("AsyncMethod: request={0}", request);

            await Task.Delay(250).ConfigureAwait(true);

            log.InfoFormat("after synchronized await, Trace Identifier={0}", ActivityManager.GetTraceId());

            await Task.Delay(250).ConfigureAwait(false);

            log.InfoFormat("after unsynchronized await, Trace Identifier={0}", ActivityManager.GetTraceId());
        }

        public UserInfo GetUserInfo(string userName)
        {
            log.InfoFormat("Get User Info for userName {0}", userName);
            return new UserInfo
            {
                UserName = userName,
                EmailAddress = $"{userName}@McdonaldConsulting.net",
            };
        }

        public void ParallelMethod(int count)
        {
            log.InfoFormat("ParallelMethod: count={0}", count);

            Parallel.For(0, count, i =>
            {
                log.InfoFormat("Parallel iteraltion {0}, Trace Identifier={1}", i, ActivityManager.GetTraceId());
            });
        }
    }
}
