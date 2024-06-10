using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IManageGameServiceCallback))]
    public interface IManageGameService
    {
        [OperationContract(IsOneWay = true)]
        void NewGame(int hostUserId, String gameName);

        [OperationContract(IsOneWay = true)]
        void JoinGame(int userId, String accessCode);

        [OperationContract(IsOneWay = true)]
        void DisconnectGame(int userId, int gameId);
    }

    [ServiceContract]
    public interface IManageGameServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void StartGameRoom(Gamematch game);

        [OperationContract(IsOneWay = true)]
        void AccessCodeNotFound();

        [OperationContract(IsOneWay = true)]
        void CompleteRoom();

        [OperationContract(IsOneWay = true)]
        void CanceledGame();

        [OperationContract(IsOneWay = true)]
        void StartGame(Gamematch game);
    }
}
