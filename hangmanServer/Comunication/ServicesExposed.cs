using Logic;
using System.ServiceModel;

namespace Comunication
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class ServicesExposed : IPlayerManagement
    {
        public bool RegisterPlayer(Player newPlayer)
        {
            throw new System.NotImplementedException();
        }
    }
}
