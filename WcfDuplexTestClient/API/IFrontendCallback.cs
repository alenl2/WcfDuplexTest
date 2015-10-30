using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfDuplexTest.API
{
    public interface IFrontendCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateCredits (UInt64 val);
    }
    
}
