using DataAccess;
using Logic;
using Logic.DTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace Comunication
{
    [ServiceContract(CallbackContract = typeof(IManageGameServiceCallback))]
    public interface IManageGameService
    {
        [OperationContract(IsOneWay = true)]
        void NewGame(Gamematch game);

        [OperationContract(IsOneWay = true)]
        void JoinGame(Gamematch gamematch, bool withAccessCode);

        [OperationContract(IsOneWay = true)]
        void DisconnectGame(int userId, int gameId);

        [OperationContract(IsOneWay = true)]
        void StartGame(int gameId);

        [OperationContract(IsOneWay = true)]
        void ValidateLetter(int gameId, char letter);

        [OperationContract(IsOneWay = true)]
        void Disconnect(int userId, int gameId);

        [OperationContract]
        List<DTOGameMatch> GetGamematches();

        [OperationContract]
        List<DTOStatistics> GetStatistics(int idChallenger);

        [OperationContract]
        List<DTOWord> GetWords(int idCategory);

        [OperationContract]
        int GetScorePlayer(int idPlayer);
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
        void UserConnectionNotification(Gamematch gamematch, string namePlayerGuesser);

        [OperationContract(IsOneWay = true)]
        void UserDisconectionNotification(Gamematch gamematch, string namePlayerGuesser);

        [OperationContract(IsOneWay = true)]
        void UserDisconected();

        [OperationContract(IsOneWay = true)]
        void StartGameChallenger(string word, string hint, string letter);

        [OperationContract(IsOneWay = true)]
        void StartGameGuesser(string hint, string letters);

        [OperationContract(IsOneWay = true)]
        void NotificationIfGuessed(char[] letters, int failedAttempts, bool guesser);

        [OperationContract(IsOneWay = true)]
        void FinishGame(string word, int score, bool win, bool challenger);
    }
}
