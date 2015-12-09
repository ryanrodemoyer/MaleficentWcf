using System;
using System.ServiceModel;

namespace Maleficent.Messages
{
    [ServiceContract]
    public interface IMaleficentService
    {
        [OperationContract]
        DateTime GetSystemDateTime();
    }
}
