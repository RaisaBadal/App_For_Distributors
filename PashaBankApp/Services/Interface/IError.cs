using PashaBankApp.DbContexti;
using PashaBankApp.Enums;
using PashaBankApp.Models;
using PashaBankApp.ResponseAndRequest;

namespace PashaBankApp.Services.Interface
{
    public interface IError
    {
        List<Error> GetAllErrors();

        List<Error> GetAllErrorsBetweenDate(ErrorBetweenDateRequest dateresp);
    }
}
