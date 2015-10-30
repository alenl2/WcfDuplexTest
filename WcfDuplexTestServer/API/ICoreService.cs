using System.ServiceModel;

namespace WcfDuplexTest.API
{


    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IFrontendCallback))]
    public interface ICoreService
    { 

        [OperationContract(IsOneWay = true)]
        void CallButtonPressed();

        [OperationContract(IsOneWay = true)]
        void FrontEndLoaded();
    }

    
}
