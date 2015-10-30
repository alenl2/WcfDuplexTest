using WcfDuplexTest.API;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;

namespace WcfDuplexTest
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Program:SingletonPattern<Program>, ICoreService
    {
        private List<IFrontendCallback> callbacks = new List<IFrontendCallback>(); //frontend instaces saved here

        public static void Main(string[] args)
        {
            //Program.GetInstance();
			new Program ();
        }



        public void CallButtonPressed()
        {
            Console.WriteLine("Button !!");
        }

        public void FrontEndLoaded()
        {
            callbacks.Add(OperationContext.Current.GetCallbackChannel<IFrontendCallback>()); // add this instance
        }

        public Program()
        {
            Uri baseAddress = new Uri("net.tcp://localhost:8085/FrontendAdapter");


            ServiceHost host = new ServiceHost(this, baseAddress);

            NetTcpBinding binding = new NetTcpBinding();
            binding.Security.Mode = SecurityMode.None;

            host.AddServiceEndpoint(typeof(ICoreService), binding, "");
            host.Open();

            ulong i = 0;
            while (host.State == CommunicationState.Opened || host.State == CommunicationState.Opening || host.State == CommunicationState.Created)
            {
                foreach(IFrontendCallback cb in callbacks)
                {
                    if(cb != null)
                    {
                        try
                        {
                            cb.UpdateCredits(i);
                        }
                        catch (Exception) { }
                    }
                }
                i++;
                Console.WriteLine(i);
                Thread.Sleep(50);
            }

            host.Close();
        }


    }
}