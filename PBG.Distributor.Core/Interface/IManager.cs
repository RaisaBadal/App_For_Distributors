using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IManager
    {
        Task<bool> RegistrationManager(InsertManager signup);
        Task<string> SignIn(GetManagerAuthent manAuth);
    }
}
