using DataAccess;
using System.ServiceModel;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IManageGameServiceCallback))]
    public interface IManageGameService
    {
        [OperationContract(IsOneWay = true)]
        void NewGame(Gamematch game);

        [OperationContract(IsOneWay = true)]
        void JoinGame(Gamematch gamematch);

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
        void UserConnectionNotification(Gamematch game);
    }
}
