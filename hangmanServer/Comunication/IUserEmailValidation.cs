using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Comunication
{
    [ServiceContract]
    public interface IUserEmailValidation
    {
        [OperationContract]
        bool CheckIfEmailAndUserExist(String username, String email);

        [OperationContract]
        bool SendEmailValidation(String codeValidation, String email);

        [OperationContract]
        String GenerateValidationCode();
    }
}
