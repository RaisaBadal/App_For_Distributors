namespace PashaBankApp.Controllers.Interface
{
    public interface ICommandHandler<TCommand>
    {
            bool Handle(TCommand command);
    }
}
