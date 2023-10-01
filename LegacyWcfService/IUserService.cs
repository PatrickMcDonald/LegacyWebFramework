using System.ServiceModel;
using System.Threading.Tasks;
using LegacyWcfService.Models;

namespace LegacyWcfService
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        UserInfo GetUserInfo(string userName);

        [OperationContract]
        Task AsyncMethod(string request);

        [OperationContract]
        void ParallelMethod(int count);
    }
}
