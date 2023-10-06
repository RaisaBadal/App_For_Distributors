using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IManager
    {
        bool RegistrationManager(InsertManager signup);
        bool SignIn(GetManagerAuthent manAuth);
    }
}
