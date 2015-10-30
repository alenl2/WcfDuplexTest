using System.ServiceModel;
using System.ServiceModel.Channels;

public class WCFDuplexClientProxy<T> : DuplexClientBase<T> where T : class
{
    private bool _disposed = false;

    public WCFDuplexClientProxy(InstanceContext ctx,Binding binding, EndpointAddress endpointAddress) : base(ctx, binding, endpointAddress)
    {
    }

    public T Proxy
    {
        get { return this.Channel; }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (disposing)
            {
                if (this.State == CommunicationState.Faulted)
                {
                    base.Abort();
                }
                else
                {
                    try
                    {
                        base.Close();
                    }
                    catch
                    {
                        base.Abort();
                    }
                }
                _disposed = true;
            }
        }
    }
}