using System;
using System.ServiceModel;
using WcfDuplexTest.API;

namespace WcfDuplexTest
{
    public class Program: SingletonPattern<Program>, IFrontendCallback
    {
        public static void Main(string[] args)
        {
            Program.GetInstance();
        }

        public void UpdateCredits(ulong val)
        {
            Console.WriteLine(val);
        }

        public Program()
        {
            var binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None; //disable security

            var ctx = new InstanceContext(this);//create context to pass to backend

            var endpoint = new EndpointAddress("net.tcp://localhost:8085/FrontendAdapter");

            var wcfClient = new WCFDuplexClientProxy<ICoreService>(ctx, binding, endpoint);//create service client connection
            wcfClient.Open();//go

            ICoreService coreService = wcfClient.Proxy;

            coreService.FrontEndLoaded();

            Console.Read();
        }
    }
}
