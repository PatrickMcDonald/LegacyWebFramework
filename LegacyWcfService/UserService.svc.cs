using LegacyWcfService.Models;

namespace LegacyWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public UserInfo GetUserInfo(string userName)
        {
            log.InfoFormat("Get User Info for userName {0}", userName);
            return new UserInfo
            {
                UserName = userName,
                EmailAddress = $"{userName}@McdonaldConsulting.net",
            };
        }
    }
}
