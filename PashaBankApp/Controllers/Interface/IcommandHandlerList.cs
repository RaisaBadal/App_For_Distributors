using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Controllers.Interface
{
    public interface IcommandHandlerList<TCommandLIst>
    { 

        List<TCommandLIst> Handle();
    }
}
