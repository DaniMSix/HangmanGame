using Logic;
using System.ServiceModel;

namespace Comunication
{
    [ServiceContract]
    public interface IPlayerManagement
    {
        [OperationContract]
        bool RegisterPlayer(Player newPlayer);
    }
}
