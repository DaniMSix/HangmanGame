using Logic.DTO;
using System;
using System.ServiceModel;

namespace Comunication
{
    [ServiceContract]
    public interface IPlayerManagement
    {
        [OperationContract]
        DTOPlayer AuthenticateLogin(String username, String password);

        [OperationContract]
        bool RegisterPlayer(DTOPlayer newPlayer);

        [OperationContract]
        bool UpdateEmailPassword(DTOPlayer dataPlayer);

        [OperationContract]
        bool UpdateFullProfile(DTOPlayer dataPlayer);
    }
}