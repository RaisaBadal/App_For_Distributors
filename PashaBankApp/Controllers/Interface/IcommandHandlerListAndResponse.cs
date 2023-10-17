namespace PashaBankApp.Controllers.Interface
{
    public interface IcommandHandlerListAndResponse<TCommand, TResult>
    {
        List<TResult> Handle(TCommand command);
    }

}
