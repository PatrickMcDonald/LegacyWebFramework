using System.ServiceModel;
using LegacyWcfService.Models;

namespace LegacyWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        UserInfo GetUserInfo(string userName);
    }
}
